namespace PaymentAssistSDK;

public struct InvoiceRequest
{
    /// <summary>
    /// The token you received when calling the "begin" endpoint.
    /// </summary>
    public string ApplicationToken;

    /// <summary>
    /// The file type. Some supported options are "pdf", "html", "txt", "doc" and "xls".
    /// </summary>
    public string FileType;

    /// <summary>
    /// The file as an array of bytes.
    /// </summary>
    public byte[] FileData;
}