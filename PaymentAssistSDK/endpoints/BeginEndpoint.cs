using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("PaymentAssistSDK.Tests")]

namespace PaymentAssistSDK;

internal static class BeginEndpoint
{
    public static Task<BeginResponse> Fetch(BeginRequest request)
    {
        request = ApplyDefaults(request);

        Validate(request);

        var requestParams = new List<string> {
            "addr1=" + request.CustomerAddress1,
            "addr2=" + request.CustomerAddress2,
            "addr3=" + request.CustomerAddress3,
            "amount=" + request.Amount,
            "auto_capture=" + request.EnableAutoCapture,
            "county=" + request.CustomerCounty,
            "description=" + request.Description,
            "email=" + request.CustomerEmail,
            "expiry=" + request.Expiry,
            "f_name=" + request.CustomerFirstName,
            "failure_url=" + request.FailureURL,
            "multi_plan=" + request.EnableMultiPlan,
            "order_id=" + request.OrderID,
            "plan_id=" + request.PlanID,
            "postcode=" + request.CustomerPostcode,
            "qr_code=" + request.ReturnQRCode,
            "reg_no=" + request.VehicleLicencePlate,
            "s_name=" + request.CustomerLastName,
            "send_email=" + request.SendEmail,
            "send_sms=" + request.SendSMS,
            "success_url=" + request.SuccessURL,
            "telephone=" + request.CustomerTelephone,
            "town=" + request.CustomerTown,
            "webhook_url=" + request.WebhookURL,
	    };

	    requestParams = RequestHelpers.RemoveEmptyParams(requestParams);
	    var signature = RequestHelpers.GenerateSignature(requestParams, DataStore.APISecret);

        requestParams.Add("api_key=" + DataStore.APIKey);
        requestParams.Add("signature=" + signature);

        return RequestHelpers.DoAPIPOSTRequestAsync<BeginResponse>(requestParams, "begin");
    }

    private static void Validate(BeginRequest request)
    {
        if (string.IsNullOrEmpty(request.OrderID))
            throw new ArgumentException("OrderID cannot be empty");

        if (request.Amount <= 0)
            throw new ArgumentException("Amount must be greater than 0");

        if (string.IsNullOrEmpty(request.CustomerFirstName))
            throw new ArgumentException("CustomerFirstName cannot be empty");

        if (string.IsNullOrEmpty(request.CustomerLastName))
            throw new ArgumentException("CustomerLastName cannot be empty");
        
        if (string.IsNullOrEmpty(request.CustomerAddress1))
            throw new ArgumentException("CustomerAddress1 cannot be empty");

        if (string.IsNullOrEmpty(request.CustomerPostcode))
            throw new ArgumentException("CustomerPostcode cannot be empty");
        
        if (request.SendEmail == true && string.IsNullOrEmpty(request.CustomerEmail))
            throw new ArgumentException("CustomerEmail cannot be empty if SendEmail is true");
        
        if (request.SendSMS == true && string.IsNullOrEmpty(request.CustomerTelephone))
            throw new ArgumentException("CustomerTelephone cannot be empty if SendSMS is true");
    }

    private static BeginRequest ApplyDefaults(BeginRequest request)
    {
        request.SendEmail ??= false;
        request.SendSMS ??= false;
        request.EnableMultiPlan ??= false;
        request.ReturnQRCode ??= false;
        request.EnableAutoCapture ??= true;

        return request;
    }
}
