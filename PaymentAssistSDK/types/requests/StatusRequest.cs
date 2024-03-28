namespace PaymentAssistSDK;

/// <summary>
/// Contains the data for a request to the "status" endpoint.
/// </summary>
public struct StatusRequest
{
    /// <summary>
    /// The token you received when calling the "begin" endpoint.
    /// </summary>
    public string ApplicationToken;
}
