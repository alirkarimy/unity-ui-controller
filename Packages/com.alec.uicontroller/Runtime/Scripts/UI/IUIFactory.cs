using System.Threading.Tasks;
namespace Elka.UI.Controller
{
    public interface IUIFactory
    {
        Task<IUserInterface> GetUIAsync(string pageName);

    }

}
