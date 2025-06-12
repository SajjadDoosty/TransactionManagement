using Application.Aggregates.Categories;
using Application.Aggregates.Transactions.ViewModels;
using Domain.Aggregates.Transactions;
using Domain.Aggregates.Transactions.Enums;
using Framework.DataType;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Resources.Messages;

namespace Application.Aggregates.Transactions;

public class TransactionApplication
    (DatabaseContext database, UnitOfWork unitOfWork,
    CategoryApplication categoryApplication)
{
    public async Task<ResultContract> CreateAsync(CreateViewModel viewModel)
    {
        var result =
            Transaction.Create(viewModel.Amount, viewModel.Type, viewModel.UserId, viewModel.CategoryId);

        var newTransaction =
            result.Data;

        if (result.IsSuccessful == false || newTransaction == null)
        {
            return result;
        }

        await database.Transactions.AddAsync(newTransaction);
        await unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<ResultContract<ResultViewModel>> GetAllAsync(Guid userId)
    {
        var transactions =
            await database.Transactions
            .Where(x => x.UserId == userId)
            .ToListAsync();

        var transactionVewModels =
            transactions.Adapt<List<IndexViewModel>>();

        return new ResultViewModel()
        {
            Transactions = transactionVewModels
        };
    }

    public async Task<ResultContract<List<IndexViewModel>>> GetByCategoryIdAsync(Guid categoryId)
    {
        var transactions =
            await database.Transactions
            .Where(x => x.CategoryId == categoryId)
            .ToListAsync();

        if (transactions == null)
        {
            return (ErrorType.NotFound, Errors.NotFound);
        }

        return transactions.Adapt<List<IndexViewModel>>();
    }

    public async Task<ResultContract<List<CategoryResultViewModel>>> GetAllGroupByCategoryAsync(Guid userId)
    {
        List<CategoryResultViewModel> result = new();

        var categories = (await categoryApplication.GetAllAsync(userId)).Data;

        if (categories == null)
        {
            return result;
        }

        foreach (var category in categories)
        {
            var transactions = (await GetByCategoryIdAsync(category.Id)).Data;

            result.Add(new CategoryResultViewModel()
            {
                Transactions = transactions,
                Category = category,
            });
        }

        return result;
    }
}
