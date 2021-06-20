using System.Collections.Generic;
using Git.Services.Commits;
using Git.ViewModels.CommitModels;
using SUS.HTTP;
using SUS.MvcFramework;

namespace Git.Controllers
{
    public class CommitsController : Controller
    {
        private readonly CommitsService _commitsService;

        public CommitsController(CommitsService commitsService)
        {
            this._commitsService = commitsService;
        }

        public HttpResponse Delete(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error("You are not authorized!");
            }

            string loggedUser = this.GetUserId();

            bool isOwner = this._commitsService.CheckRepositoryOwner(id, loggedUser);

            if (!isOwner)
            {
                return this.Error("You cannot delete commit because you are not owner of it.");
            }

            this._commitsService.Delete(id);

            return this.Redirect("/Commits/All");
        }

        public HttpResponse Create(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error("You are not authorized!");
            }

            CommitRepositoryViewModel view = this._commitsService.GetRepository(id);

            return this.View(view);
        }

        [HttpPost]
        public HttpResponse Create(CommitInputModel model)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error("You are not authorized!");
            }

            this._commitsService.Create(model, GetUserId());

            return this.Redirect("/Repositories/All");
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error("You are not authorized!");
            }

            List<CommitViewModel> commits = this._commitsService.GetAllCommits();

            return this.View(commits.ToArray());
        }
    }
}