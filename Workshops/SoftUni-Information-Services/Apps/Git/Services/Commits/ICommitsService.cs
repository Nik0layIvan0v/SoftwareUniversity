using System.Collections.Generic;
using Git.ViewModels.CommitModels;

namespace Git.Services.Commits
{
    public interface ICommitsService
    {
        void Create(CommitInputModel model, string creatorId);

        void Delete(string commitId);

        List<CommitViewModel> GetAllCommits();

        CommitRepositoryViewModel GetRepository(string repoId);

        bool CheckRepositoryOwner(string repoId, string ownerId);
    }
}