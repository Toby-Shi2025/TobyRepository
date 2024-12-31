namespace MyFirstCSharpProject
{
    using Newtonsoft.Json;
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;

    /// <summary>
    /// FE_SUN-031672_Retail Stores RTO Auto Receive Functionality
    /// Toby Shi - 12/24/2024
    /// <c>FE_UPSTrackingAPIHelper</c> Call UPS tracking API helper class.
    /// </summary>
    public class UPSTrackingAPI
    {
        public const string ClientId = "g01sR0sH6s91rRYJNCVGRoiiGWC9ImRGf10kQdP5sg0Mbyyw";
        public const string ClientSecret = "AohdaAo2Uq2vMjdrVPLZoGehDOBZEV9tcLhH8bTYlKpFQs4576jLsXEqfAQ03NY3";
        public const string TokenUrl = "https://wwwcie.ups.com/security/v1/oauth/token";
        public const string trackingUrl = "https://onlinetools.ups.com/api/track/v1/details/1ZRV90090355833895?locale=en_US&returnSignature=false";
        public const string TransactionSRC = "D365";

        
        public static void Main(string[] args)
        {
            UPSTrackingAPI helper = new UPSTrackingAPI();
            helper.Process();
        }
        public void Process()
        {
            try
            {
                string token = GetAccessToken();
                if (!string.IsNullOrEmpty(token))
                {
                    GetTrackingDetails(token);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while processing the UPS tracking API: " + ex.Message, ex);
            }
        }

        
        private string GetAccessToken()
        {
            using (var client = new HttpClient())
            {
                var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{ClientId}:{ClientSecret}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);

                var content = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");
                var response = client.PostAsync(TokenUrl, content).Result;

                if (response.IsSuccessStatusCode
                    && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    response.EnsureSuccessStatusCode();
                    var responseBody = response.Content.ReadAsStringAsync().Result;
                    dynamic jsonResponse = JsonConvert.DeserializeObject(responseBody);
                    
                    if (jsonResponse != null)
                    return jsonResponse.access_token;
                }
            }
            return "";
        }

        
        private void GetTrackingDetails(string token)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                string transId = Guid.NewGuid().ToString();
                client.DefaultRequestHeaders.Add("transId", transId);
                client.DefaultRequestHeaders.Add("transactionSrc", TransactionSRC);

                var response = client.GetAsync(trackingUrl).Result;

                if (response.IsSuccessStatusCode
                    && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    response.EnsureSuccessStatusCode();
                    string responseBody = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine(responseBody);
                }
            }
        }
        
    }
}