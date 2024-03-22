using System.Text.Json.Serialization;

namespace PaymentAssistSDK;

/// <summary>
/// Contains the data returned by a successful call to the "preapproval" endpoint.
/// </summary>
public class PreapprovalResponse
{
    /// <summary>
    /// Whether or not this customer passed the pre-approval checks.
    /// </summary>
    [JsonPropertyName("approved")]
    public bool Approved { get; set; }
}
