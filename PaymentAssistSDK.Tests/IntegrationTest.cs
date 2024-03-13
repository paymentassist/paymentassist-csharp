using Xunit;
using System;

namespace PaymentAssistSDK.Tests;

public class IntegrationTest
{
    public IntegrationTest()
    {
        TestHelpers.TestsAreRunning = true;
        TestHelpers.IntegrationTestsAreRunning = true;
        PASDK.Initialise("test", "test", "");
    }
    
    [Fact]
    public void TestEndpoints()
    {
        // Remove this return and initialise with working credentials above in order to
        // run the integration tests.
        return;

        var accountResponse = Account();
        
        Plan(accountResponse);
        Preapproval();
        
        var beginResponse = Begin();

        Status(beginResponse.ApplicationID);
        Update(beginResponse.ApplicationID);
        Capture(beginResponse.ApplicationID);
        Invoice(beginResponse.ApplicationID);
    }

    private void Status(string applicationID)
    {
        var request = new StatusRequest{
            ApplicationID = applicationID,
        };

        var response = PASDK.Status(request);

        Assert.Equal(100000, response.Amount);
        Assert.True(response.ExpiresAt > DateTime.Now);
        Assert.True(response.ExpiresAt < DateTime.Now.AddHours(25));
        Assert.Equal(applicationID, response.ApplicationID);
        Assert.False(response.HasInvoice);
        Assert.False(response.RequriesInvoice);
        Assert.Equal("pending", response.Status);
    }

    private void Update(string applicationID)
    {
        // Test only updating some fields.
        var request = new UpdateRequest{
            ApplicationID = applicationID,
            Amount = 80000,
        };

        var response = PASDK.Update(request);

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

        response = PASDK.Update(request);

        Assert.Equal(70000, response.Amount);
        Assert.Equal(600, response.ExpiresIn);
        Assert.Null(response.OrderID);
    }

    private void Capture(string applicationID)
    {
        var request = new CaptureRequest{
            ApplicationID = applicationID,
        };

        // This is the closest we can get to testing it because only a completed application
        // can be captured.
        var exception = Assert.Throws<ArgumentException>(() => PASDK.Capture(new CaptureRequest()));
        Assert.Contains("Application is not awaiting capture", exception.Message);
    }

    private void Invoice(string applicationID)
    {
        var request = new InvoiceRequest{
            ApplicationID = applicationID,
            FileType = "txt",
            FileData = new byte[] { 0x01 },
        };

        var response = PASDK.Invoice(request);

        // Only a completed application can be invoiced so we are expecting this to fail.
        var exception = Assert.Throws<ArgumentException>(() => PASDK.Invoice(new InvoiceRequest()));
        Assert.Contains("Application is not yet completed", exception.Message);
    }

    private BeginResponse Begin()
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

        var response = PASDK.Begin(request);

        Assert.Equal(36, response.ApplicationID.Length);
        Assert.True(response.ContinuationURL.Length > 10);

    	return response;
    }

    private void Preapproval()
    {
        var request = new PreapprovalRequest{
            CustomerFirstName = "Test",
            CustomerLastName = "Testington",
            CustomerAddress1 = "Test House",
            CustomerPostcode = "TEST TES",
        };

        var response = PASDK.Preapproval(request);

        Assert.True(response.Approved);   
    }

    private void Plan(AccountResponse accountResponse)
    {
        var request = new PlanRequest{
            Amount = 50000,
            PlanID = accountResponse.Plans[0].ID,
        };

        var response = PASDK.Plan(request);

        Assert.Equal(50000, response.Amount);
        Assert.Equal(0, response.Interest);
        Assert.Equal(accountResponse.Plans[0].Name, response.PlanName);
        Assert.Equal(50000, response.TotalRepayable);
        Assert.Equal(4, response.PaymentSchedule.Count);
        Assert.True(response.PaymentSchedule[3].Amount > 0);
        Assert.True(response.PaymentSchedule[3].Date > DateTime.Now);
        Assert.True(response.PaymentSchedule[3].Date < DateTime.Now.AddMonths(5));
    }

    private AccountResponse Account()
    {
        var accountResponse = PASDK.Account();

        Assert.False(string.IsNullOrEmpty(accountResponse.DisplayName));
        Assert.False(string.IsNullOrEmpty(accountResponse.LegalName));
        Assert.NotEqual(0, accountResponse.Plans[0].APR);
        Assert.NotEqual(0, accountResponse.Plans[0].CommissionRate);
        Assert.False(string.IsNullOrEmpty(accountResponse.Plans[0].Frequency));
        Assert.False(string.IsNullOrEmpty(accountResponse.Plans[0].Name));
        Assert.True(accountResponse.Plans[0].DepositRequired);
        Assert.NotEqual(0, accountResponse.Plans[0].ID);
        Assert.NotEqual(0, accountResponse.Plans[0].Instalments);
        Assert.NotEqual(0, accountResponse.Plans[0].MaxAmount);

	    return accountResponse;
    }
}