using Xunit;
using System;

namespace PaymentAssistSDK.Tests;

public class BeginEndpointTest
{
    public BeginEndpointTest()
    {
        TestHelpers.TestsAreRunning = true;
        TestHelpers.IntegrationTestsAreRunning = false;
        PASDK.Initialise("test", "test", "");
    }
    
    [Fact]
    public void TestBegin()
    {
        var request = new BeginRequest{
            OrderID = "111",
            Amount = 50000,
            CustomerFirstName = "Test",
            CustomerLastName = "Testington",
            CustomerAddress1 = "Test House",
            CustomerPostcode = "TEST TES",
        };

        var response = PASDK.Begin(request);

        Assert.Equal("0138ef43-f703-41cb-8f08-f36f41b47560", response.ApplicationID);
        Assert.Contains("https://", response.ContinuationURL);
    }

    [Fact]
    public void TestValidate()
    {
        var request = new BeginRequest();

        var exception = Assert.Throws<ArgumentException>(() => PASDK.Begin(request));
        Assert.Equal("OrderID cannot be empty", exception.Message);

        request.OrderID = "test";
        exception = Assert.Throws<ArgumentException>(() => PASDK.Begin(request));
        Assert.Equal("Amount must be greater than 0", exception.Message);

        request.Amount = 50000;
        exception = Assert.Throws<ArgumentException>(() => PASDK.Begin(request));
        Assert.Equal("CustomerFirstName cannot be empty", exception.Message);

        request.CustomerFirstName = "test";
        exception = Assert.Throws<ArgumentException>(() => PASDK.Begin(request));
        Assert.Equal("CustomerLastName cannot be empty", exception.Message);

        request.CustomerLastName = "test";
        exception = Assert.Throws<ArgumentException>(() => PASDK.Begin(request));
        Assert.Equal("CustomerAddress1 cannot be empty", exception.Message);

        request.CustomerAddress1 = "test";
        exception = Assert.Throws<ArgumentException>(() => PASDK.Begin(request));
        Assert.Equal("CustomerPostcode cannot be empty", exception.Message);

        request.CustomerPostcode = "test";
        PASDK.Begin(request);

        request.SendEmail = true;
        exception = Assert.Throws<ArgumentException>(() => PASDK.Begin(request));
        Assert.Equal("CustomerEmail cannot be empty if SendEmail is true", exception.Message);

        request.CustomerEmail = "test";
        PASDK.Begin(request);

        request.SendSMS = true;
        exception = Assert.Throws<ArgumentException>(() => PASDK.Begin(request));
        Assert.Equal("CustomerTelephone cannot be empty if SendSMS is true", exception.Message);

        request.CustomerTelephone = "test";
        PASDK.Begin(request);
    }
}