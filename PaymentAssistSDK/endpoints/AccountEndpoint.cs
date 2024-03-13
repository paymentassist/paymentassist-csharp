namespace PaymentAssistSDK;

internal static class AccountEndpoint
{
    public static AccountResponse Fetch()
    {
        var signature = RequestHelpers.GenerateSignature(new List<string>(), DataStore.APISecret);
            
        var requestParams = new List<string> {
            "api_key=" + DataStore.APIKey,
            "signature=" + signature
        };

        var requestURL = RequestHelpers.GetRequestURL();

        return RequestHelpers.DoAPIGETRequest<AccountResponse>(requestParams, requestURL + "account");
    }
}
