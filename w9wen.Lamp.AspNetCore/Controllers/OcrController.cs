using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace w9wen.Lamp.AspNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OcrController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<string>> OCR()
        {
            var request = HttpContext.Request;
            var files = request.Form.Files;

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var fileName = formFile.FileName;

                    return await AzureOcr(formFile.OpenReadStream());
                }
            }

            return this.Ok();
        }

        private async Task<string> AzureOcr(Stream fileStream)
        {
            string subscriptionKey = "SubscriptionKey";
            string endpoint = "Endpoint";

            var computerVision = new ComputerVisionClient(new ApiKeyServiceClientCredentials(subscriptionKey))
            {
                Endpoint = endpoint,
            };

            var analysis = await computerVision.RecognizePrintedTextInStreamAsync(true, fileStream);

            var result = DisplayResults(analysis);

            return result;
        }

        private string DisplayResults(OcrResult analysis)
        {
            //text

            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("Text:");
            stringBuilder.AppendLine("Language: " + analysis.Language);
            stringBuilder.AppendLine("Text Angle: " + analysis.TextAngle);
            stringBuilder.AppendLine("Orientation: " + analysis.Orientation);
            stringBuilder.AppendLine("Text regions: ");
            foreach (var region in analysis.Regions)
            {
                stringBuilder.AppendLine("Region bounding box: " + region.BoundingBox);
                foreach (var line in region.Lines)
                {
                    stringBuilder.AppendLine("Line bounding box: " + line.BoundingBox);

                    foreach (var word in line.Words)
                    {
                        stringBuilder.AppendLine("Word bounding box: " + word.BoundingBox);
                        stringBuilder.AppendLine("Text: " + word.Text);
                    }
                    stringBuilder.AppendLine("\n");
                }
                stringBuilder.AppendLine("\n \n");
            }

            return stringBuilder.ToString();
        }
    }
}