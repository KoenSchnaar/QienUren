using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrenRegistratieQien.Data;
using UrenRegistratieQien.DatabaseClasses;
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
        public async Task AddNewClient(ClientModel clientModel)
        {
            context.Clients.Add(new DatabaseClasses.Client
            {
                ClientId = clientModel.ClientId,
                CompanyName = clientModel.CompanyName,
                CompanyPhone = clientModel.CompanyPhone,
                Contact1Name = clientModel.Contact1Name,
                Contact2Name = clientModel.Contact2Name,
                Contact1Phone = clientModel.Contact1Phone,
                Contact2Phone = clientModel.Contact2Phone,
                Contact1Email = clientModel.Contact1Email,
                Contact2Email = clientModel.Contact2Email
            });

            context.SaveChanges();
        }

        public async Task EditAClient(ClientModel clientModel)
        {
            var clientEntity = context.Clients.Single(c => c.ClientId == clientModel.ClientId);

            clientEntity.ClientId = clientModel.ClientId;
            clientEntity.CompanyName = clientModel.CompanyName;
            clientEntity.CompanyPhone = clientModel.CompanyPhone;
            clientEntity.Contact1Name = clientModel.Contact1Name;
            clientEntity.Contact1Phone = clientModel.Contact1Phone;
            clientEntity.Contact1Email = clientModel.Contact1Email;
            clientEntity.Contact2Name = clientModel.Contact2Name;
            clientEntity.Contact2Phone = clientModel.Contact2Phone;
            clientEntity.Contact2Email = clientModel.Contact2Email;

            context.SaveChanges();
        }
            

        public async Task<ClientModel> GetClient(int clientId)
        {
            var databaseClient = context.Clients.Single(p => p.ClientId == clientId);

            return new ClientModel
            {
                ClientId = clientId,
                CompanyName = databaseClient.CompanyName,
                Contact1Name = databaseClient.Contact1Name,
                Contact2Name = databaseClient.Contact2Name,
                Contact1Phone = databaseClient.Contact1Phone,
                Contact2Phone = databaseClient.Contact2Phone,
                Contact1Email = databaseClient.Contact1Email,
                Contact2Email = databaseClient.Contact2Email,
                CompanyPhone = databaseClient.CompanyPhone

            };

        }

        public async Task<ClientModel> GetClientByUserId(string userId)
        {
            var employee = context.Users.Single(u => u.Id == userId);

            var employeeCasted = (Employee)employee;

            var databaseClient = context.Clients.Single(p => p.ClientId == employeeCasted.ClientId);

            return new ClientModel
            {
                ClientId = employeeCasted.ClientId,
                CompanyName = databaseClient.CompanyName,
                Contact1Name = databaseClient.Contact1Name,
                Contact2Name = databaseClient.Contact2Name,
                Contact1Phone = databaseClient.Contact1Phone,
                Contact2Phone = databaseClient.Contact2Phone,
                Contact1Email = databaseClient.Contact1Email,
                Contact2Email = databaseClient.Contact2Email,
                CompanyPhone = databaseClient.CompanyPhone

            };

        }
        public async Task DeleteClient(int clientId)
        {
            var client = context.Clients.Single(c => c.ClientId == clientId);
            context.Clients.Remove(client);
            context.SaveChanges();
        }
    }
}
