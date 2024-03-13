using System.Text.Json.Serialization;

namespace PaymentAssistSDK;

/// <summary>
/// Contains the data returned by a successful call to the "update" endpoint.
/// </summary>
public class UpdateResponse
{
    /// <summary>
    /// The ID (token) of this application.
    /// </summary>
    [JsonPropertyName("token")]
    public string ApplicationID { get; set; } = ""; 

    /// <summary>
    /// The new order ID you requested, if any.
    /// </summary>
    [JsonPropertyName("order_id")]
    public string? OrderID { get; set; } 

    /// <summary>
    /// The new expiry time you requested in seconds, if any.
    /// </summary>
    [JsonPropertyName("expiry")]
    public int? ExpiresIn { get; set; } 

    /// <summary>
    /// The new amount you requested in pence, if any.
    /// </summary>
    [JsonPropertyName("amount")]
    public int? Amount { get; set; }
}