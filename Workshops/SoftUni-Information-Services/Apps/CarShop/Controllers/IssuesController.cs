using System.Linq;
using CarShop.Services;
using CarShop.ViewModels.Issues;
using SUS.HTTP;
using SUS.MvcFramework;
using static CarShop.Data.DataConstants.DataConstants;

namespace CarShop.Controllers
{
    public class IssuesController : Controller
    {
        private protected readonly IssueService IssueService;
        private protected readonly UsersService UsersService;

        public IssuesController(IssueService issueService, UsersService usersService)
        {
            this.IssueService = issueService;
            this.UsersService = usersService;
        }

        public HttpResponse Add(string carId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error("You are not logged in!");
            }

            CarIssueViewModel Model = new CarIssueViewModel
            {
                CarId = carId
            };

            return this.View(Model);
        }

        [HttpPost("/Issues/Add")]
        public HttpResponse AddIssue(IssueInputModel model)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error("You are not logged in!");
            }

            if (model.Description.Count(char.IsLetterOrDigit) < IssueMinDescription)
            {
                return this.Error($"Invalid Description. Min length is {IssueMinDescription}");
            }

            this.IssueService.AddCarIssue(model);

            return this.CarIssues(model.CarId);
        }

        public HttpResponse CarIssues(string carId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error("You are not logged in!");
            }

            var viewModel = this.IssueService.GetAllCarIssues(carId);

            return this.View(viewModel);
        }

        public HttpResponse Fix(string issueId, string CarId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error("You are not logged in!");
            }

            if (!this.UsersService.IsUserMechanic(this.GetUserId()))
            {
                return this.Error("Only mechanics can fix issues");
            }

            this.IssueService.FixCarIssue(issueId, CarId);

            return this.CarIssues(CarId);
        }

        public HttpResponse Delete(string issueId, string CarId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error("You are not logged in!");
            }

            this.IssueService.DeleteCarIssue(issueId, CarId);

            return this.CarIssues(CarId);
        }
    }
}