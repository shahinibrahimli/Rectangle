namespace Rectangle.Domain.Contracts;

public interface IUnitOfWorkInstanceBuilder
{
    IUnitOfWork BuildNewUnitOfWork();
}