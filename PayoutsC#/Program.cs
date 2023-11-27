using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        string apiUrl = "https://api.dlocal.com/api_curl/cashout_api/request_cashout";
        string timestamp = DateTimeOffset.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
        string login = "";
        string transKey = "";
        string secretKey = "";

        // Generate a random external_id
        string externalId = Guid.NewGuid().ToString();

        // Replace this with your actual request payload
        string requestPayload = $@"
        {{
            ""login"": ""{login}"",
            ""pass"": ""{transKey}"",
            ""external_id"": ""{externalId}"",
            ""document_id"": ""123.456.789-10"",
            ""beneficiary_name"": ""JUAN"",
            ""beneficiary_lastname"": ""NASCIMENTO"",
            ""country"": ""BR"",
            ""bank_code"": ""341"",
            ""bank_branch"": ""0167"",
            ""bank_name"": ""Itau"",
            ""bank_account"": ""12345-1"",
            ""account_type"": ""C"",
            ""comments"": ""this is the 1st comment"",
            ""notification_url"": ""https://thisisawebsite.net"",
            ""amount"": ""1100.00"",
            ""currency"": ""BRL"",
            ""type"": ""json""
        }}";

        string signature = GenerateHMACSHA256Signature(requestPayload, secretKey);

        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("X-Date", timestamp);
            client.DefaultRequestHeaders.Add("X-Login", login);
            client.DefaultRequestHeaders.Add("X-Trans-Key", transKey);
            client.DefaultRequestHeaders.Add("payload-signature", signature);

            var content = new StringContent(requestPayload, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Payment request successful!");
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response: {responseBody}");
                Console.WriteLine($"Header: {response}");
            }
            else
            {
                Console.WriteLine($"Payment request failed. Status code: {response.StatusCode}");
            }
        }
    }

    static string GenerateHMACSHA256Signature(string payload, string secretKey)
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(secretKey);
        byte[] payloadBytes = Encoding.UTF8.GetBytes(payload);

        using (var hmacsha256 = new HMACSHA256(keyBytes))
        {
            byte[] hashBytes = hmacsha256.ComputeHash(payloadBytes);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }
}
