using AmanTaskAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AmanTaskAPI.Repository.BaseRepository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        #region Fileds
        protected readonly AmanTaskContext _context;
        protected readonly DbSet<T> _dbSet;
        #endregion

        #region Constructors
        public BaseRepository(AmanTaskContext amanTaskcontext)
        {
            _context = amanTaskcontext;
            _dbSet = amanTaskcontext.Set<T>();
        }
        #endregion

        #region Methods

        #region Get
        public virtual async Task<IEnumerable<T>> GetAll() => await _dbSet.ToListAsync();

        public virtual async Task<T> GetById(int id) => await _dbSet.FindAsync(id);
        #endregion

        #region Add
        public virtual async Task Add(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Update
        public Task Update(int id, T entity)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Delete
        public virtual async Task DeleteById(int id)
        {
            var entity = await GetById(id);
            if (entity == null)
                return;

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
        #endregion

        #endregion

    }
}
