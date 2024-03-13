namespace PaymentAssistSDK;

internal static class DataStore
{
    public static string APIKey = "";
    public static string APISecret = "";
    public static string APIURL 
    {
        get => _apiURL.Length == 0 || _apiURL.Last() == '/' ? _apiURL : _apiURL + "/";
        set => _apiURL = value;
    }

    private static string _apiURL = "";
}