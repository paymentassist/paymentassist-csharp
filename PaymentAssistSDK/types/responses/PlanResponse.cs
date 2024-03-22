using System.Text.Json.Serialization;

namespace PaymentAssistSDK;

/// <summary>
/// Contains the data returned by a successful call to the "plan" endpoint.
/// </summary>
public class PlanResponse
{
    /// <summary>
    /// The name of this plan.
    /// </summary>
    [JsonPropertyName("plan")]
    public string PlanName { get; set; } = "";

    /// <summary>
    /// The amount you requested, in pence.
    /// </summary>
    [JsonPropertyName("amount")]
    public int Amount { get; set; }

    /// <summary>
    /// The amount of interest payable, in pence.
    /// </summary>
    [JsonPropertyName("interest")]
    public int Interest { get; set; }

    /// <summary>
    /// The total amount that would be repayable under this plan, in pence.
    /// </summary>
    [JsonPropertyName("repayable")]
    public int TotalRepayable { get; set; }

    /// <summary>
    /// A breakdown of what the repayments would look like under this plan.
    /// </summary>
    [JsonPropertyName("schedule")]
    public List<Repayment> PaymentSchedule { get; set; } = new();
}