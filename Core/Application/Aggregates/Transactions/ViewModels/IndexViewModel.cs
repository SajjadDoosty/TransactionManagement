using Resources;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Application.Aggregates.Transactions.ViewModels;

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
