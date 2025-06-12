using System.ComponentModel.DataAnnotations;
using Domain.Aggregates.Transactions.Enums;
using Resources;

namespace Application.Aggregates.Transactions.ViewModels;

public class CreateViewModel
{
    public CreateViewModel()
    {
    }


    // **********
    [Display
        (Name = nameof(DataDictionary.Amount),
        ResourceType = typeof(DataDictionary))]
    [Required
        (AllowEmptyStrings = false,
        ErrorMessageResourceType = typeof(Resources.Messages.Validations),
        ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
    [Range
        (minimum: (double)Constants.MinLength.Amount,
        maximum: (double)Constants.MaxLength.Amount,
        ErrorMessageResourceType = typeof(Resources.Messages.Validations),
        ErrorMessageResourceName = nameof(Resources.Messages.Validations.Range))]

    public decimal Amount { get; set; }
    // **********

    // **********
    [Display
        (Name = nameof(DataDictionary.TransactionType),
        ResourceType = typeof(DataDictionary))]
    [Required
        (AllowEmptyStrings = false,
        ErrorMessageResourceType = typeof(Resources.Messages.Validations),
        ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

    public TransactionType Type { get; set; }
    // **********

    // **********
    [Required
        (AllowEmptyStrings = false,
        ErrorMessageResourceType = typeof(Resources.Messages.Validations),
        ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

    public Guid UserId { get; set; }
    // **********

    // **********
    [Required
        (AllowEmptyStrings = false,
        ErrorMessageResourceType = typeof(Resources.Messages.Validations),
        ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

    public Guid CategoryId { get; set; }
    // **********
}
