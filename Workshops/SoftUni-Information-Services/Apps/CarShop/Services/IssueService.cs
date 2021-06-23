using CarShop.Data;
using CarShop.Data.Models;
using CarShop.ViewModels.Issues;
using System.Linq;

namespace CarShop.Services
{
    public class IssueService : IIssueService
    {
        private protected readonly ApplicationDbContext DbContext;

        public IssueService(ApplicationDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public CarIssueViewModel GetAllCarIssues(string carId)
        {
            return this.DbContext.Cars
                .Select(c => new CarIssueViewModel
                {
                    CarId = c.Id,
                    CarYear = c.Year,
                    CarModel = c.Model,
                    CarIssues = c.Issues
                    .Select(i => new IssueInfoViewModel
                    {
                        IssueId = i.Id,
                        Description = i.Description,
                        IsFixed =
                            i.IsFixed == true
                                ? "Is Fixed"
                                : "Not fixed"
                    }).ToList()

                }).FirstOrDefault(x => x.CarId == carId);
        }

        public void FixCarIssue(string issueId, string carId)
        {
            Issue issue = this.DbContext.Issues
                .FirstOrDefault(x => x.Id == issueId && x.CarId == carId);

            if (issue != null)
            {
                issue.IsFixed = true;
            }

            this.DbContext.SaveChanges();
        }

        public void AddCarIssue(IssueInputModel issueInputModel)
        {
            Issue issue = new Issue
            {
                Description = issueInputModel.Description,
                IsFixed = false,
                CarId = issueInputModel.CarId,
            };

            this.DbContext.Issues.Add(issue);
            this.DbContext.SaveChanges();
        }

        public void DeleteCarIssue(string issueId, string carId)
        {
            Issue issue = this.DbContext.Issues
                .FirstOrDefault(x => x.Id == issueId && x.CarId == carId);

            if (issue != null)
            {
                this.DbContext.Issues.Remove(issue);

                this.DbContext.SaveChanges();
            }

        }
    }
}