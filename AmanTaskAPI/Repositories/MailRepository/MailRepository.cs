using AmanTaskAPI.Models;
using AmanTaskAPI.Repository.BaseRepository;

namespace AmanTaskAPI.Repositories.MailRepository
{
    public class MailRepository : BaseRepository<Mail>, IMailRepository
    {
        #region Fileds

        #endregion

        #region Constructors
        public MailRepository(AmanTaskContext amanTaskcontext) : base(amanTaskcontext)
        {
        }
        #endregion

        #region Methods

        #endregion
    }
}
