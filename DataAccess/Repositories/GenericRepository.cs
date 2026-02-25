using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DataAccess.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        public GenericRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            IQueryable<T> query = _context.Set<T>();
            return await query.ToListAsync();
        }
        public async Task<T> GetByIDAsync(int id) {
            return await _context.Set<T>().FindAsync(id);
        }
        public async Task AddAsync(T entity) { 
            await _context.AddAsync<T>(entity);
            await SaveAsync();
        }
        public async Task UpdateAsync(T entity) { 
            _context.Entry<T>(entity).State = EntityState.Modified;
            await SaveAsync();
        }
        public async Task DeleteAsync(int id)
        {
            T entity = await _context.Set<T>().FindAsync(id);
            if (entity == null)
                throw new NullReferenceException("$Found no entity of id: {id}");

            _context.Remove<T>(entity);
            await SaveAsync();
        }
        public async Task SaveAsync() { await _context.SaveChangesAsync(); }
    }
}
