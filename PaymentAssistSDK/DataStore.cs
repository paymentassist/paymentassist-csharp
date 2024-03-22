namespace PaymentAssistSDK;

internal static class DataStore
{
    public static string APIKey = "";
    public static string APISecret = "";
    public static string APIURL 
    {
        get => _apiURL;
        set 
        {
            if (string.IsNullOrEmpty(value))
                _apiURL = "";
            else
                _apiURL = value.Last() == '/' ? value : value + "/";
        }
    }

    private static string _apiURL = "";
}