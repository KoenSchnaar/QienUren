
﻿using System.Collections.Generic;
using System.Threading.Tasks;
using UrenRegistratieQien.Models;




namespace UrenRegistratieQien.Repositories
{
    public interface IClientRepository
    {
        public Task<List<ClientModel>> GetAllClients();
        Task AddNewClient(ClientModel clientModel);
        Task EditAClient(ClientModel clientModel);
        Task<ClientModel> GetClient(int clientId);
        Task<ClientModel> GetClientByUserId(string userId);
        Task DeleteClient(int clientId);
    }
}