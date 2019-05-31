using System;
using System.Globalization;
using System.Threading.Tasks;
using Windows.Web.Http;
using Newtonsoft.Json;
using Quisco.Model;

namespace Quisco.DataAccess
{
    public static class QuestionRequest
    {
        static Uri questionsBaseUri = new Uri("http://localhost:55418/api/Questions/");
        static HttpClient httpClient = new HttpClient();

        public static async Task<Question[]> GetQuestionListByQuizIdAsync(int id)
        {
            UriBuilder questionsUri = new UriBuilder(questionsBaseUri + "quizId/" + id);

            var result = await httpClient.GetAsync(questionsUri.Uri);
            var json = await result.Content.ReadAsStringAsync();
            var questionList = JsonConvert.DeserializeObject<Question[]>(json);
            return questionList;
        }

        public static async Task<Question> GetQuestionByIdAsync(int id)
        {
            Uri questionsUri = new Uri(questionsBaseUri + id.ToString(CultureInfo.InvariantCulture));

            var result = await httpClient.GetAsync(questionsUri);
            var json = await result.Content.ReadAsStringAsync();
            var question = JsonConvert.DeserializeObject<Question>(json);
            return question;
        }

        public static async Task<bool> QuestionExists(int id)
        {
            var a = await GetQuestionByIdAsync(id).ConfigureAwait(true);
            if (a == null) return false;
            return true;
        }

        public static async Task<bool> UpdateQuestion(Question question)
        {
            //Adds new question if it's new
            if (question.QuestionId == 0)
            {
                bool addedAnswerSucceeded = await AddQuestionToDbAsync(question).ConfigureAwait(true);
                return addedAnswerSucceeded;
            }

            else
            {
                //Updates question
                Uri questionsUri = new Uri(questionsBaseUri + question.QuestionId.ToString(CultureInfo.InvariantCulture));

                var json = JsonConvert.SerializeObject(question, Formatting.None,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                var httpContent = new HttpStringContent(json, Windows.Storage.Streams.UnicodeEncoding.Utf8,
                    "application/json");
                var result = await httpClient.PutAsync(questionsUri, httpContent);
                return result.IsSuccessStatusCode;
            }
        }

        public static async Task<bool> AddQuestionToDbAsync(Question question)
        {
            question.BelongingQuiz = null;
            var json = JsonConvert.SerializeObject(question, Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            var result = await httpClient.PostAsync(questionsBaseUri, new HttpStringContent(json, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));

            return result.IsSuccessStatusCode;
        }

        public static async Task<Question[]> GetQuestionListAsync()
        {
            var result = await httpClient.GetAsync(questionsBaseUri);
            var json = await result.Content.ReadAsStringAsync();
            var questionList = JsonConvert.DeserializeObject<Question[]>(json);
            return questionList;
        }
    }
}
