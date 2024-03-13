using Xunit;
using System;

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
    public async void TestUpdate()
    {
        var request = new UpdateRequest{
            ApplicationID ="aed3bd4e-c478-4d73-a6fa-3640a7155e4f",
            Amount = 100000,
            OrderID = "neworderid",
            ExpiresIn = 600,
        };

        var response = await PASDK.Update(request);

        Assert.Equal("aed3bd4e-c478-4d73-a6fa-3640a7155e4f", response.ApplicationID);
        Assert.Equal(100000, response.Amount);
        Assert.Equal("neworderid", response.OrderID);
        Assert.Equal(600, response.ExpiresIn);
    }

    [Fact]
    public async void TestValidate()
    {
        var request = new UpdateRequest();

        var exception = await Assert.ThrowsAsync<ArgumentException>(() => PASDK.Update(request));
        Assert.Equal("ApplicationID cannot be empty", exception.Message);

        request.ApplicationID = "test";
        await PASDK.Update(request);
    }
}