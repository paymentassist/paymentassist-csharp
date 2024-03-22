using Xunit;
using System;
using System.Threading.Tasks;

namespace PaymentAssistSDK.Tests;

public class InvoiceEndpointTest
{
    public InvoiceEndpointTest()
    {
        TestHelpers.TestsAreRunning = true;
        TestHelpers.IntegrationTestsAreRunning = false;
        PASDK.Initialise("test", "test", "");
    }
    
    [Fact]
    public async Task TestInvoice()
    {
        var request = new InvoiceRequest{
            ApplicationToken = "aed3bd4e-c478-4d73-a6fa-3640a7155e4f",
            FileType = "txt",
            FileData = new byte[] { 0x01 },
        };

        var response = await PASDK.Invoice(request);

        Assert.Equal("aed3bd4e-c478-4d73-a6fa-3640a7155e4f", response.ApplicationToken);
        Assert.Equal("success", response.UploadStatus);
    }

    [Fact]
    public async Task TestValidate()
    {
        var request = new InvoiceRequest();

        var exception = await Assert.ThrowsAsync<ArgumentException>(() => PASDK.Invoice(request));
        Assert.Equal("ApplicationToken cannot be empty", exception.Message);

        request.ApplicationToken = "test";
        exception = await Assert.ThrowsAsync<ArgumentException>(() => PASDK.Invoice(request));
        Assert.Equal("FileType cannot be empty", exception.Message);

        request.FileType = "test";
        exception = await Assert.ThrowsAsync<ArgumentException>(() => PASDK.Invoice(request));
        Assert.Equal("FileData cannot be empty", exception.Message);

        request.FileData = new byte[] { 0x01 };

        await PASDK.Invoice(request);
    }
}