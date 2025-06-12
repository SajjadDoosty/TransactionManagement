using BuildingBlocks.Domain.Aggregates;
using Domain.Aggregates.Categories;
using Domain.Aggregates.Transactions.Enums;
using Domain.Aggregates.Users;
using Framework.DataType;

namespace Domain.Aggregates.Transactions;

public class Transaction : Entity
{
    public Transaction()
    {
        // FOR EF!
    }


    public decimal Amount { get; private set; }
    public TransactionType Type { get; private set; }
    public Guid UserId { get; private set; }
    public Guid CategoryId { get; private set; }
    public User User { get; private set; }
    public Category Category { get; private set; }

    private Transaction
        (decimal amount, TransactionType type, Guid userId, Guid categoryId)
    {
        Amount = amount;
        Type = type;
        UserId = userId;
        CategoryId = categoryId;

        SetInsertDateTime();
    }

    public static ResultContract<Transaction> Create
        (decimal amount, TransactionType type, Guid userId, Guid categoryId)
    {
        var transaction =
            new Transaction(amount, type, userId, categoryId);

        return transaction;
    }
}
