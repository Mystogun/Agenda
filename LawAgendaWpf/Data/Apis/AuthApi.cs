using System.Threading.Tasks;
using Flurl.Http;
using LawAgendaWpf.Constants;
using LawAgendaWpf.Data.Responses;

namespace LawAgendaWpf.Data.Apis
{
    public static class AuthApi
    {
        public static async Task<LoginResponse> Login(string username, string password)
        {
            var client = BaseApi.Instance.Client;
            var response = await client.Request(Links.LoginUrl)
                .PostUrlEncodedAsync(new {username, password})
                .ReceiveJson<LoginResponse>();
            
            BaseApi.Instance.StoreAuth(response.Token);

            return response;
        }
    }
}