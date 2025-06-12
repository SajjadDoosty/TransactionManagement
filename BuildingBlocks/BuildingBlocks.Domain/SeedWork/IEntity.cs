namespace BuildingBlocks.Domain.SeedWork;

public interface IEntity : IEntityHasUpdateInfo

{
    public Guid Id { get; }
    public long InsertDateTime { get; }
    public Guid InsertedBy { get; }
    void SetInsertBy(Guid Id);
    void SetInsertDateTime();
}
