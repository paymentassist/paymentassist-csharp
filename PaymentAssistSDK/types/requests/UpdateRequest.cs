namespace PaymentAssistSDK;

/// <summary>
/// Contains the data for a request to the "update" endpoint.
/// </summary>
public struct UpdateRequest
{
    /// <summary>
    /// The token you received when calling the "begin" endpoint.
    /// </summary>
    public string ApplicationToken;

    /// <summary>
    /// Your new order ID. You can only change this if the application's status is "completed".
    /// </summary>
    public string? OrderID;

    /// <summary>
    /// The new expiry time for this appication in seconds from now. Setting this to 0 will instantly expire the application. You can only change this if the application's status is "pending", "in_progress" or "pending_capture".
    /// </summary>
    public int? ExpiresIn;

    /// <summary>
    /// The new amount for this application in pence. You can only change this if the application's status is "pending", "in_progress" or "pending_capture". The new amount must be less than the current amount.
    /// </summary>
    public int? Amount;   
}
