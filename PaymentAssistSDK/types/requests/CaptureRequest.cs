namespace PaymentAssistSDK;

/// <summary>
/// Contains the data for a request to the "capture" endpoint.
/// </summary>
public struct CaptureRequest
{
    /// <summary>
    /// The token you received when calling the "begin" endpoint.
    /// </summary>
    public string ApplicationToken;
}