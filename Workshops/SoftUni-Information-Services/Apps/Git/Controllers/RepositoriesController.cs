using Git.Services.Repositories;
using Git.ViewModels.RepositoryModels;
using SUS.HTTP;
using SUS.MvcFramework;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Git.Controllers
{
    public class RepositoriesController : Controller
    {
        private readonly RepositoryService _repositoryService;

        public RepositoriesController(RepositoryService repositoryService)
        {
            _repositoryService = repositoryService;
        }

        public HttpResponse All()
        {
            RepositoryViewModel[] publicRepositories = this._repositoryService.GetAllPublicRepositories();

            if (this.IsUserSignedIn())
            {
                List<RepositoryViewModel> publicPlusPrivateRepos =
                    this._repositoryService.GetAllPrivateRepositories(this.GetUserId());

                publicPlusPrivateRepos.AddRange(publicRepositories.ToArray());

                return this.View(publicPlusPrivateRepos.ToArray());
            }
            
            return this.View(publicRepositories);
        }

        public HttpResponse Create()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error("You are not authorized!");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(RepositoryInputModel model)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error("You are not authorized!");
            }

            if (model.Name.Length < 3 || 
                model.Name.Length > 10 || 
                string.IsNullOrWhiteSpace(model.Name))
            {
                return this.Error("Invalid Repository name!");
            }

            int isPublic = string.Compare(model.RepositoryType, "Public", CultureInfo.InvariantCulture, CompareOptions.IgnoreCase);
            int isPrivate = string.Compare(model.RepositoryType, "Private", CultureInfo.InvariantCulture, CompareOptions.IgnoreCase);

            if (isPublic == -1 && isPrivate == -1 ||
                string.IsNullOrWhiteSpace(model.RepositoryType))
            {
                return this.Error("Invalid Repository type!");
            }

            string ownerId = this.GetUserId();

            this._repositoryService.CreateRepository(model, ownerId);

            return this.Redirect("/Repositories/All");
        }
    }
}