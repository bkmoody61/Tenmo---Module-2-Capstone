using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace TenmoClient.DAO
{
    class API_AccountsDAO
    {
        static protected RestClient client;
        public API_AccountsDAO(string apiUrl)
        {
            client = new RestClient(apiUrl);
        }
    }
}
