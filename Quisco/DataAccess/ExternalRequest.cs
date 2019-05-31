using System;
using System.Threading.Tasks;
using Windows.Web.Http;
using Newtonsoft.Json;
using Quisco.Helpers;
using Quisco.Model;

namespace Quisco.DataAccess
{
    public static class ExternalRequest
    {
        static HttpClient httpClient = new HttpClient();

        public static async Task<RootObject> GetQuizzesFromExternal(OpentdbParams opentdbParams)
        {
            string uriString = "https://opentdb.com/api.php?amount=" + opentdbParams.Amount + opentdbParams.Category + opentdbParams.Difficulty + "&type=multiple";
            Uri opentdbUri = new Uri(uriString);

            var result = await httpClient.GetAsync(opentdbUri);
            string json = "";
            if (result.IsSuccessStatusCode)
            json = await result.Content.ReadAsStringAsync();
            RootObject rootObject = JsonConvert.DeserializeObject<RootObject>(json);
            return rootObject;
        }
    }
}
