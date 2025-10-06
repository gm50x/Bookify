namespace Bookify.Domain.Abstractions;
public abstract class Entity(Guid id)
{
    private readonly List<IDomainEvent> _domainEvents = [];
    public Guid Id { get; init; } = id;

    public IReadOnlyList<IDomainEvent> GetDomainEvents => _domainEvents.AsReadOnly();

    protected void RaiseDomainEvent(IDomainEvent @event)
    {
        _domainEvents.Add(@event);
    }
}
