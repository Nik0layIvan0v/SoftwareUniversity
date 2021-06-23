using System.Collections.Generic;

namespace CarShop.ViewModels.Issues
{
    public class CarIssueViewModel
    {
        public string CarId { get; set; }

        public int CarYear { get; set; }

        public string CarModel { get; set; }

        public List<IssueInfoViewModel> CarIssues { get; set; } = new List<IssueInfoViewModel>();
    }
}