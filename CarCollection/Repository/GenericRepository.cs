using CarCollection.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace CarCollection.Repository
    {
    public class GenericRepository<T> : IGenericRepository<T> where T : class
        {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext applicationDbContext)
            {
            _context = applicationDbContext;
            }


        public async Task<T> CreateAsync(T entity)
            {
            _context.Update(entity);
            _ = await _context.SaveChangesAsync();
            return entity;
            }

        public async Task DeleteAsync(T entity)
            {
            _context.Set<T>().Remove(entity);
            _ = await _context.SaveChangesAsync();
            }

        public async Task<IEnumerable<T>> GetAllAsync()
            {
            return await _context.Set<T>().ToListAsync();
            }

        public async Task<T> GetAsync(int? id)
            {
            return await _context.Set<T>().FindAsync(id);
            }

        public async Task<T> PutAsync(T entity)
            {
            _context.Update(entity);
            _ = await _context.SaveChangesAsync();
            return entity;
            }

        public async Task<T> UpdateAsync(T entity)
            {
            _context.Update(entity);
            _ = await _context.SaveChangesAsync();
            return entity;
            }
        /// <summary>
        /// If entity not equal to null return true, else return false
        /// </summary>
        /// <param name="id">id as input</param>
        /// <returns>returns boolean value of true if not equal to null</returns>
        public async Task<bool> Exist(int? id)
            {
            var entity = await GetAsync(id);
            return entity != null;
            }
        }
    }
