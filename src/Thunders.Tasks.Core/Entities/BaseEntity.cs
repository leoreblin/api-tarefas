namespace Thunders.Tasks.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; }

        protected BaseEntity() { }

        protected BaseEntity(Guid id) => Id = id;
    }
}
