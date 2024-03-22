using System.Text.Json.Serialization;

namespace PaymentAssistSDK;

/// <summary>
/// Contains the data returned by a call to the "invoice" endpoint. Unlike some
/// endpoints, "invoice" can return a response even if the upload was unsuccessful.
/// </summary>
public class InvoiceResponse
{
    /// <summary>
    /// The ID (token) of this application.
    /// </summary>
    [JsonPropertyName("token")]
    public string ApplicationToken { get; set; } = "";

    /// <summary>
    /// The status of the upload ("success" or "failed").
    /// </summary>
    [JsonPropertyName("upload_status")]
    public string UploadStatus { get; set; } = "";
}