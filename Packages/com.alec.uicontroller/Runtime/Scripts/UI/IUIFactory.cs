namespace Elka.UI.Controller
{
    public interface IUIFactory
    {
        IUserInterface GetUIAsync(string pageName);

    }

}
