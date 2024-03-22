using Xunit;
using System;
using System.Threading.Tasks;

namespace PaymentAssistSDK.Tests;

public class StatusEndpointTest
{
    public StatusEndpointTest()
    {
        TestHelpers.TestsAreRunning = true;
        TestHelpers.IntegrationTestsAreRunning = false;
        PASDK.Initialise("test", "test", "");
    }
    
    [Fact]
    public async Task TestStatus()
    {
        var request = new StatusRequest{
            ApplicationToken = "aed3bd4e-c478-4d73-a6fa-3640a7155e4f",
        };

        var response = await PASDK.Status(request);

        Assert.Equal("aed3bd4e-c478-4d73-a6fa-3640a7155e4f", response.ApplicationToken);
        Assert.Equal("pending", response.Status);
        Assert.Equal(50000, response.Amount);
        Assert.Equal(new DateTime(2022, 5, 24, 19, 28, 6), response.ExpiresAt);
        Assert.True(response.HasInvoice);
        Assert.True(response.RequriesInvoice);
        Assert.Equal("testreference", response.PaymentAssistReference);
    }

    [Fact]
    public void TestValidate()
    {
        var request = new StatusRequest();

        var exception = Assert.ThrowsAsync<ArgumentException>(() => PASDK.Status(request)).GetAwaiter().GetResult();
        Assert.Equal("ApplicationToken cannot be empty", exception.Message);

        request.ApplicationToken = "test";
        PASDK.Status(request);
    }
}