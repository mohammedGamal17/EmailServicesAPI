using AmanTaskAPI.Models;
using AmanTaskAPI.Repository.BaseRepository;

namespace AmanTaskAPI.Repositories.ReceiverRepository
{
    public class ReceiverRepository : BaseRepository<Receiver>, IReceiverRepository
    {
        #region Fileds

        #endregion

        #region Constructors
        public ReceiverRepository(AmanTaskContext amanTaskcontext) : base(amanTaskcontext)
        {
        }
        #endregion

        #region Methods
        public int GetReceiverIdByEmail(string email)
        {
            Receiver? receiver = _context.Receivers.FirstOrDefault(r => r.Email == email);
            if (receiver == null) return -1;

            return receiver.Id;
        }


        public string GetReceiverEmailById(int id)
        {
            Receiver? receiver = _context.Receivers.FirstOrDefault(e => e.Id == id);

            return receiver.Email;
        }
        #endregion
    }
}
