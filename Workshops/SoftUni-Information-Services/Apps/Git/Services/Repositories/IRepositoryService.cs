using System.Collections.Generic;
using Git.Data.Models;
using Git.ViewModels.RepositoryModels;

namespace Git.Services.Repositories
{
    public interface IRepositoryService
    {
        void CreateRepository(RepositoryInputModel model, string ownerId);

        RepositoryViewModel[] GetAllPublicRepositories();

        List<RepositoryViewModel> GetAllPrivateRepositories(string userId);
    }
}