using System.ComponentModel.DataAnnotations.Schema;
using BuildingBlocks.Domain.SeedWork;

namespace BuildingBlocks.Domain.Aggregates;

public class Entity : IEntity
{
	public Entity()
	{
		Id = Guid.NewGuid();

		SetInsertDateTime();
	}


	[DatabaseGenerated
	(DatabaseGeneratedOption.None)]
	public Guid Id { get; protected set; }

    public Guid InsertedBy { get; protected set; }
    public Guid UpdatedBy { get; protected set; }

    public long InsertDateTime { get; protected set; }
	public long UpdateDateTime { get; protected set; }


    public void SetInsertBy(Guid Id)
    {
        InsertedBy = Id;
    }

    public void SetUpdateBy(Guid Id)
    {
        UpdatedBy = Id;
    }

    public void SetInsertDateTime()
	{
		InsertDateTime =
			DateTimeUtility.GetCurrentUnixUTCTimeSeconds();
	}

    public void SetUpdateDateTime()
	{
		UpdateDateTime =
			DateTimeUtility.GetCurrentUnixUTCTimeSeconds();
	}
}