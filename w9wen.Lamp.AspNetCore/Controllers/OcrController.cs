using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using w9wen.Lamp.BE;

namespace w9wen.Lamp.AspNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OcrController : ControllerBase
    {
        private List<AssetEntity> AssetList { get; set; }

        public OcrController()
        {
            this.AssetList = new List<AssetEntity>();

            this.AssetList.Add(new AssetEntity()
            {
                Id = 0,
                //AssetNo = "1057604",
                AssetNo = "1070791",
                OldAssetNo = string.Empty,
                AssetName = "Acer M490 i5",
                Custodian = "01_5F05_梁X壁",
                CustodyLocation = "886-台北",
                CustodyDepartment = "總務採購部",
            });

            this.AssetList.Add(new AssetEntity()
            {
                Id = 1,
                AssetNo = "1062934",
                OldAssetNo = "1035558",
                AssetName = "Acer M4610 i3",
                Custodian = "01_3F05_王大明",
                CustodyLocation = "886-台北",
                CustodyDepartment = "普橘島",
            });

            this.AssetList.Add(new AssetEntity()
            {
                Id = 2,
                AssetNo = "1063008",
                OldAssetNo = "1036280",
                AssetName = "mangoldvision眼球追蹤系統PC",
                Custodian = "01_5F01_詹O安",
                CustodyLocation = "886-台北",
                CustodyDepartment = "總務採購部",
            });

            this.AssetList.Add(new AssetEntity()
            {
                Id = 3,
                AssetNo = "1064828",
                OldAssetNo = "1043268",
                AssetName = "Acer M4610 I5",
                Custodian = "01_5F05_陳凱莉",
                CustodyLocation = "886-台北",
                CustodyDepartment = "總務採購部",
            });

            this.AssetList.Add(new AssetEntity()
            {
                Id = 4,
                AssetNo = "1080166",
                OldAssetNo = string.Empty,
                AssetName = "Acer VM6660G(使用人:羅慶祥)",
                Custodian = "01_3A05_孤獨夫",
                CustodyLocation = "886-台北",
                CustodyDepartment = "行政服務處",
            });
        }

        [HttpPost]
        public async Task<ActionResult<AssetEntity>> OCR()
        {
            var request = HttpContext.Request;
            var files = request.Form.Files;

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var fileName = formFile.FileName;

                    //return await DisplayResults(formFile.OpenReadStream());

                    var assetNo = await DisplayLines(formFile.OpenReadStream()).ConfigureAwait(false);
                    // var assetNo = "1063008";

                    var assetItem = this.AssetList.FirstOrDefault(a => a.AssetNo == assetNo);

                    if (assetItem != null)
                    {
                        return this.Ok(assetItem);
                    }
                    else
                    {
                        return this.BadRequest();
                    }
                }
            }

            return this.BadRequest();
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
                    //stringBuilder.Append(Environment.NewLine);
                }
            }

            return stringBuilder.ToString();
        }
    }
}