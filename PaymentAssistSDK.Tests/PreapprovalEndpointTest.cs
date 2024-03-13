using Xunit;
using System;

namespace PaymentAssistSDK.Tests;

public class PreapprovalEndpointTest
{
    public PreapprovalEndpointTest()
    {
        TestHelpers.TestsAreRunning = true;
        TestHelpers.IntegrationTestsAreRunning = false;
        PASDK.Initialise("test", "test", "");
    }
    
    [Fact]
    public async void TestPreapproval()
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

    [Fact]
    public async void TestValidate()
    {
        var request = new PreapprovalRequest();

        var exception = await Assert.ThrowsAsync<ArgumentException>(() => PASDK.Preapproval(request));
        Assert.Equal("CustomerFirstName cannot be empty", exception.Message);

        request.CustomerFirstName = "test";
        exception = await Assert.ThrowsAsync<ArgumentException>(() => PASDK.Preapproval(request));
        Assert.Equal("CustomerLastName cannot be empty", exception.Message);

        request.CustomerLastName = "test";
        exception = await Assert.ThrowsAsync<ArgumentException>(() => PASDK.Preapproval(request));
        Assert.Equal("CustomerAddress1 cannot be empty", exception.Message);

        request.CustomerAddress1 = "test";
        exception = await Assert.ThrowsAsync<ArgumentException>(() => PASDK.Preapproval(request));
        Assert.Equal("CustomerPostcode cannot be empty", exception.Message);

        request.CustomerPostcode = "test";
        await PASDK.Preapproval(request);
    }
}