using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrenRegistratieQien.Data;
using UrenRegistratieQien.Models;

namespace UrenRegistratieQien.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDbContext context;

        public ClientRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public List<ClientModel> GetAllClients()
        {
            var clientModelList = new List<ClientModel>();

            foreach (var client in context.Clients)

                clientModelList.Add(new ClientModel
                {
                    ClientId = client.ClientId,
                    CompanyName = client.CompanyName,
                    Contact1Name = client.Contact1Name,
                    Contact2Name = client.Contact2Name,
                    Contact1Phone = client.Contact1Phone,
                    Contact2Phone = client.Contact2Phone,
                    Contact1Email = client.Contact1Email,
                    Contact2Email = client.Contact2Email,
                    CompanyPhone = client.CompanyPhone
                }) ;

            return clientModelList;
        }
        public void AddNewClient(ClientModel clientModel)
        {
            context.Clients.Add(new DatabaseClasses.Client
            {
                ClientId = clientModel.ClientId,
                CompanyName = clientModel.CompanyName,
                Contact1Name = clientModel.Contact1Name,
                Contact2Name = clientModel.Contact2Name,
                Contact1Phone = clientModel.Contact1Phone,
                Contact2Phone = clientModel.Contact2Phone,
                Contact1Email = clientModel.Contact1Email,
                Contact2Email = clientModel.Contact2Email,
                CompanyPhone = clientModel.CompanyPhone
            });

            context.SaveChanges();
        }
}
}
