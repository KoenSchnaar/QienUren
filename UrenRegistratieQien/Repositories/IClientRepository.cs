using UrenRegistratieQien.Models;

namespace UrenRegistratieQien.Repositories
{
    public interface IClientRepository
    {
        ClientModel GetClient(int clientId);
    }
}