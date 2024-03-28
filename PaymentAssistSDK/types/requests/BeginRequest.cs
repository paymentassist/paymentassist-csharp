namespace PaymentAssistSDK;

/// <summary>
/// Contains the data for a request to the "begin" endpoint.
/// </summary>
public struct BeginRequest
{
    /// <summary>
    /// A unique invoice ID or order ID.
    /// </summary>
    public string OrderID;

    /// <summary>
    /// The invoice amount in pence.
    /// </summary>
    public int Amount;

    /// <summary>
    /// The customer's first name.
    /// </summary>
    public string CustomerFirstName;

    /// <summary>
    /// The customer's last name.
    /// </summary>
    public string CustomerLastName;

    /// <summary>
    /// The first line of the customer's address.
    /// </summary>
    public string CustomerAddress1;

    /// <summary>
    /// The second line of the customer's address.
    /// </summary>
    public string? CustomerAddress2;

    /// <summary>
    /// The third line of the customer's address.
    /// </summary>
    public string? CustomerAddress3;

    /// <summary>
    /// The customer's town.
    /// </summary>
    public string? CustomerTown;

    /// <summary>
    /// The customer's county.
    /// </summary>
    public string? CustomerCounty;

    /// <summary>
    /// The customer's postcode.
    /// </summary>
    public string CustomerPostcode;

    /// <summary>
    /// The customer's email address. This is required if SendEmail is true.
    /// </summary>
    public string? CustomerEmail;

    /// <summary>
    /// The customer's telephone number. This is required if SendSMS is true.
    /// </summary>
    public string? CustomerTelephone;

    /// <summary>
    /// Whether to send the application link to the customer via email. Defaults to false.
    /// </summary>
    public bool? SendEmail;

    /// <summary>
    /// Whether to send the application link to the customer via SMS. Defaults to false.
    /// </summary>
    public bool? SendSMS;

    /// <summary>
    /// If true, the customer will see a list of all available payment plans and will
    /// be able to select one themselves. Defaults to false.
    /// </summary>
    public bool? EnableMultiPlan;

    /// <summary>
    /// If true, a base64-encoded QR code will be returned, which the customer can scan with a mobile device to continue the application. Defaults to false.
    /// </summary>
    public bool? ReturnQRCode;
    
    /// <summary>
    /// Enables auto-capture (see https://api-docs.payment-assist.co.uk/auto-capture). Defaults to true.
    /// </summary>
    public bool? EnableAutoCapture;

    /// <summary>
    /// A URL you want the customer to be redirected to when the application is denied.
    /// </summary>
    public string? FailureURL;

    /// <summary>
    /// A URL you want the customer to be redirected to when the application is approved.
    /// </summary>
    public string? SuccessURL;

    /// <summary>
    /// A callback URL for receiving webhooks (see https://api-docs.payment-assist.co.uk/webhooks).
    /// </summary>
    public string? WebhookURL;

    /// <summary>
    /// The ID of the application's plan type. This is required if the account has access to multiple plan types and EnableMultiPlan is false.
    /// </summary>
    public int? PlanID;

    /// <summary>
    /// The vehicle's registration plate, where relevant.
    /// </summary>
    public string? VehicleRegistrationPlate;

    /// <summary>
    /// A description of the services or goods being sold.
    /// </summary>
    public string? Description;

    /// <summary>
    /// The amount of time before the application expires, in seconds. This is 24 hours by default.
    /// </summary>
    public int? Expiry;
}