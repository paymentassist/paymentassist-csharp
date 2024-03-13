using System.Security.Cryptography;

namespace PaymentAssistSDK;

internal static class TestHelpers
{
    public static bool TestsAreRunning = false;
    public static bool IntegrationTestsAreRunning = false;

    public static string GetRandomID()
    {
        byte[] randomBytes = new byte[10];
        
        using (RandomNumberGenerator generator = RandomNumberGenerator.Create())
        {
            generator.GetBytes(randomBytes);
        }
        
        return BitConverter.ToString(randomBytes)
            .Replace("-", "")
            .ToLower()[..10];
    }
}