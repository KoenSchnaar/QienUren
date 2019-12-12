using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrenRegistratieQien.Models;
using UrenRegistratieQien.Repositories;

namespace UrenRegistratieQienTest.Fakes
{
    class FakeClientRepository : IClientRepository
    {
        public Task AddNewClient(ClientModel clientModel)
        {
            throw new NotImplementedException();
        }

        public Task DeleteClient(int clientId)
        {
            throw new NotImplementedException();
        }

        public Task EditAClient(ClientModel clientModel)
        {
            throw new NotImplementedException();
        }

        public Task<List<ClientModel>> GetAllClients()
        {
            throw new NotImplementedException();
        }

        public Task<ClientModel> GetClient(int clientId)
        {
            throw new NotImplementedException();
        }

        public Task<ClientModel> GetClientByUserId(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
