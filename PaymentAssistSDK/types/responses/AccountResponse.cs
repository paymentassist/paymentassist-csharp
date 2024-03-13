using System.Text.Json.Serialization;

namespace PaymentAssistSDK;

/// <summary>
/// Contains the data returned by a successful call to the "account" endpoint.
/// </summary>
public class AccountResponse
{
    /// <summary>
    /// The legal name of the dealer.
    /// </summary>
    [JsonPropertyName("legal_name")]
    public string LegalName { get; set; } = "";

    /// <summary>
    /// The display name of the dealer.
    /// </summary>
    [JsonPropertyName("display_name")]
    public string DisplayName { get; set; } = "";

    /// <summary>
    /// A list of available plan types for this dealer.
    /// </summary>
    [JsonPropertyName("plans")]
    public List<Plan> Plans { get; set; } = new();
}