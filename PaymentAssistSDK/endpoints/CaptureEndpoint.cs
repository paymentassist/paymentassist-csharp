namespace PaymentAssistSDK;

internal static class CaptureEndpoint
{
    public static Task<CaptureResponse> Fetch(CaptureRequest request)
    {
        Validate(request);

        var requestParams = new List<string>
        {
            "token=" + request.ApplicationID,
        };

        requestParams = RequestHelpers.RemoveEmptyParams(requestParams);
        var signature = RequestHelpers.GenerateSignature(requestParams, DataStore.APISecret);

        requestParams.Add("api_key=" + DataStore.APIKey);
        requestParams.Add("signature=" + signature);

        return RequestHelpers.DoAPIPOSTRequestAsync<CaptureResponse>(requestParams, "capture");
    }

    private static void Validate(CaptureRequest request)
    {
        if (string.IsNullOrEmpty(request.ApplicationID))
            throw new ArgumentException("ApplicationID cannot be empty");
    }
}
