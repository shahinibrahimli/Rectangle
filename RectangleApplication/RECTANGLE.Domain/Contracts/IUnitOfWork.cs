using System;
using System.Threading.Tasks;
using Rectangle.Domain.Entities;

namespace Rectangle.Domain.Contracts
{
    public interface IUnitOfWork : IDisposable
    {  
        public IRectangleRepository RectangleRepository { get; } 

        Task SaveChangesAsync();
    }
}
