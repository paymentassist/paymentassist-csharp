namespace PaymentAssistSDK;

internal static class UpdateEndpoint
{
    public static Task<UpdateResponse> Fetch(UpdateRequest request)
    {
        Validate(request);

        var requestParams = new List<string>
        {
            "amount=" + request.Amount,
            "expiry=" + request.ExpiresIn,
            "order_id=" + request.OrderID,
            "token=" + request.ApplicationID
        };

        requestParams = RequestHelpers.RemoveEmptyParams(requestParams);

        var signature = RequestHelpers.GenerateSignature(requestParams, DataStore.APISecret);

        requestParams.Add("api_key=" + DataStore.APIKey);
        requestParams.Add("signature=" + signature);

        return RequestHelpers.DoAPIPOSTRequestAsync<UpdateResponse>(requestParams, "update");
    }

    private static void Validate(UpdateRequest request)
    {
        if (string.IsNullOrEmpty(request.ApplicationID))
            throw new ArgumentException("ApplicationID cannot be empty");
    }
}
