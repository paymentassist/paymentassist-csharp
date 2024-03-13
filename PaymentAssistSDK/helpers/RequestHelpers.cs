using System.Security.Cryptography;
using System.Text;

namespace PaymentAssistSDK;

internal static class RequestHelpers
{
    public static List<string> RemoveEmptyParams(List<string> requestParams)
    {
        return requestParams.Where(x => x.Split('=')[1] != "").ToList();
    }

    /// <summary>
    /// The keys of requestParams should already be in alphabetical order.
    /// </summary>
    public static string GenerateSignature(List<string> requestParams, string apiSecret)
    {
        requestParams = CapitaliseParamKeys(requestParams);
        var requestString = string.Join('&', requestParams);

        if (!string.IsNullOrEmpty(requestString))
		    requestString += "&";
        
        using (var hasher = new HMACSHA256(Encoding.UTF8.GetBytes(apiSecret)))
        {
            var hashBytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(requestString));
            return Convert.ToHexString(hashBytes).ToLower();
        }
	}

    private static List<string> CapitaliseParamKeys(List<string> requestParams)
    {
        return requestParams.Select(x =>
        {
            var parts = x.Split('=');
            return parts[0].ToUpper() + "=" + parts[1];
        }).ToList();
    }

    public static string GetRequestURL()
    {
        if (TestHelpers.TestsAreRunning && !TestHelpers.IntegrationTestsAreRunning)
            return "";

        if (!DataStore.APIURL.Contains("https:"))
            throw new ArgumentException("the API URL must contain the string \"https:\"");

        if (DataStore.APIURL.Last() != '/')
            return DataStore.APIURL + "/";

        return DataStore.APIURL;
    }

    /// <summary>
    /// Throw an error if our credentials are missing or invalid.
    /// </summary>
    private static void CheckCredentials()
    {
        if (string.IsNullOrEmpty(DataStore.APIKey))
            throw new ArgumentException("APIKey cannot be empty - call PASDK.Initialise to pass in your credentials");
        if (string.IsNullOrEmpty(DataStore.APISecret))
            throw new ArgumentException("APISecret cannot be empty - call PASDK.Initialise to pass in your credentials");
        if (string.IsNullOrEmpty(DataStore.APIURL) && !TestHelpers.TestsAreRunning)
            throw new ArgumentException("APIURL cannot be empty - call PASDK.Initialise to pass in the URL you want to send a request to");
        if (!DataStore.APIURL.Contains("https:") && !TestHelpers.TestsAreRunning)
            throw new ArgumentException("APIURL must contain the characters \"https:\"");
    }

    public static async Task<T> DoAPIPOSTRequestAsync<T>(List<string> formData, string endpoint) where T : new()
    {
        CheckCredentials();

        var formValues = new FormUrlEncodedContent(formData.Select(x =>
        {
            var parts = x.Split('=');
            return new KeyValuePair<string, string>(parts[0], parts[1]);
        }));

        if (TestHelpers.TestsAreRunning && !TestHelpers.IntegrationTestsAreRunning)
            return MockResponses.GetMockAPIResponse<T>(endpoint).Data;

        using (var client = new HttpClient())
        {
            client.Timeout = TimeSpan.FromSeconds(30);
            client.DefaultRequestHeaders.Add("X-Origin", "payment-assist-csharp-sdk");

            var response = await client.PostAsync(endpoint, formValues);
            var body = await response.Content.ReadAsStringAsync();

            if (body == null || body.Length == 0)
                throw new HttpRequestException("the API response was malformed - the body was empty");

            CheckStatusCode((int)response.StatusCode, body);

            var result = JSONHelpers.Deserialize<APIResponse<T>>(body);

            if (result.Status == "error")
                throw new HttpRequestException("the API refused your request: " + body);
            if (result.Status != "ok")
                throw new HttpRequestException("the API returned an unexpected response: " + body);
            if (result == null || result.Data == null)
                throw new InvalidOperationException("failed to deserialise response JSON: " + body);

            return result.Data;
        }
    } 

    public static async Task<T> DoAPIGETRequestAsync<T>(List<string> formData, string endpoint) where T : new()
    {
        CheckCredentials();

        endpoint += "?";

        foreach (var data in formData)
        {
            var parts = data.Split('=');
            endpoint += parts[0] + "=" + Uri.EscapeDataString(parts[1]) + "&";
        }

        endpoint = endpoint.Remove(endpoint.Length - 1);

        if (TestHelpers.TestsAreRunning && !TestHelpers.IntegrationTestsAreRunning)
            return MockResponses.GetMockAPIResponse<T>(endpoint).Data;

        using (var client = new HttpClient())
        {
            client.Timeout = TimeSpan.FromSeconds(30);
            client.DefaultRequestHeaders.Add("X-Origin", "payment-assist-csharp-sdk");

            var response = await client.GetAsync(endpoint);
            var body = await response.Content.ReadAsStringAsync();

            if (body.Length == 0)
                throw new HttpRequestException("the API response was malformed - the body was empty");

            CheckStatusCode((int)response.StatusCode, body);

            var result = JSONHelpers.Deserialize<APIResponse<T>>(body);

            if (result.Status == "error")
                throw new HttpRequestException("the API refused your request: " + body);
            if (result.Status != "ok")
                throw new HttpRequestException("the API returned an unexpected response: " + body);
            if (result == null || result.Data == null)
                throw new InvalidOperationException("failed to deserialise response JSON: " + body);

            return result.Data;
        }
    }

    /// <summary>
    /// Throw an error if the status code indicated failure.
    /// </summary>
    private static void CheckStatusCode(int statusCode, string body)
    {
	    if ((statusCode >= 0 && statusCode < 200)
            || (statusCode >= 300 && statusCode < 400)
            || (statusCode >= 500 && statusCode < 600))
        {
            throw new HttpRequestException("API request failed returning status code " + statusCode + ": " + body);
        }

        if (statusCode >= 400 && statusCode < 500) 
            throw new HttpRequestException("API refused your request returning status code " + statusCode + ": " + body);
    }
}