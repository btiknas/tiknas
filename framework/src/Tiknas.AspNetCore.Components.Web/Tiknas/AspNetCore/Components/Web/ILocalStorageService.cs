using System.Threading.Tasks;

namespace Tiknas.AspNetCore.Components.Web;

public interface ILocalStorageService
{
    public ValueTask SetItemAsync(string key, string value);
    public ValueTask<string> GetItemAsync(string key);
    public ValueTask RemoveItemAsync(string key);
}