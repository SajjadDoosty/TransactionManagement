using Resources;
using System.ComponentModel.DataAnnotations;

namespace Application.Aggregates.Categories.ViewModels;

public class CreateViewModel
{
    public CreateViewModel()
    {
    }

    // **********
    [Display
        (Name = nameof(DataDictionary.Title),
        ResourceType = typeof(DataDictionary))]
    [Required
        (AllowEmptyStrings = false,
        ErrorMessageResourceType = typeof(Resources.Messages.Validations),
        ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

    public string Title { get; set; }
    // **********

    // **********
    [Display
        (Name = nameof(DataDictionary.IsActive),
        ResourceType = typeof(DataDictionary))]
    [Required
        (AllowEmptyStrings = false,
        ErrorMessageResourceType = typeof(Resources.Messages.Validations),
        ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

    public bool IsSystemic { get; set; }
    // **********

    // **********
    public Guid UserId { get; set; }
    // **********

}
