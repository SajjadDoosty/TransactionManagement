using BuildingBlocks.Domain.Data;

namespace Persistence;

public class UnitOfWork(DatabaseContext database) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken? cancellationToken = null)
    {
        if (cancellationToken.HasValue)
            return database.SaveChangesAsync(cancellationToken.Value);
        else
            return database.SaveChangesAsync();
    }

    public async Task ClearDataAsync()
    {
        await database.Database.EnsureDeletedAsync();
        await database.Database.EnsureCreatedAsync();
    }
}