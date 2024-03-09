namespace AmanTaskAPI.Repository.BaseRepository
{
    public interface IBaseRepository<T> where T : class
    {
        #region Get

        #region Get All
        /// <summary>
        /// Get All Entities
        /// </summary>
        /// <returns>IEnumerable (List) of Entities </returns>
        Task<IEnumerable<T>> GetAll();
        #endregion

        #region Get By Id
        /// <summary>
        /// Get one Entitiy by Id
        /// </summary>
        /// <returns> entity </returns>
        Task<T> GetById(int id);
        #endregion

        #endregion

        #region Add
        /// <summary>
        /// Add Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task Add(T entity);
        #endregion

        #region Update
        /// <summary>
        /// Update Entity by check id = entity.id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task Update(int id, T entity);
        #endregion

        #region Delete
        /// <summary>
        /// Delete Entity by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteById(int id);
        #endregion

    }
}
