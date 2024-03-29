﻿namespace PaymentAssistSDK;

internal static class InvoiceEndpoint
{
    public static Task<InvoiceResponse> Fetch(InvoiceRequest request)
    {
        Validate(request);

        var requestParams = new List<string>
        {
            "filedata=" + Convert.ToBase64String(request.FileData),
            "filetype=" + request.FileType,
            "token=" + request.ApplicationToken
        };

        requestParams = RequestHelpers.RemoveEmptyParams(requestParams);
        var signature = RequestHelpers.GenerateSignature(requestParams, DataStore.APISecret);

        requestParams.Add("api_key=" + DataStore.APIKey);
        requestParams.Add("signature=" + signature);

        return RequestHelpers.DoAPIPOSTRequestAsync<InvoiceResponse>(requestParams, "invoice");
    }

    private static void Validate(InvoiceRequest request)
    {
        if (string.IsNullOrEmpty(request.ApplicationToken))
            throw new ArgumentException("ApplicationToken cannot be empty");
        if (string.IsNullOrEmpty(request.FileType))
            throw new ArgumentException("FileType cannot be empty");
        if (request.FileData == null || request.FileData.Length == 0)
            throw new ArgumentException("FileData cannot be empty");
    }
}
