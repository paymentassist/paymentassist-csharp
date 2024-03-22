using System.Text.Json.Serialization;

namespace PaymentAssistSDK;

/// <summary>
/// Represents the basic characteristics of a given repayment plan.
/// </summary>
public class Plan
{
    /// <summary>
    /// The ID of this plan.
    /// </summary>
    [JsonPropertyName("plan_id")]
    public int ID { get; set; }

    /// <summary>
    /// The name of this plan.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    /// <summary>
    /// The number of instalments in this plan.
    /// </summary>
    [JsonPropertyName("instalments")]
    public int Instalments { get; set; }

    /// <summary>
    /// Whether a deposit is required by this plan (first payment taken immediately).
    /// </summary>
    [JsonPropertyName("deposit")]
    public bool DepositRequired { get; set; }

    /// <summary>
    /// The annual percentage interest rate of this plan.
    /// </summary>
    [JsonPropertyName("apr")]
    public decimal APR { get; set; }

    /// <summary>
    /// How often repayments are made on this plan.
    /// </summary>
    [JsonPropertyName("frequency")]
    public string Frequency { get; set; } = "";

    /// <summary
    /// The minimum amount allowed under this plan in pence, if any.
    /// </summary>
    [JsonPropertyName("min_amount")]
    public int? MinAmount { get; set; }

    /// <summary
    /// The maximum amount allowed under this plan in pence, if any.
    /// </summary>
    [JsonPropertyName("max_amount")]
    public int? MaxAmount { get; set; }

    /// <summary>
    /// The Payment Assist commission rate charged under this plan as a percentage. 
    /// </summary>
    [JsonPropertyName("commission_rate")]
    public decimal CommissionRate { get; set; }

    /// <summary>
    /// The Payment Assist fixed commission fee charged under this plan in pence.
    /// </summary>
    [JsonPropertyName("commission_fixed_fee")]
    public int? CommissionFixedFee { get; set; }
}