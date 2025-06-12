using BuildingBlocks.Domain.Aggregates;
using Domain.Aggregates.Categories;
using Domain.Aggregates.Transactions;
using Domain.Aggregates.Users.Enums;
using Framework.DataType;
using Resources.Messages;

namespace Domain.Aggregates.Users;

public class User : Entity
{
    public User()
    {
        // FOR EF!
    }


    public string Username { get; private set; }
    public string Password { get; private set; }
    public string? Email { get; private set; }
    public RoleType Role { get; private set; }

    public List<Category> Categories { get; set; }
    public List<Transaction> Transactions { get; set; }

    private User(string username, string password, RoleType role)
    {
        Username = username;
        Password = password;
        Role = role;

        SetInsertDateTime();
    }

    public static ResultContract<User> Create
        (string username, string password, string? email)
    {
        username = username.Fix()!;
        password = password.Fix()!;
        email = email.Fix()!;

        if (!email.IsValidEmail() && string.IsNullOrWhiteSpace(email) == false)
            return (ErrorType.InvalidFormat, Validations.EmailAddress);

        if (!password.IsValidPassword())
            return (ErrorType.InvalidFormat, Validations.Password);

        var user = new User(username, password, RoleType.USER)
        {
            Email = email,
        };

        return user;
    }

    public ResultContract Update
        (string username, string password, string? email)
    {
        username = username.Fix()!;
        password = password.Fix()!;
        email = email.Fix()!;

        if (!email.IsValidEmail())
            return (ErrorType.InvalidFormat, Validations.EmailAddress);

        if (!password.IsValidPassword())
            return (ErrorType.InvalidFormat, Validations.Password);

        Username = username.Fix()!;
        Password = password.Fix()!;
        Email = email.Fix()!;

        return true;
    }


    public void SetUserRole(RoleType roleType)
    {
        Role = roleType;
    }
}
