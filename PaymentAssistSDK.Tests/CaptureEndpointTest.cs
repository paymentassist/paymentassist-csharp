using Xunit;
using System;
using System.Threading.Tasks;

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
    public async Task TestCapture()
    {
        var request = new CaptureRequest{
            ApplicationToken = "aed3bd4e-c478-4d73-a6fa-3640a7155e4f",
        };

        var response = await PASDK.Capture(request);

        Assert.Equal("aed3bd4e-c478-4d73-a6fa-3640a7155e4f", response.ApplicationToken);
        Assert.Equal("completed", response.Status);
        Assert.True(response.DepositCaptured);
        Assert.Null(response.DepositCaptureFailureReason);
    }

    [Fact]
    public async Task TestValidate()
    {
        var request = new CaptureRequest();

        var exception = await Assert.ThrowsAsync<ArgumentException>(() => PASDK.Capture(request));
        Assert.Equal("ApplicationToken cannot be empty", exception.Message);

        request.ApplicationToken = "test";
        await PASDK.Capture(request);
    }
}