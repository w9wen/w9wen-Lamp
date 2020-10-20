using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using w9wen.Lamp.BE.API;

namespace w9wen.Lamp.APP.UI.Services
{
    public interface IOcrService
    {
        Task<ResponseEntity<string>> GetItemAsync(List<Stream> mediaFileList);
    }
}