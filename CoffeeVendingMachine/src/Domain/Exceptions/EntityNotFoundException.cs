namespace CoffeeVendingMachine.Domain.Exceptions;
public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(int id)
        : base($"Entity with id \"{id}\" is not found.")
    {
    }
}
