namespace Git.ViewModels.RepositoryModels
{
    public class RepositoryViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Owner { get; set; }

        public string DateOfCreation { get; set; }

        public int CountOfCommits { get; set; }
    }
}