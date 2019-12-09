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
        public void AddNewClient(ClientModel clientModel)
        {
            var implemented = true;
        }

        public void DeleteClient(int clientId)
        {
            throw new NotImplementedException();
        }

        public void EditAClient(ClientModel clientModel)
        {
            var implemented = true;
        }

        public List<ClientModel> GetAllClients()
        {
            return new List<ClientModel>();
        }

        public ClientModel GetClient(int clientId)
        {
            throw new NotImplementedException();
        }
        public ClientModel GetClientByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        Task IClientRepository.AddNewClient(ClientModel clientModel)
        {
            throw new NotImplementedException();
        }

        Task IClientRepository.DeleteClient(int clientId)
        {
            throw new NotImplementedException();
        }

        Task IClientRepository.EditAClient(ClientModel clientModel)
        {
            throw new NotImplementedException();
        }

        Task<ClientModel> IClientRepository.GetClient(int clientId)
        {
            throw new NotImplementedException();
        }

        Task<ClientModel> IClientRepository.GetClientByUserId(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
