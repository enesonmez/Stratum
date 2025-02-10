namespace Domain.Entities;

public class User : Core.Security.Entities.User<Guid>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}