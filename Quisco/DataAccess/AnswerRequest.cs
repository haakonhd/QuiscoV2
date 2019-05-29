using System;
using System.Threading.Tasks;
using Windows.Web.Http;
using Newtonsoft.Json;
using Quisco.Model;

namespace Quisco.DataAccess
{
    public class AnswerRequest
    {
        private static readonly Uri answersBaseUri = new Uri("http://localhost:55418/api/Answers");
        private readonly HttpClient httpClient = new HttpClient();

        public async Task<Answer[]> GetAnswerList()
        {
            var result = await httpClient.GetAsync(answersBaseUri);
            var json = await result.Content.ReadAsStringAsync();
            var answerList = JsonConvert.DeserializeObject<Answer[]>(json);
            return answerList;
        }

        public async Task<Answer[]> GetAnswerListByQuestionId(int id)
        {
            UriBuilder answersUri = new UriBuilder(answersBaseUri + "/questionId/" + id);

            var result = await httpClient.GetAsync(answersUri.Uri);
            var json = await result.Content.ReadAsStringAsync();
            var answerList = JsonConvert.DeserializeObject<Answer[]>(json);
            return answerList;
        }
    }
}
