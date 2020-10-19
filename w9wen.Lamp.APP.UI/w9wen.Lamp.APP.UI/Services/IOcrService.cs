using System.Threading.Tasks;

namespace w9wen.Lamp.APP.UI.Services
{
    public interface IOcrService
    {
        Task<string> GetItemAsync();
    }
}