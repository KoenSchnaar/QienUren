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
        public ClientModel GetClient(int clientId)
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
    }
}
