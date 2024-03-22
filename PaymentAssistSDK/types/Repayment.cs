using System.Text.Json.Serialization;

namespace PaymentAssistSDK;

public struct Repayment
{
    /// <summary>
    /// The due date of this repayment.
    /// </summary>
    [JsonPropertyName("date")]
    public DateTime Date { get; set; }

    /// <summary>
    /// The amount of this repayment, in pence.
    /// </summary>
    [JsonPropertyName("amount")]
    public int Amount { get; set; }
}