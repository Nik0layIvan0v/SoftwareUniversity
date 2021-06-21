namespace SUS.MvcFramework.ViewEngine
{
    public interface IView
    {
        string GenerateHtml(object viewModel, string user, string userRole, string username);
    }
}