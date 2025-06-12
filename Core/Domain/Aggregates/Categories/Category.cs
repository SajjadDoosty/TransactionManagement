using BuildingBlocks.Domain.Aggregates;
using BuildingBlocks.Domain.SeedWork;
using Domain.Aggregates.Users;
using Framework.DataType;

namespace Domain.Aggregates.Categories;

public class Category :
    Entity, IEntityHasIsSystemic
{
    public Category()
    {
        // FOR EF!
    }


    public string Title { get; private set; }
    public bool IsSystemic { get; private set; }
    public Guid? UserId { get; private set; }
    public User? User { get; private set; }

    private Category(string title, Guid userId)
    {
        Title = title;
        UserId = userId;

        SetInsertDateTime();
    }

    public static ResultContract<Category> Create
        (string title, Guid userId)
    {
        title = title.Fix()!;

        var category =
            new Category(title, userId);

        return category;
    }

    public ResultContract Update(string title)
    {
        Title = title;

        return true;
    }


    public void MakeSystemic()
    {
        IsSystemic = true;
    }
}
