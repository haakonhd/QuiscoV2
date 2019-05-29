using System;
using System.Threading.Tasks;
using Windows.Web.Http;
using Newtonsoft.Json;
using Quisco.Model;

namespace Quisco.DataAccess
{
    public class QuestionRequest
    {
        static Uri questionsBaseUri = new Uri("http://localhost:55418/api/Questions");
        HttpClient httpClient = new HttpClient();

        public async Task<Question[]> GetQuestionListByQuizId(int id)
        {
            UriBuilder questionsUri = new UriBuilder(questionsBaseUri + "/quizId/" + id);

            var result = await httpClient.GetAsync(questionsUri.Uri);
            var json = await result.Content.ReadAsStringAsync();
            var questionList = JsonConvert.DeserializeObject<Question[]>(json);
            return questionList;
        }

        public async Task<Question[]> GetQuestionList()
        {
            var result = await httpClient.GetAsync(questionsBaseUri);
            var json = await result.Content.ReadAsStringAsync();
            var questionList = JsonConvert.DeserializeObject<Question[]>(json);
            return questionList;
        }
    }
}
