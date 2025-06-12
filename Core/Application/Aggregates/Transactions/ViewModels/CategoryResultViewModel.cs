namespace Application.Aggregates.Transactions.ViewModels;

public class CategoryResultViewModel : ResultViewModel
{
    public CategoryResultViewModel()
    {
    }


    public Categories.ViewModels.IndexViewModel Category { get; set; }
    
}
