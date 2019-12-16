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
            return null;
        }

        public Task DeleteClient(int clientId)
        {
            throw new NotImplementedException();
        }

        public Task EditAClient(ClientModel clientModel)
        {
            return null;
        }

        public Task<List<ClientModel>> GetAllClients()
        {
            return Task.FromResult(new List<ClientModel>());
        }

        public Task<ClientModel> GetClient(int clientId)
        {
            return Task.FromResult(new ClientModel());
        }

        public Task<ClientModel> GetClientByUserId(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
