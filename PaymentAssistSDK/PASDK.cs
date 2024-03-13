namespace PaymentAssistSDK;

public static class PASDK
{
    /// <summary>
    /// Call this function to initialise the SDK with your API credentials as well as
    /// the URL of the API server you want to send requests to.
    /// </summary>
    public static void Initialise(string apiKey, string apiSecret, string apiURL)
    {
        // Let's not do any validation here as the user may want to wipe or change
        // their credentials.
        DataStore.APIKey = apiKey;
        DataStore.APISecret = apiSecret;
        DataStore.APIURL = apiURL;
    }

    /// <summary>
    /// Returns information about this dealer's account, including what plan types are available.
    /// </summary>
    public static AccountResponse Account()
    {
        return AccountEndpoint.Fetch();
    }

    /// <summary>
    /// Begins the application process.
    /// </summary>
    public static BeginResponse Begin(BeginRequest request)
    {
        return BeginEndpoint.Fetch(request);
    }

    /// <summary>
    /// Finalises an application that's currently in a "pending_capture" state.
    /// </summary>
    public static CaptureResponse Capture(CaptureRequest request)
    {
        return CaptureEndpoint.Fetch(request); 
    }

    /// <summary>
    /// Uploads an invoice for a completed application.
    /// </summary>
    public static InvoiceResponse Invoice(InvoiceRequest request)
    {
        return InvoiceEndpoint.Fetch(request);
    }

    /// <summary>
    /// Generates a repayment schedule for a hypothetical application.
    /// Accepts a transaction amount and an optional plan ID and term length,
    /// returning a full repayment schedule including amounts and dates.
    /// </summary>
    public static PlanResponse Plan(PlanRequest request)
    {
        return PlanEndpoint.Fetch(request);
    }

    /// <summary>
    /// Checks the eligibity of a customer in advance.
    /// Success simply means that the customer has passed our internal checks. They
    /// will still need to have funds available to cover any deposit payment for
    /// the application to be successful.
    /// </summary>
    public static PreapprovalResponse Preapproval(PreapprovalRequest request)
    {
        return PreapprovalEndpoint.Fetch(request);
    }

    /// <summary>
    /// Checks the status of an existing application.
    /// </summary>
    public static StatusResponse Status(StatusRequest request)
    {
        return StatusEndpoint.Fetch(request);
    }

    /// <summary>
    /// Allows you to update an existing application.
    /// </summary>
    public static UpdateResponse Update(UpdateRequest request)
    {
        return UpdateEndpoint.Fetch(request);
    }
}
