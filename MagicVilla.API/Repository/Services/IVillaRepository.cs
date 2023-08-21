using MagicVilla.API.Models;

namespace MagicVilla.API.Repository.Services
{
    public interface IVillaRepository : IRepository<Villa> 
    {
        Task<Villa> UpdateAsync(Villa entity);
    }
}
