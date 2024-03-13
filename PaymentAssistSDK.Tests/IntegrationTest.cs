using Xunit;
using System;
using System.Threading.Tasks;
using System.Net.Http;

namespace PaymentAssistSDK.Tests;

public class IntegrationTest
{
    public IntegrationTest()
    {
        TestHelpers.TestsAreRunning = true;
        TestHelpers.IntegrationTestsAreRunning = true;
        PASDK.Initialise("", "", "");
    }
    
    [Fact]
    public async Task TestEndpoints()
    {
        // Remove this return and initialise with working credentials above in order to
        // run the integration tests.
        return;

        var accountResponse = await AccountAsync();
        
        await PlanAsync(accountResponse);
        await PreapprovalAsync();
        
        var beginResponse = await BeginAsync();

        await StatusAsync(beginResponse.ApplicationID);
        await UpdateAsync(beginResponse.ApplicationID);
        await CaptureAsync(beginResponse.ApplicationID);
        await InvoiceAsync(beginResponse.ApplicationID);
    }

    private async Task StatusAsync(string applicationID)
    {
        var request = new StatusRequest{
            ApplicationID = applicationID,
        };

        var response = await PASDK.Status(request);

        Assert.Equal(100000, response.Amount);
        Assert.True(response.ExpiresAt > DateTime.Now);
        Assert.True(response.ExpiresAt < DateTime.Now.AddHours(25));
        Assert.Equal(applicationID, response.ApplicationID);
        Assert.False(response.HasInvoice);
        Assert.False(response.RequriesInvoice);
        Assert.Equal("pending", response.Status);
    }

    private async Task UpdateAsync(string applicationID)
    {
        // Test only updating some fields.
        var request = new UpdateRequest{
            ApplicationID = applicationID,
            Amount = 80000,
        };

        var response = await PASDK.Update(request);

        Assert.Equal(80000, response.Amount);
        Assert.Null(response.ExpiresIn);
        Assert.Null(response.OrderID);

        // Test updating most fields. We can't easily test updating order ID
        // because that requires a completed application.
        request = new UpdateRequest{
            ApplicationID = applicationID,
            Amount = 70000,
            ExpiresIn = 60 * 10,
        };

        response = await PASDK.Update(request);

        Assert.Equal(70000, response.Amount);
        Assert.Equal(600, response.ExpiresIn);
        Assert.Null(response.OrderID);
    }

    private async Task CaptureAsync(string applicationID)
    {
        var request = new CaptureRequest{
            ApplicationID = applicationID,
        };

        // This is the closest we can get to testing it because only a completed application
        // can be captured.
        var exception = await Assert.ThrowsAsync<HttpRequestException>(() => PASDK.Capture(request));
        Assert.Contains("Application is not awaiting capture", exception.Message);
    }

    private async Task InvoiceAsync(string applicationID)
    {
        var request = new InvoiceRequest{
            ApplicationID = applicationID,
            FileType = "txt",
            FileData = new byte[] { 0x01 },
        };

        // Only a completed application can be invoiced so we are expecting this to fail.
        var exception = await Assert.ThrowsAsync<HttpRequestException>(() => PASDK.Invoice(request));
        Assert.Contains("Application is not yet completed", exception.Message);
    }

    private async Task<BeginResponse> BeginAsync()
    {
        var request = new BeginRequest{
            OrderID = TestHelpers.GetRandomID(),
            Amount = 100000,
            CustomerFirstName = "Test",
            CustomerLastName = "Testington",
            CustomerAddress1 = "Test House",
            CustomerPostcode = "TEST TES",
            EnableAutoCapture = false,
        };

        var response = await PASDK.Begin(request);

        Assert.Equal(36, response.ApplicationID.Length);
        Assert.True(response.ContinuationURL.Length > 10);

    	return response;
    }

    private async Task PreapprovalAsync()
    {
        var request = new PreapprovalRequest{
            CustomerFirstName = "Test",
            CustomerLastName = "Testington",
            CustomerAddress1 = "Test House",
            CustomerPostcode = "TEST TES",
        };

        var response = await PASDK.Preapproval(request);

        Assert.True(response.Approved);   
    }

    private async Task PlanAsync(AccountResponse accountResponse)
    {
        var request = new PlanRequest{
            Amount = 50000,
            PlanID = accountResponse.Plans[0].ID,
        };

        var response = await PASDK.Plan(request);

        Assert.Equal(50000, response.Amount);
        Assert.Equal(0, response.Interest);
        Assert.Equal(accountResponse.Plans[0].Name, response.PlanName);
        Assert.Equal(50000, response.TotalRepayable);
        Assert.Equal(4, response.PaymentSchedule.Count);
        Assert.True(response.PaymentSchedule[3].Amount > 0);
        Assert.True(response.PaymentSchedule[3].Date > DateTime.Now.Date, "date was "+response.PaymentSchedule[3].Date);
        Assert.True(response.PaymentSchedule[3].Date < DateTime.Now.Date.AddMonths(5));
    }

    private async Task<AccountResponse> AccountAsync()
    {
        var accountResponse = await PASDK.Account();

        Assert.False(string.IsNullOrEmpty(accountResponse.DisplayName));
        Assert.False(string.IsNullOrEmpty(accountResponse.LegalName));
        Assert.False(string.IsNullOrEmpty(accountResponse.Plans[0].Frequency));
        Assert.False(string.IsNullOrEmpty(accountResponse.Plans[0].Name));
        Assert.True(accountResponse.Plans[0].DepositRequired);
        Assert.NotEqual(0, accountResponse.Plans[0].ID);
        Assert.NotEqual(0, accountResponse.Plans[0].Instalments);
        Assert.NotEqual(0, accountResponse.Plans[0].MaxAmount);

	    return accountResponse;
    }
}