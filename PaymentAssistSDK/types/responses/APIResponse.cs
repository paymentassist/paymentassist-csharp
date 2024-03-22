using System.Text.Json.Serialization;

namespace PaymentAssistSDK;

internal class APIResponse<T> where T : new()
{
    [JsonPropertyName("data")]
    public T? Data { get; set; }

    [JsonPropertyName("msg")]
    public string Message { get; set; } = "";

    [JsonPropertyName("status")]
    public string Status { get; set; } = "";
}