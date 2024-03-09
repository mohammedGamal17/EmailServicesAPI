using AmanTaskAPI.Models;
using AmanTaskAPI.Repository.BaseRepository;


namespace AmanTaskAPI.Repositories.ReceiverRepository
{
    public interface IReceiverRepository : IBaseRepository<Receiver>
    {
        public int GetReceiverIdByEmail(string email);
        public string GetReceiverEmailById(int id);
    }
}
