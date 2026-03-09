namespace Infrastructure.Persistence.EfCore.Entities;

public sealed class ContactRequestEntity
{
    public string Id { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string Message { get; set; } = null!;
    public bool MarkedAsRead { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
