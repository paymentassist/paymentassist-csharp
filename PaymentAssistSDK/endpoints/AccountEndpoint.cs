namespace PaymentAssistSDK;

internal static class AccountEndpoint
{
    public static Task<AccountResponse> Fetch()
    {
        var signature = RequestHelpers.GenerateSignature(new List<string>(), DataStore.APISecret);
            
        var requestParams = new List<string> {
            "api_key=" + DataStore.APIKey,
            "signature=" + signature
        };

        return RequestHelpers.DoAPIGETRequestAsync<AccountResponse>(requestParams, "account");
    }
}
