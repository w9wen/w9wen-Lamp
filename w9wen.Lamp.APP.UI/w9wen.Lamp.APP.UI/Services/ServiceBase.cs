using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using w9wen.Lamp.BE.API;
using w9wen.Lamp.BE.Exceptions;

namespace w9wen.Lamp.APP.UI.Services
{
    public class ServiceBase
    {
        protected HttpClient GetClient()
        {
            return GetClient(App.ServerUrl);
        }

        protected HttpClient GetClient(string serverUrl)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(serverUrl);

            return client;
        }

        protected async Task<ResponseEntity<T>> GetItemAsync<T>(string serviceUrl, List<Stream> mediaFileList = null)
        {
            using (HttpClient client = GetClient())
            {
                var response = new HttpResponseMessage();
                try
                {
                    var stringContent = new StringContent(string.Empty, Encoding.UTF8, "application/json");
                    if (mediaFileList == null || mediaFileList.Count < 1)
                    {
                        //// 無上傳檔案
                        response = await client.PostAsync(serviceUrl, stringContent).ConfigureAwait(false);
                    }
                    else
                    {
                        //// 上傳檔案
                        using (var multipartFormDataContent = new MultipartFormDataContent())
                        {
                            multipartFormDataContent.Add(stringContent, "Parameter");
                            var i = 0;
                            foreach (var item in mediaFileList)
                            {
                                i++;
                                var streamContent = new StreamContent(item);
                                streamContent.Headers.Add("Content-Type", "application/octet-stream");
                                streamContent.Headers.Add("Content-Disposition", $@"form-data; name=""{i}-files""; filename=""{i}.jpg""");

                                multipartFormDataContent.Add(streamContent, "file", i.ToString() + ".jpg");
                            }

                            client.DefaultRequestHeaders
                                .Accept
                                .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                            response = await client.PostAsync(serviceUrl, multipartFormDataContent).ConfigureAwait(false);
                        }
                    }

                    if (!response.IsSuccessStatusCode)
                    {
                    }

                    var resultJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    var result = new ResponseEntity<T>
                    {
                        Status = ResponseStatusEnum.Success,
                        Message = string.Empty,
                        DetailMessage = string.Empty,
                        Result = JsonConvert.DeserializeObject<T>(resultJson),
                    };

                    return result;
                }
                catch (Exception ex)
                {
                    var result = new ResponseEntity<T>
                    {
                        Status = ResponseStatusEnum.Error,
                        Message = string.Empty,
                        DetailMessage =
                            $"[Message: {ex.Message}]" + Environment.NewLine +
                            $"[StackTrace: {ex.StackTrace}]"
                    };
                    return result;
                }
            }
        }
    }
}