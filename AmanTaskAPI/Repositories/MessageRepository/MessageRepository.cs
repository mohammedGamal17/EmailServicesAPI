using AmanTaskAPI.Models;
using AmanTaskAPI.Repository.BaseRepository;
using Microsoft.EntityFrameworkCore;

namespace AmanTaskAPI.Repositories.MessageRepository
{
    public class MessageRepository : BaseRepository<Message>, IMessageRepository
    {
        #region Fileds

        #endregion

        #region Constructors
        public MessageRepository(AmanTaskContext amanTaskcontext) : base(amanTaskcontext)
        {
        }
        #endregion

        #region Methods

        #region Get All
        public override async Task<IEnumerable<Message>> GetAll()
        {
            return await _context.Messages.Where(message => message.IsDeleted != true).ToListAsync();
        }
        #endregion

        #region Soft Delete
        public override async Task DeleteById(int id)
        {
            Message? entity = await _context.Messages.FindAsync(id);
            if (entity == null)
                return;

            // Soft Delete
            entity.IsDeleted = true;

            await _context.SaveChangesAsync();
        }
        #endregion

        #endregion
    }
}
