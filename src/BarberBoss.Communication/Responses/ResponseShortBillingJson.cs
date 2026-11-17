namespace BarberBoss.Communication.Responses;

public class ResponseShortBillingJson
{
    public long Id { get; set; }

    public string BarberName { get; set; } = string.Empty;

    public decimal Amount { get; set; }
}