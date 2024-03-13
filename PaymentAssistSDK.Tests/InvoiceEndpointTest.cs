using Xunit;
using System;

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
    public void TestInvoice()
    {
        var request = new InvoiceRequest{
            ApplicationID = "aed3bd4e-c478-4d73-a6fa-3640a7155e4f",
            FileType = "txt",
            FileData = new byte[] { 0x01 },
        };

        var response = PASDK.Invoice(request);

        Assert.Equal("aed3bd4e-c478-4d73-a6fa-3640a7155e4f", response.ApplicationID);
        Assert.Equal("success", response.UploadStatus);
    }

    [Fact]
    public void TestValidate()
    {
        var request = new InvoiceRequest();

        var exception = Assert.Throws<ArgumentException>(() => PASDK.Invoice(request));
        Assert.Equal("ApplicationID cannot be empty", exception.Message);

        request.ApplicationID = "test";
        exception = Assert.Throws<ArgumentException>(() => PASDK.Invoice(request));
        Assert.Equal("FileType cannot be empty", exception.Message);

        request.FileType = "test";
        exception = Assert.Throws<ArgumentException>(() => PASDK.Invoice(request));
        Assert.Equal("FileData cannot be empty", exception.Message);

        request.FileData = new byte[] { 0x01 };

        PASDK.Invoice(request);
    }
}