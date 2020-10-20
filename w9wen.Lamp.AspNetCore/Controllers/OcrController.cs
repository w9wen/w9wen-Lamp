using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System;
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

                    //return await DisplayResults(formFile.OpenReadStream());
                    return await DisplayLines(formFile.OpenReadStream());
                }
            }

            return this.Ok();
        }

        // [HttpPost]
        // [Route("api/Ocr/GetLines")]
        // public async Task<ActionResult<string>> GetLines()
        // {
        //     return this.Ok("Test");
        // }

        private async Task<OcrResult> AzureOcr(Stream fileStream)
        {
            string subscriptionKey = "";
            string endpoint = "";

            var computerVision = new ComputerVisionClient(new ApiKeyServiceClientCredentials(subscriptionKey))
            {
                Endpoint = endpoint,
            };

            var analysis = await computerVision.RecognizePrintedTextInStreamAsync(true, fileStream);

            //var result = DisplayResults(analysis);

            return analysis;
        }

        private async Task<string> DisplayResults(Stream fileStream)
        {
            //text
            var ocrResult = await AzureOcr(fileStream);

            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("Text:");
            stringBuilder.AppendLine("Language: " + ocrResult.Language);
            stringBuilder.AppendLine("Text Angle: " + ocrResult.TextAngle);
            stringBuilder.AppendLine("Orientation: " + ocrResult.Orientation);
            stringBuilder.AppendLine("Text regions: ");
            foreach (var region in ocrResult.Regions)
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

        private async Task<string> DisplayLines(Stream fileStream)
        {
            //text
            var ocrResult = await AzureOcr(fileStream);

            var stringBuilder = new StringBuilder();

            //stringBuilder.AppendLine("Text:");
            //stringBuilder.AppendLine("Language: " + ocrResult.Language);
            //stringBuilder.AppendLine("Text Angle: " + ocrResult.TextAngle);
            //stringBuilder.AppendLine("Orientation: " + ocrResult.Orientation);
            //stringBuilder.AppendLine("Text regions: ");
            foreach (var region in ocrResult.Regions)
            {
                //stringBuilder.AppendLine("Region bounding box: " + region.BoundingBox);
                foreach (var line in region.Lines)
                {
                    //stringBuilder.AppendLine("Line bounding box: " + line.BoundingBox);
                    //stringBuilder.AppendLine(line.ToString());
                    foreach (var word in line.Words)
                    {
                        //stringBuilder.AppendLine("Word bounding box: " + word.BoundingBox);
                        stringBuilder.Append(word.Text);
                    }
                    stringBuilder.Append(Environment.NewLine);
                }
            }

            return stringBuilder.ToString();
        }
    }
}