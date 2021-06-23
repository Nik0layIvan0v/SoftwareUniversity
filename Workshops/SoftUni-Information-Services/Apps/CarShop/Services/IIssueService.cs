using System.Collections.Generic;
using CarShop.ViewModels.Issues;

namespace CarShop.Services
{
    public interface IIssueService
    {
        CarIssueViewModel GetAllCarIssues(string carId);

        void FixCarIssue(string issueId, string carId);

        void AddCarIssue(IssueInputModel issueInputModel);

        void DeleteCarIssue(string issueId, string carId);
    }
}
