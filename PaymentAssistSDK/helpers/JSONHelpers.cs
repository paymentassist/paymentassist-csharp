using System.Text.Json;
using System.Text.Json.Serialization;

namespace PaymentAssistSDK;

internal static class JSONHelpers
{
    private static JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
        NumberHandling = JsonNumberHandling.AllowReadingFromString,
    };

    public static T Deserialize<T>(string json)
    {
        var result = JsonSerializer.Deserialize<T>(json, _jsonOptions);

        if (result == null)
            throw new InvalidOperationException("failed to deserialize object: "+json);

        return result;
    }
}