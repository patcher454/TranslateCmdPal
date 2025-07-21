using TranslateCmdPal.DTO;
using TranslateCmdPal.Enum;

using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TranslateCmdPal.Job
{
    public class JobHttp
    {
        private static HttpClient httpClient;
        private static string oldAPIKey;
        private const int MaxRetries = 3;
        private const int InitialRetryDelayMs = 1000;
        private static readonly Random Random = new Random();

        public static async Task<TranslationResultDTO> Translation(LangCode.Code targetCode, string text, string apiKey)
        {
            if (httpClient == null || oldAPIKey != apiKey)
            {
                Init(apiKey);
            }

            if (httpClient != null)
            {
                int retryCount = 0;
                while (true)
                {
                    try
                    {
                        object body = new
                        {
                            text = new string[] { text },
                            target_lang = LangCode.ToString(targetCode)
                        };

                        using StringContent jsonContent = new(
                            JsonSerializer.Serialize(body),
                            Encoding.UTF8,
                            "application/json"
                        );

                        HttpResponseMessage response = await httpClient.PostAsync("translate", jsonContent);

                        if (response.IsSuccessStatusCode)
                        {
                            string responseString = await response.Content.ReadAsStringAsync();
                            if (responseString != null)
                            {
                                var result = JsonSerializer.Deserialize<TranslationResultDTO>(responseString);
                                if (result != null)
                                {
                                    result.TargetLangCode = LangCode.ToString(targetCode);
                                    return result;
                                }
                            }
                        }

                        if (response.StatusCode == HttpStatusCode.Forbidden)
                        {
                            return CreateErrorResult(Properties.Resource.invalid_api_key, targetCode);
                        }
                        else if (response.StatusCode == HttpStatusCode.TooManyRequests)
                        {
                            if (retryCount >= MaxRetries)
                            {
                                return CreateErrorResult(Properties.Resource.error_too_many_requests, targetCode);
                            }

                            int delayMs = InitialRetryDelayMs * (int)Math.Pow(2, retryCount);
                            await Task.Delay(delayMs);
                            retryCount++;
                            continue;
                        }

                        return CreateErrorResult(Properties.Resource.error_message_during_translation, targetCode);
                    }
                    catch (Exception ex)
                    {
                        if (retryCount >= MaxRetries)
                        {
                            return CreateErrorResult($"Error: {ex.Message}", targetCode);
                        }
                        int delayMs = CalculateDelayWithJitter(retryCount);
                        await Task.Delay(delayMs);
                        retryCount++;
                    }
                }
            }

            return CreateErrorResult(Properties.Resource.error_message_during_translation, targetCode);
        }

        private static TranslationResultDTO CreateErrorResult(string message, LangCode.Code targetCode)
        {
            return new TranslationResultDTO
            {
                Translations = [
                    new TranslationDTO
                {
                    DetectedSourceLanguage = LangCode.ToString(LangCode.Code.UNK),
                    Text = message
                }
                ],
                TargetLangCode = LangCode.ToString(targetCode)
            };
        }

        private static int CalculateDelayWithJitter(int retryCount)
        {
            double baseDelay = 1000 * Math.Pow(2, retryCount);
            const double jitterPercentage = 0.23;
            double jitter = (Random.NextDouble() * 2 - 1) * jitterPercentage * baseDelay;
            return (int)Math.Min(baseDelay + jitter, 120000);
        }


        private static void Init(string apiKey)
        {
            oldAPIKey = apiKey;

            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api-free.deepl.com/v2/"),
                Timeout = TimeSpan.FromMinutes(2)
            };
            httpClient.DefaultRequestHeaders.Add("Authorization", apiKey);
        }
    }
}
