using Xunit;
using System;

namespace PaymentAssistSDK.Tests;

public class CaptureEndpointTest
{
    public CaptureEndpointTest()
    {
        TestHelpers.TestsAreRunning = true;
        TestHelpers.IntegrationTestsAreRunning = false;
        PASDK.Initialise("test", "test", "");
    }
    
    [Fact]
    public void TestCapture()
    {
        var request = new CaptureRequest{
            ApplicationID = "aed3bd4e-c478-4d73-a6fa-3640a7155e4f",
        };

        var response = PASDK.Capture(request);

        Assert.Equal("aed3bd4e-c478-4d73-a6fa-3640a7155e4f", response.ApplicationID);
        Assert.Equal("completed", response.Status);
        Assert.True(response.DepositCaptured);
        Assert.Null(response.DepositCaptureFailureReason);
    }

    [Fact]
    public void TestValidate()
    {
        var request = new CaptureRequest();

        var exception = Assert.Throws<ArgumentException>(() => PASDK.Capture(request));
        Assert.Equal("ApplicationID cannot be empty", exception.Message);

        request.ApplicationID = "test";
        PASDK.Capture(request);
    }
}