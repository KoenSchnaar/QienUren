using System.Collections.Generic;
using UrenRegistratieQien.Models;

namespace UrenRegistratieQien.Repositories
{
    public interface IClientRepository
    {
        List<ClientModel> GetAllClients();
        void AddNewClient(ClientModel clientModel);
        void EditAClient(ClientModel clientModel);
    }
}