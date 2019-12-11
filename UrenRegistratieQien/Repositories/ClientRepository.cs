using Microsoft.EntityFrameworkCore;
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

        public ClientModel EntityToClientModel(Client entity)
        {
            var newModel = new ClientModel
            {
                ClientId = entity.ClientId,
                CompanyName = entity.CompanyName,
                Contact1Name = entity.Contact1Name,
                Contact2Name = entity.Contact2Name,
                Contact1Phone = entity.Contact1Phone,
                Contact2Phone = entity.Contact2Phone,
                Contact1Email = entity.Contact1Email,
                Contact2Email = entity.Contact2Email,
                CompanyPhone = entity.CompanyPhone
            };
            return newModel;
        }

        public Client ClientModelToEntity(ClientModel model)
        {
            var newEntity = new Client
            {
                ClientId = model.ClientId,
                CompanyName = model.CompanyName,
                CompanyPhone = model.CompanyPhone,
                Contact1Name = model.Contact1Name,
                Contact2Name = model.Contact2Name,
                Contact1Phone = model.Contact1Phone,
                Contact2Phone = model.Contact2Phone,
                Contact1Email = model.Contact1Email,
                Contact2Email = model.Contact2Email
            };
            return newEntity;
        }

        public async Task<List<ClientModel>> GetAllClients()
        {
            var clientModelList = new List<ClientModel>();

            foreach (var client in await context.Clients.ToListAsync())
            {
                clientModelList.Add(EntityToClientModel(client));
            }
            return clientModelList;
        }

        public async Task AddNewClient(ClientModel clientModel)
        {
            await context.Clients.AddAsync(ClientModelToEntity(clientModel));

            await context.SaveChangesAsync();
        }

        public async Task EditAClient(ClientModel clientModel)
        {
            var clientEntity = await context.Clients.SingleAsync(c => c.ClientId == clientModel.ClientId);

            clientEntity.ClientId = clientModel.ClientId;
            clientEntity.CompanyName = clientModel.CompanyName;
            clientEntity.CompanyPhone = clientModel.CompanyPhone;
            clientEntity.Contact1Name = clientModel.Contact1Name;
            clientEntity.Contact1Phone = clientModel.Contact1Phone;
            clientEntity.Contact1Email = clientModel.Contact1Email;
            clientEntity.Contact2Name = clientModel.Contact2Name;
            clientEntity.Contact2Phone = clientModel.Contact2Phone;
            clientEntity.Contact2Email = clientModel.Contact2Email;

            await context.SaveChangesAsync();
        }

        public async Task<ClientModel> GetClient(int clientId)
        {
            var databaseClient = await context.Clients.SingleAsync(p => p.ClientId == clientId);

            return EntityToClientModel(databaseClient);
        }

        public async Task<ClientModel> GetClientByUserId(string userId)
        {
            var employee = await context.Users.SingleAsync(u => u.Id == userId);

            var employeeCasted = (Employee)employee;

            var databaseClient = await context.Clients.SingleAsync(p => p.ClientId == employeeCasted.ClientId);

            return EntityToClientModel(databaseClient);

        }

        public async Task DeleteClient(int clientId)
        {
            var client = await context.Clients.SingleAsync(c => c.ClientId == clientId);
            context.Clients.Remove(client);
            await context.SaveChangesAsync();
        }
    }
}
