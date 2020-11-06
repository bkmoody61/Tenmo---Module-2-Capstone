using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Text;

namespace TenmoClient.Data
{
    public class APIService
    {
        private readonly static string API_BASE_URL = "https://localhost:44315/";
        private readonly IRestClient client = new RestClient();

        public Account GetUserAccountBalance(int userId)
        {
            RestRequest request = new RestRequest($"{API_BASE_URL}accounts/{userId}");
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());
            IRestResponse<Account> response = client.Get<Account>(request);

            return response.Data;
        }

        public List<User> GetAllUsers()
        {
            RestRequest request = new RestRequest($"{API_BASE_URL}user");
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());
            IRestResponse<List<User>> response = client.Get<List<User>>(request);

            return response.Data;
        }

        public Transfer AddTransfer(Transfer newTransfer)
        {
            RestRequest request = new RestRequest($"{API_BASE_URL}transfer");
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());
            request.AddJsonBody(newTransfer);
            IRestResponse<Transfer> response = client.Post<Transfer>(request);

            if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
            {
                ProcessErrorResponse(response);
            }
            else
            {
                return response.Data;
            }
            return null;
        }

        private void ProcessErrorResponse(IRestResponse<Transfer> response)
        {
            throw new NotImplementedException();
        }
    }
}
