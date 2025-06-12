using Application.Aggregates.Users.ViewModels;
using Domain.Aggregates.Users;
using Framework.DataType;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Resources.Messages;

namespace Application.Aggregates.Users;

public class UserApplication
    (DatabaseContext database, UnitOfWork unitOfWork)
{
    public async Task<ResultContract> CreateAsync(CreateViewModel viewModel)
    {
        var isExist = await database.Users.AnyAsync
            (x => x.Username == viewModel.Username || x.Email == viewModel.Email && !string.IsNullOrWhiteSpace(x.Email));

        if (isExist)
        {
            var message =
                string.Format(Errors.AlreadyExists, Resources.DataDictionary.UserName);

            return (ErrorType.DuplicateRecord, message);
        }

        var result =
            User.Create(viewModel.Username, viewModel.Password, viewModel.Email);

        var newUser =
            result.Data;

        if (result.IsSuccessful == false || newUser == null)
        {
            return result;
        }

        newUser.SetUserRole(viewModel.Role);

        await database.Users.AddAsync(newUser);
        await unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<ResultContract<List<IndexViewModel>>> GetAllAsync()
    {
        var users =
            await database.Users
            .OrderByDescending(x => x.InsertDateTime)
            .ToListAsync();

        return users.Adapt<List<IndexViewModel>>();
    }

    public async Task<ResultContract<IndexViewModel>> GetAsync(Guid userId)
    {
        var user =
            await database.Users.FirstOrDefaultAsync(x => x.Id == userId);

        if (user == null)
        {
            return (ErrorType.NotFound, Errors.NotFound);
        }

        return user.Adapt<IndexViewModel>();
    }

    public async Task<ResultContract<IndexViewModel>> GetByUsernameAsync(string username)
    {
        var user =
            await database.Users.FirstOrDefaultAsync(x => x.Username == username);

        if (user == null)
        {
            return (ErrorType.NotFound, Errors.NotFound);
        }

        return user.Adapt<IndexViewModel>();
    }

    public async Task<ResultContract> DeleteAsync(Guid id)
    {
        var user =
            await database.Users.FirstOrDefaultAsync(x => x.Id == id);

        if (user == null)
        {
            return (ErrorType.NotFound, Errors.NotFound);
        }

        database.Users.Remove(user);
        await unitOfWork.SaveChangesAsync();

        return true;
    }
}
