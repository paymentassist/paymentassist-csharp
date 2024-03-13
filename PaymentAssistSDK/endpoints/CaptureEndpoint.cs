namespace PaymentAssistSDK;

internal static class CaptureEndpoint
{
    public static CaptureResponse Fetch(CaptureRequest request)
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

        var requestURL = RequestHelpers.GetRequestURL();

        return RequestHelpers.DoAPIPOSTRequest<CaptureResponse>(requestParams, requestURL + "capture");
	}

    private static void Validate(CaptureRequest request)
    {
        if (string.IsNullOrEmpty(request.ApplicationID))
            throw new ArgumentException("ApplicationID cannot be empty");
    }
}
