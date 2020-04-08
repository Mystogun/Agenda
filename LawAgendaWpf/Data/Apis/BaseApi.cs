using System;
using Flurl.Http;
using LawAgendaWpf.Constants;

namespace LawAgendaWpf.Data.Apis
{
    public class BaseApi
    {
        private static readonly Lazy<BaseApi> _lazy = new Lazy<BaseApi>(() => new BaseApi());
        public readonly FlurlClient Client;


        public BaseApi()
        {
            Client = new FlurlClient(Links.BaseUrl);
        }

        public static BaseApi Instance => _lazy.Value;


        public void StoreAuth(string token)
        {
            Client.Headers["Authorization"] = $"Bearer {token}";
        }
    }
}