using JazzcashPortal.DAL;
using JazzcashPortal.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace JazzcashPortal.BLL
{
    public class HomePolicyService
    {
        private readonly HomePolicyRepository _dal;
        public HomePolicyService(HomePolicyRepository dal)
        {
            _dal = dal;
        }

        //**************************************** HomePolicyService ****************************************
        public DataTable GetLeads()
        {
            return _dal.GetLeads();
        }

        public DataTable SearchHomePolicy(HomePolicy mdl)
        {
            return _dal.SearchHomePolicy(mdl);
        }

        public async Task<DbActionResult> ReversePolicy(Jazzcash m, string policy_code)
        {
            bool output = false;
            var dbar = new DbActionResult();
            var result = await JazzcashResultAPI(m);
            //bool output = _dal.ReversePolicy(policy_code);
            if (output)
            {
                dbar.Action = true;
                dbar.Message = "Revered Successfully.";
            }
            else
            {
                dbar.ErrorMessage = "Error Occured.";
            }
            return dbar;
        }

        public static async Task<JazzcashResult> JazzcashResultAPI(Jazzcash m)
        {
            string iv = m.IV ?? "";
            string secretKey = m.Secret_Key ?? "";
            string decryptedResponse = "";
            var obj = new JazzcashResult();

            string inputpara = $@"{{
  ""originalTransactionId"": ""{m.TRANSACTION_ID}"",
  ""referenceId"": ""dsdsds"",
  ""POSID"": ""POSID1""
}}
";

            string encryptedRequest = Encrypt(inputpara, secretKey, iv);

            var postData = new
            {
                data = encryptedRequest
            };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json");

            string apiUrl = "https://gateway-sandbox.jazzcash.com.pk/jazzcash/third-party-integration/rest/api/wso2/v1/insurance/unsub";
            using (var client = new HttpClient())
            {
                // Add required headers
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("X-CLIENT-ID", m.X_CLIENT_ID);
                client.DefaultRequestHeaders.Add("X-CLIENT-SECRET", m.X_CLIENT_SECRET);
                client.DefaultRequestHeaders.Add("X-PARTNER-ID", m.X_PARTNER_ID);

                try
                {
                    var response = await client.PostAsync(apiUrl, jsonContent);

                    if (!response.IsSuccessStatusCode)
                    {
                        string errorBody = await response.Content.ReadAsStringAsync();
                        obj = new JazzcashResult
                        {
                            action = false,
                            error_message = errorBody,
                            status_code = ((int)response.StatusCode).ToString(),
                        };
                        //Console.WriteLine($"Error: {(int)response.StatusCode} - {response.ReasonPhrase}");
                        //Console.WriteLine($"Body: {errorBody}");
                    }
                    else
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        var jsonObj = JObject.Parse(responseBody);
                        string encryptedData = jsonObj["data"]?.ToString() ?? string.Empty;

                        if (!string.IsNullOrEmpty(responseBody))
                        {
                            decryptedResponse = Decrypt(encryptedData, secretKey, iv);
                        }
                        obj = new JazzcashResult
                        {
                            action = true,
                            message = decryptedResponse,
                            status_code = "200",
                        };
                    }
                    return obj;
                }
                catch (HttpRequestException httpEx)
                {
                    obj = new JazzcashResult
                    {
                        action = false,
                        error_message = httpEx.Message,
                        status_code = "500"
                    };
                    return obj;
                }
            }
        }

        public static string Encrypt(string plainText, string key, string iv)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = Encoding.UTF8.GetBytes(iv);
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

                return BitConverter.ToString(encryptedBytes).Replace("-", "");
            }
        }

        // AES-128-CBC decryption
        public static string Decrypt(string hexCipherText, string key, string iv)
        {
            byte[] cipherText = HexStringToByteArray(hexCipherText);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = Encoding.UTF8.GetBytes(iv);
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (var msDecrypt = new MemoryStream(cipherText))
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (var srDecrypt = new StreamReader(csDecrypt))
                {
                    return srDecrypt.ReadToEnd();
                }
            }
        }

        private static byte[] HexStringToByteArray(string hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }


        //**************************************** ActivePoliciesService ****************************************
        public DataTable GetActivePolicies()
        {
            return _dal.GetActivePolicies();
        }


        //**************************************** CancelPoliciesService ****************************************
        public DataTable GetCancelPolicies()
        {
            return _dal.GetCancelPolicies();
        }
    }
}
