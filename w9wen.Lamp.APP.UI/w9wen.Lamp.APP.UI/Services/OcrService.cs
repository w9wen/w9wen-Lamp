using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using w9wen.Lamp.BE.API;

namespace w9wen.Lamp.APP.UI.Services
{
    public class OcrService : ServiceBase, IOcrService
    {
        public async Task<ResponseEntity<string>> GetItemAsync(List<Stream> mediaFileList)
        {
            return await GetItemAsync<string>("OCR", mediaFileList);
        }
    }
}