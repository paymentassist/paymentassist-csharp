using System.Text.Json.Serialization;

namespace PaymentAssistSDK;

/// <summary>
/// Contains the data returned by a call to the "capture" endpoint. Unlike some other
/// endpoints, "capture" can return a response even when unsuccessful.
/// </summary>
public class CaptureResponse
{
    /// <summary>
    /// The ID (token) of this application.
    /// </summary>
    [JsonPropertyName("token")]
    public string ApplicationID { get; set; } = "";

    /// <summary>
    /// The status of this application after the application was captured.
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = "";

    /// <summary>
    /// Indicates whether the deposit was successfully captured. This is always null if the application does not include a deposit.
    /// </summary>
    [JsonPropertyName("deposit_captured")]
    public bool? DepositCaptured { get; set; }

    /// <summary>
    /// If DepositCaptured is false, this contains the reason for capture failure. This is null in all other situations.
    /// </summary>
    [JsonPropertyName("deposit_reason")]
    public string? DepositCaptureFailureReason { get; set; }

}
