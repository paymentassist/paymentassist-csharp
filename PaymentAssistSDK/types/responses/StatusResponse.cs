using System.Text.Json.Serialization;

namespace PaymentAssistSDK;

/// <summary>
/// Contains the data returned by a successful call to the "status" endpoint.
/// </summary>
public class StatusResponse
{
    /// <summary>
    /// The ID (token) of this application.
    /// </summary>
    [JsonPropertyName("token")]
    public string ApplicationID { get; set; } = "";

    /// <summary>
    /// The status of this application.
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = "";

    /// <summary>
    /// The amount being applied for, in pence.
    /// </summary>
    [JsonPropertyName("amount")]
    public int Amount { get; set; }

    /// <summary>
    /// The time this application expires.
    /// </summary>
    [JsonPropertyName("expires_at")]
    public DateTime ExpiresAt { get; set; }

    /// <summary>
    /// Payment Assist's internal reference code for this application. This might be empty as an internal reference is not generated as soon as the application is started.
    /// </summary>
    [JsonPropertyName("pa_ref")]
    public string PaymentAssistReference { get; set; } = "";

    /// <summary>
    /// Whether an invoice needs to be uploaded for this application before payment can be made to the dealer.
    /// </summary>
    [JsonPropertyName("requires_invoice")]
    public bool RequriesInvoice { get; set; }

    /// <summary>
    /// Whether an invoice has been uploaded for this application.
    /// </summary>
    [JsonPropertyName("has_invoice")]
    public bool HasInvoice { get; set; }
}