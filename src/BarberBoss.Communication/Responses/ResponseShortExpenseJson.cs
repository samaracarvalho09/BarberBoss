namespace BarberBoss.Communication.Responses;

public class ResponseShortExpenseJson
{
    public long Id { get; set; }

    public string BarberName { get; set; } = string.Empty;

    public decimal Amount { get; set; }
}