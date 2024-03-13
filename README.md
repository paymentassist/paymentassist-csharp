# This SDK is undergoing testing and has not been released!

# paymentassist-csharp

The official C# SDK for the Payment Assist Merchant API.

## Dependencies

.Net 6 and later

## Installation

???????????????????????

## Workflow

![Payment Assist API Workflow](https://raw.githubusercontent.com/paymentassist/paymentassist-php/master/api-workflow.png "API Workflow")

## Usage

The full API reference can be found here: https://api-docs.payment-assist.co.uk/.

To use this SDK, first initialise it by calling `PASDK.Initialise(...)`, which takes your API credentials as well as the PaymentAssist API URL you want to make requests to. You only need to call this once.

```
PASDK.Initialise("my_api_key", "my_api_secret", "https://api.demo.payassi.st/");
```

Note that it is not recommended to hard-code your API credentials like in the above example, this is just for illustration purposes.

After this, you can being to make the requests against the API via the static methods provided. Most requests take a single data class parameter and return a single response object. A nullable request field generally indicates that it is optional.

In the case of failure, the SDK will throw an error. Where the API refuses a request, or where there was an internal API error (e.g. any 4xx or 5xx response code), a `HttpRequestException` will be thrown. If you pass invalid request parameters, an `ArgumentException` will be thrown. Where the SDK encounters some irrecoverable error, an `InvalidOperationException` exception is thrown. Other exceptions are also possible, although the SDK does not intentionally throw these.

Note that `InvoiceRequest` and `CaptureRequest` may return a response without throwing even if the request was unsuccessful; specific error data for these is provided in the response.

Example:

```
request := pasdk.AccountRequest{}
accountResponse, err := request.Fetch()

if err != nil {
    fmt.Println("There was an error: "+err.Error())
	return
}

// Print the dealer's display name.
fmt.Println(accountResponse.DisplayName)
```

The following actions are available:

| Action | Description |
|--------|-------------|
| __AccountRequest__ | Returns information about the dealer's account. |
| __PlanRequest__ | Returns what the repayments would look like under a hypothetical repayment plan. |
| __PreapprovalRequest__ | Checks whether a customer would pass the basic pre-approval checks. |
| __BeginRequest__ | Begins an application. |
| __StatusRequest__ | Returns information about an ongoing application. |
| __UpdateRequest__ | Updates an existing application. |
| __CaptureRequest__ | Finalises an application that's in pending_capture state (used only when auto-capture is disabled). |
| __InvoiceRequest__ | Uploads an invoice for a completed application. |

## Notes

As virtually all requests to the API should return immediately (apart from /capture, which can take a few seconds to process deposits), there is currently no support for cancelling an ongoing request. There is a hard-coded timeout of 30 seconds per request which should be sufficient in all scenarios.

## Support

For technical support, please email [itsupport@payment-assist.co.uk](mailto:itsupport@payment-assist.co.uk).

If you encounter any issues or find that a particular part of the SDK isn't meeting your requirements, feel free to contact support and we will do our best to accommodate where we can.