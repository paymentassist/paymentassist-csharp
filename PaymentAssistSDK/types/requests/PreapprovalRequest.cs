namespace PaymentAssistSDK;

/// <summary>
/// Contains the data for a request to the "preapproval" endpoint.
/// </summary>
public struct PreapprovalRequest
{
    /// <summary>
    /// The customer's first name.
    /// </summary>
    public string CustomerFirstName;

    /// <summary>
    /// The customer's last name.
    /// </summary>
    public string CustomerLastName;

    /// <summary>
    /// The customer's postode.
    /// </summary>
    public string CustomerPostcode;

    /// <summary>
    /// The first line of the customer's address.
    /// </summary>
    public string CustomerAddress1;
}