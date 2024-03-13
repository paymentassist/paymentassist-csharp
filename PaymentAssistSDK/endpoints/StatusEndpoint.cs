namespace PaymentAssistSDK;

internal static class StatusEndpoint
{
    public static StatusResponse Fetch(StatusRequest request)
    {
        Validate(request);

        var requestParams = new List<string>
        {
            "token=" + request.ApplicationID
        };

        requestParams = RequestHelpers.RemoveEmptyParams(requestParams);

        var signature = RequestHelpers.GenerateSignature(requestParams, DataStore.APISecret);

        requestParams.Add("api_key=" + DataStore.APIKey);
        requestParams.Add("signature=" + signature);

        var requestURL = RequestHelpers.GetRequestURL();

        return RequestHelpers.DoAPIGETRequest<StatusResponse>(requestParams, requestURL + "status");
    }

    private static void Validate(StatusRequest request)
    {
        if (string.IsNullOrEmpty(request.ApplicationID))
            throw new ArgumentException("ApplicationID cannot be empty");
    }
}
