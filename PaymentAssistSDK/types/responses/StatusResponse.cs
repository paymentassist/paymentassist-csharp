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
    public string ApplicationToken { get; set; } = "";

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
    /// Payment Assist's reference for this application. This may be empty as a reference is not generated until the finance facility or payment is successfully created (once an application moves to a "completed" status).
    /// </summary>
    [JsonPropertyName("pa_ref")]
    public string PaymentAssistReference { get; set; } = "";

    /// <summary>
    /// Whether an invoice needs to be uploaded for this application before funds will be released to the merchant.
    /// </summary>
    [JsonPropertyName("requires_invoice")]
    public bool RequriesInvoice { get; set; }

    /// <summary>
    /// Whether an invoice has been uploaded for this application.
    /// </summary>
    [JsonPropertyName("has_invoice")]
    public bool HasInvoice { get; set; }
}