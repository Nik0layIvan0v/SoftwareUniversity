using Git.Data;
using Git.Data.Models;
using Git.ViewModels.RepositoryModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Git.Services.Repositories
{
    public class RepositoryService : IRepositoryService
    {
        private readonly ApplicationDbContext _dbContext;

        public RepositoryService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateRepository(RepositoryInputModel model, string ownerId)
        {
            string repositoryName = model.Name;

            DateTime createdOn = DateTime.UtcNow;

            bool isPublic = model.RepositoryType.ToLower() == "public" ? true : false;

            Repository repository = new Repository
            {
                Name = repositoryName,
                CreatedOn = createdOn,
                IsPublic = isPublic,
                OwnerId = ownerId,
            };

            this._dbContext.Repositories.Add(repository);
            this._dbContext.SaveChanges();
        }

        //DTO!
        public RepositoryViewModel[] GetAllPublicRepositories()
        {
            return this._dbContext.Repositories
                .Where(x => x.IsPublic)
                .Select(x=> new RepositoryViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Owner = x.Owner.Username,
                    DateOfCreation = x.CreatedOn.ToString("G"),
                    CountOfCommits = x.Commits.Count,

                })
                .ToArray();
        }

        public List<RepositoryViewModel> GetAllPrivateRepositories(string userId)
        {
            return this._dbContext.Repositories
                .Where(x=>x.IsPublic == false)
                .Select(x => new RepositoryViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Owner = x.Owner.Username,
                    DateOfCreation = x.CreatedOn.ToString("G"),
                    CountOfCommits = x.Commits.Count
                })
                .ToList();
        }
    }
}