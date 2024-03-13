namespace PaymentAssistSDK;

internal static class InvoiceEndpoint
{
    public static Task<InvoiceResponse> Fetch(InvoiceRequest request)
    {
        Validate(request);

        var requestParams = new List<string>
        {
            "filedata=" + Convert.ToBase64String(request.FileData),
            "filetype=" + request.FileType,
            "token=" + request.ApplicationID
        };

        requestParams = RequestHelpers.RemoveEmptyParams(requestParams);

        var signature = RequestHelpers.GenerateSignature(requestParams, DataStore.APISecret);

        requestParams.Add("api_key=" + DataStore.APIKey);
        requestParams.Add("signature=" + signature);

        var requestURL = RequestHelpers.GetRequestURL();

        return RequestHelpers.DoAPIPOSTRequestAsync<InvoiceResponse>(requestParams, requestURL + "invoice");
    }

    private static void Validate(InvoiceRequest request)
    {
        if (string.IsNullOrEmpty(request.ApplicationID))
            throw new ArgumentException("ApplicationID cannot be empty");
        if (string.IsNullOrEmpty(request.FileType))
            throw new ArgumentException("FileType cannot be empty");
        if (request.FileData == null || request.FileData.Length == 0)
            throw new ArgumentException("FileData cannot be empty");
    }
}
