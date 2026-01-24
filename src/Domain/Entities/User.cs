using Core.Security.Abstractions.Enums;

namespace Domain.Entities;

public class User : Core.Security.Abstractions.Entities.User<Guid>
{
    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; } = null!;
    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = null!;

    public User()
    {
    }

    public User(string firstName, string lastName, string email, byte[] passwordSalt, byte[] passwordHash,
        AuthenticatorType authenticatorType) : this()
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordSalt = passwordSalt;
        PasswordHash = passwordHash;
        AuthenticatorType = authenticatorType;
    }
    
    public static User Create(string firstName, string lastName, string email, byte[] passwordHash, byte[] passwordSalt)
    {
        if (string.IsNullOrWhiteSpace(firstName)) 
            throw new ArgumentException("First name cannot be empty.", nameof(firstName));
        
        if (string.IsNullOrWhiteSpace(lastName)) 
            throw new ArgumentException("Last name cannot be empty.", nameof(lastName));
        
        if (string.IsNullOrWhiteSpace(email)) 
            throw new ArgumentException("Email cannot be empty.", nameof(email));

        var user = new User(
            firstName: firstName,
            lastName: lastName,
            email: email,
            passwordSalt: passwordSalt,
            passwordHash: passwordHash,
            authenticatorType: AuthenticatorType.None
        );

        return user;
    }

    public void Delete()
    {}
    
    public void Update(string firstName, string lastName, string email)
    {
        if (string.IsNullOrWhiteSpace(firstName)) 
            throw new ArgumentException("First name cannot be empty.", nameof(firstName));
        if (string.IsNullOrWhiteSpace(lastName)) 
            throw new ArgumentException("Last name cannot be empty.", nameof(lastName));
        if (string.IsNullOrWhiteSpace(email)) 
            throw new ArgumentException("Email cannot be empty.", nameof(email));

        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }
}