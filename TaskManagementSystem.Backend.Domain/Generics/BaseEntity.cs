namespace TaskManagementSystem.Backend.Domain.Generics
{
    /// <summary>
    /// Represents a base entity from which all other entities must inherit from.
    /// </summary>
    public class BaseEntity
    {
        public Guid Id {  get; set; }
        public BaseEntity(Guid id)
        {
            this.Id = id;
        }
    }
}
