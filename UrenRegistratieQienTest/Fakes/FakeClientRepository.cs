using System;
using System.Collections.Generic;
using System.Text;
using UrenRegistratieQien.Models;
using UrenRegistratieQien.Repositories;

namespace UrenRegistratieQienTest.Fakes
{
    class FakeClientRepository : IClientRepository
    {
        public void AddNewClient(ClientModel clientModel)
        {
            throw new NotImplementedException();
        }

        public void EditAClient(ClientModel clientModel)
        {
            throw new NotImplementedException();
        }

        public List<ClientModel> GetAllClients()
        {
            var clientModelList = new List<ClientModel>();


            clientModelList.Add(new ClientModel
            {
                ClientId = 0,
                CompanyName = "companyname",
                Contact1Name = "1name",
                Contact2Name = "2name",
                Contact1Phone = 0,
                Contact2Phone = 0,
                Contact1Email = "1Email",
                Contact2Email = "2Email",
                CompanyPhone = 0
            });

            return clientModelList;
        }

        public ClientModel GetClient(int clientId)
        {
            throw new NotImplementedException();
        }
    }
}
