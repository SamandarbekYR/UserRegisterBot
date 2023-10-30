namespace UserRegisterBot.Entity;

public class Users
{
    public long Id { get; set; }
    public long ChatId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
}
