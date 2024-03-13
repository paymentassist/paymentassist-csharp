namespace PaymentAssistSDK;

public struct PlanRequest
{
    /// <summary>
    /// The invoice amount in pence.
    /// </summary>
    public int Amount;

    /// <summary>
    /// The plan ID. If empty, the account's default plan is used.
    /// </summary>
    public int? PlanID;
}