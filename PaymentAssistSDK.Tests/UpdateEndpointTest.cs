using Xunit;
using System;
using System.Threading.Tasks;

namespace PaymentAssistSDK.Tests;

public class UpdateEndpointTest
{
    public UpdateEndpointTest()
    {
        TestHelpers.TestsAreRunning = true;
        TestHelpers.IntegrationTestsAreRunning = false;
        PASDK.Initialise("test", "test", "");
    }
    
    [Fact]
    public async Task TestUpdate()
    {
        var request = new UpdateRequest{
            ApplicationToken = "aed3bd4e-c478-4d73-a6fa-3640a7155e4f",
            Amount = 100000,
            OrderID = "neworderid",
            ExpiresIn = 600,
        };

        var response = await PASDK.Update(request);

        Assert.Equal("aed3bd4e-c478-4d73-a6fa-3640a7155e4f", response.ApplicationToken);
        Assert.Equal(100000, response.Amount);
        Assert.Equal("neworderid", response.OrderID);
        Assert.Equal(600, response.ExpiresIn);
    }

    [Fact]
    public async Task TestValidate()
    {
        var request = new UpdateRequest();

        var exception = await Assert.ThrowsAsync<ArgumentException>(() => PASDK.Update(request));
        Assert.Equal("ApplicationToken cannot be empty", exception.Message);

        request.ApplicationToken = "test";
        await PASDK.Update(request);
    }
}