using Microsoft.Azure.Cosmos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Services
{
    public interface IInviteRepository
    {
        Task<IEnumerable<InviteEntity>> GetInvitesAsync();
        Task<InviteEntity> CreateInviteAsync(string name, string email);
        Task DeleteInviteAsync(string id);
    }

    public class InviteRepository : IInviteRepository
    {
        private readonly IDatabaseService databaseService;

        public InviteRepository(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        public async Task<IEnumerable<InviteEntity>> GetInvitesAsync()
        {
            Container container = await GetContainerAsync();

            List<InviteEntity> list = new();
            using FeedIterator<InviteEntity> resultSet = container.GetItemQueryIterator<InviteEntity>(queryDefinition: null);

            while(resultSet.HasMoreResults)
            {
                FeedResponse<InviteEntity> response = await resultSet.ReadNextAsync();
                list.AddRange(response);
            }

            return list;
        }

        public async Task<InviteEntity> CreateInviteAsync(string name, string email)
        {
            Container container = await GetContainerAsync();
            return await container.CreateItemAsync(new InviteEntity()
            {
                Name = name,
                Email = email,
            });
        }

        public async Task DeleteInviteAsync(string id)
        {
            Container container = await GetContainerAsync();
            await container.DeleteItemAsync<InviteEntity>(id, PartitionKey.Null);
        }


        #region Private methods

        private async Task<Container> GetContainerAsync()
        {
            Database database = await databaseService.GetDatabaseAsync();
            return await database.CreateContainerIfNotExistsAsync("Invites", "/partition");
        }

        #endregion
    }
}
