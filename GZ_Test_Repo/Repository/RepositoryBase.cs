using GZ_Test_Repo.Context;
using Microsoft.EntityFrameworkCore;

namespace GZ_Test_Repo.Repository
{
    public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly GZ_DbContext _context;

        public RepositoryBase(GZ_DbContext context)
        {
            _context = context;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task CreateAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
