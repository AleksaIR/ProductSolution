using System.Threading.Tasks;

namespace ProductModels
{
    public interface IUnitOfWork
    {
        IProductRepository Products { get; }
        Task SaveChangesAsync();
    }
}