namespace Application.Dtos.Accounts;

public class UpsertAccountDto
{
    public Guid? UserId { get; set; }
    public string Name { get; set; } = null!;
    public decimal Balance { get; set; }
    public string Currency { get; set; } = "PHP";
}
