using System.Text.Json.Serialization;

namespace PaymentAssistSDK;

/// <summary>
/// Contains the data returned by a successful call to the "begin" endpoint.
/// </summary>
public class BeginResponse
{
    /// <summary>
    /// A token representing the application that was created. You should save this for later use.
    /// </summary>
    [JsonPropertyName("token")]
    public string ApplicationToken { get; set; } = "";

    /// <summary>
    /// The URL you should direct the customer to so that they can complete the rest of the signup process.
    /// </summary>
    [JsonPropertyName("url")]
    public string ContinuationURL { get; set; } = "";
}