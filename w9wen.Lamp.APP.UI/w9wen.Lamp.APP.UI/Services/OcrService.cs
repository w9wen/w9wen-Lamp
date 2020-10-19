using System.Threading.Tasks;

namespace w9wen.Lamp.APP.UI.Services
{
    public class OcrService : ServiceBase, IOcrService
    {
        public async Task<string> GetItemAsync()
        {
            return await GetItemAsync<string>("OCR");
        }
    }
}