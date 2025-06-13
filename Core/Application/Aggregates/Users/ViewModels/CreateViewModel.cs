using System.ComponentModel.DataAnnotations;
using Domain.Aggregates.Users.Enums;
using Resources;

namespace Application.Aggregates.Users.ViewModels;

public class CreateViewModel
{
    public CreateViewModel()
    {
    }


    // **********
    [Display
        (Name = nameof(DataDictionary.UserName),
        ResourceType = typeof(DataDictionary))]
    [Required
        (AllowEmptyStrings = false,
        ErrorMessageResourceType = typeof(Resources.Messages.Validations),
        ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
    [RegularExpression
        (Constants.RegularExpression.Username,
        ErrorMessageResourceType = typeof(Resources.Messages.Validations),
        ErrorMessageResourceName = nameof(Resources.Messages.Validations.Username))]

    public string Username { get; set; }
    // **********

    // **********
    [Display
        (Name = nameof(DataDictionary.Password),
        ResourceType = typeof(DataDictionary))]
    [Required
        (AllowEmptyStrings = false,
        ErrorMessageResourceType = typeof(Resources.Messages.Validations),
        ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
    [MaxLength
        (length: Constants.MaxLength.Password,
        ErrorMessageResourceType = typeof(Resources.Messages.Validations),
        ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]
    [RegularExpression
        (pattern: Constants.RegularExpression.Password,
        ErrorMessageResourceType = typeof(Resources.Messages.Validations),
        ErrorMessageResourceName = nameof(Resources.Messages.Validations.Password))]
    //[DataType
    //    (dataType: DataType.Password)]

    public string Password { get; set; }
    // **********

    // **********
    [Display
        (Name = nameof(DataDictionary.Email),
        ResourceType = typeof(DataDictionary))]
    [MaxLength
        (length: Constants.MaxLength.EmailAddress,
        ErrorMessageResourceType = typeof(Resources.Messages.Validations),
        ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]
    [RegularExpression
        (pattern: Constants.RegularExpression.EmailAddress,
        ErrorMessageResourceType = typeof(Resources.Messages.Validations),
        ErrorMessageResourceName = nameof(Resources.Messages.Validations.EmailAddress))]
    //[DataType
    //    (dataType: DataType.EmailAddress)]

    public string? Email { get; set; }
    // **********

    // **********
    [Display
        (Name = nameof(DataDictionary.Role),
        ResourceType = typeof(DataDictionary))]

    public RoleType Role { get; set; }
    // **********

}
