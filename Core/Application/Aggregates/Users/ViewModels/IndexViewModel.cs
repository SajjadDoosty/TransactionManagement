using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Resources;

namespace Application.Aggregates.Users.ViewModels;

public class IndexViewModel : UpdateViewModel
{
    public IndexViewModel()
    {
    }


    // **********
    [Display
        (Name = nameof(DataDictionary.InsertDateTime),
        ResourceType = typeof(DataDictionary))]

    public long InsertDateTime { get; set; }
    // **********

    // **********
    [Display
        (Name = nameof(DataDictionary.InsertDateTime),
        ResourceType = typeof(DataDictionary))]

    public string InsertDateTimeFa
    {
        get
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(InsertDateTime)
                .ToString("yyyy/MM/dd HH:ss", new CultureInfo("fa-IR"));
        }
    }
    // **********
}
