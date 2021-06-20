using System;
using System.Collections.Generic;
using System.Linq;
using Git.Data;
using Git.Data.Models;
using Git.ViewModels.CommitModels;

namespace Git.Services.Commits
{
    public class CommitsService : ICommitsService
    {
        private readonly ApplicationDbContext _dbContext;

        public CommitsService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(CommitInputModel model, string creatorId)
        {
            Commit commit = new Commit
            {
                Description = model.description,
                CreatedOn = DateTime.UtcNow,
                CreatorId = creatorId,
                RepositoryId = model.id,
            };

            this._dbContext.Commits.Add(commit);

            this._dbContext.SaveChanges();
        }

        public void Delete(string commitId)
        {
            var commitToDelete = this._dbContext.Commits.FirstOrDefault(commit => commit.Id == commitId);

            if (commitToDelete != null)
            {
                this._dbContext.Commits.Remove(commitToDelete);
                this._dbContext.SaveChanges();
            }
        }

        public List<CommitViewModel> GetAllCommits()
        {
            return this._dbContext.Commits
                .Select(x => new CommitViewModel
                {
                    Id = x.Id,
                    RepositoryName = x.Repository.Name,
                    Description = x.Description,
                    CreatedOn = x.CreatedOn.ToString("G")
                })
                 .ToList();
        }

        public CommitRepositoryViewModel GetRepository(string repoId)
        {
            return this._dbContext.Repositories
                .Where(repo => repo.Id == repoId)
                .Select(x => new CommitRepositoryViewModel
                {
                    RepositoryId = x.Id,
                    RepositoryName = x.Name,
                })
                .FirstOrDefault();
        }

        public bool CheckRepositoryOwner(string repoId, string ownerId)
        {
            return this._dbContext.Repositories.Any(x => x.OwnerId == ownerId && x.Id == repoId);
        }
    }
}