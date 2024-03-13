namespace PaymentAssistSDK;

internal static class PreapprovalEndpoint
{
    public static Task<PreapprovalResponse> Fetch(PreapprovalRequest request)
    {
        Validate(request);

        var requestParams = new List<string>
        {
            "addr1=" + request.CustomerAddress1,
            "f_name=" + request.CustomerFirstName,
            "postcode=" + request.CustomerPostcode,
            "s_name=" + request.CustomerLastName
        };

        requestParams = RequestHelpers.RemoveEmptyParams(requestParams);

        var signature = RequestHelpers.GenerateSignature(requestParams, DataStore.APISecret);

        requestParams.Add("api_key=" + DataStore.APIKey);
        requestParams.Add("signature=" + signature);

        var requestURL = RequestHelpers.GetRequestURL();

        return RequestHelpers.DoAPIPOSTRequestAsync<PreapprovalResponse>(requestParams, requestURL + "preapproval");
    }

    private static void Validate(PreapprovalRequest request)
    {
        if (string.IsNullOrEmpty(request.CustomerFirstName))
            throw new ArgumentException("CustomerFirstName cannot be empty");
        if (string.IsNullOrEmpty(request.CustomerLastName))
            throw new ArgumentException("CustomerLastName cannot be empty");
        if (string.IsNullOrEmpty(request.CustomerAddress1))
            throw new ArgumentException("CustomerAddress1 cannot be empty");
        if (string.IsNullOrEmpty(request.CustomerPostcode))
            throw new ArgumentException("CustomerPostcode cannot be empty");
    }
}
