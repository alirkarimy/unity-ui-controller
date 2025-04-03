namespace Elka.UI.Controller
{
    public interface IUIFactory
    {
        IUserInterface GetUI(UIType type);

        IUserInterface GetUIAsync(UIType type);

    }

}
