using Domain.Aggregates.Transactions.Enums;

namespace Application.Aggregates.Transactions.ViewModels;

public class ResultViewModel
{
    public ResultViewModel()
    {
    }


    public List<IndexViewModel> Transactions { get; set; }

    public decimal Total
    {
        get
        {
            var income =
                Transactions.Where(x => x.Type == TransactionType.INCOME).Sum(x => x.Amount);

            var expense =
                Transactions.Where(x => x.Type == TransactionType.EXPENSE).Sum(x => x.Amount);

            return income - expense;
        }
    }

    public decimal TotalIncome
    {
        get
        {
            var income =
                Transactions.Where(x => x.Type == TransactionType.INCOME).Sum(x => x.Amount);

            return income;
        }
    }

    public decimal TotalExpense
    {
        get
        {
            var expense =
                Transactions.Where(x => x.Type == TransactionType.EXPENSE).Sum(x => x.Amount);

            return expense;
        }
    }
}
