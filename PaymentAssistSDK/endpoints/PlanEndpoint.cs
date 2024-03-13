namespace PaymentAssistSDK;

internal static class PlanEndpoint
{
    public static PlanResponse Fetch(PlanRequest request)
    {
        Validate(request);

        var requestParams = new List<string>
        {
            "amount=" + request.Amount,
            "plan_id=" + request.PlanID
        };

        requestParams = RequestHelpers.RemoveEmptyParams(requestParams);

        var signature = RequestHelpers.GenerateSignature(requestParams, DataStore.APISecret);

        requestParams.Add("api_key=" + DataStore.APIKey);
        requestParams.Add("signature=" + signature);

        var requestURL = RequestHelpers.GetRequestURL();

        return RequestHelpers.DoAPIPOSTRequest<PlanResponse>(requestParams, requestURL + "plan");
	}

    private static void Validate(PlanRequest request)
    {
        if (request.Amount <= 0)
            throw new ArgumentException("Amount must be greater than 0");
    }
}
