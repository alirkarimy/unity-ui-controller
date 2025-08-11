namespace Elka.UI.Controller
{
    public interface IUIFactory
    {
        IUserInterface GetUI(string pageName);

        IUserInterface GetUIAsync(string pageName);

    }

}
