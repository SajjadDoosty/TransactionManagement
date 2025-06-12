using Application.Aggregates.Categories.ViewModels;
using Domain.Aggregates.Categories;
using Framework.DataType;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Resources.Messages;

namespace Application.Aggregates.Categories;

public class CategoryApplication
    (DatabaseContext database, UnitOfWork unitOfWork)
{
    public async Task<ResultContract> CreateAsync(CreateViewModel viewModel)
    {
        var isExist = await database.Categories.AnyAsync
            (x => x.Title == viewModel.Title);

        if (isExist)
        {
            var message =
                string.Format(Errors.AlreadyExists, Resources.DataDictionary.Category);

            return (ErrorType.DuplicateRecord, message);
        }

        var result =
            Category.Create(viewModel.Title, viewModel.UserId);

        var newCategory =
            result.Data;

        if (result.IsSuccessful == false || newCategory == null)
        {
            return result;
        }

        if (viewModel.IsSystemic)
        {
            newCategory.MakeSystemic();
        }

        await database.Categories.AddAsync(newCategory);
        await unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<ResultContract<List<IndexViewModel>>> GetAllAsync(Guid userId)
    {
        var categories =
            await database.Categories
            .Where(x => x.UserId == userId)
            .ToListAsync();

        return categories.Adapt<List<IndexViewModel>>();
    }

    public async Task<ResultContract<Guid>> DeleteAsync(Guid id)
    {
        var category =
            await database.Categories.FirstOrDefaultAsync(x => x.Id == id);

        if (category == null)
        {
            return (ErrorType.NotFound, Errors.NotFound);
        }

        var userId = 
            category.UserId;

        database.Categories.Remove(category);
        await unitOfWork.SaveChangesAsync();

        return userId;
    }

    public async Task<ResultContract<IndexViewModel>> GetAsync(Guid categoryId)
    {
        var category =
            await database.Categories.FirstOrDefaultAsync(x => x.Id == categoryId);

        if (category == null)
        {
            return (ErrorType.NotFound, Errors.NotFound);
        }

        return category.Adapt<IndexViewModel>();
    }
}
