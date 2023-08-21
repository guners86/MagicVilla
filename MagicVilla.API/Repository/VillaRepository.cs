using MagicVilla.API.Data;
using MagicVilla.API.Models;
using MagicVilla.API.Repository.Services;

namespace MagicVilla.API.Repository
{
    public class VillaRepository : Repository<Villa>, IVillaRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public VillaRepository(ApplicationDBContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<Villa> UpdateAsync(Villa entity)
        {
            entity.UpdateDate = DateTime.Now;
            _dbContext.Villas.Update(entity);
            await SaveChangesAsync();
            return entity;
        }
    }
}
