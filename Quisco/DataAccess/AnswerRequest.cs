using System;
using System.Globalization;
using System.Threading.Tasks;
using Windows.Web.Http;
using Newtonsoft.Json;
using Quisco.Model;

namespace Quisco.DataAccess
{
    public static class AnswerRequest
    {
        private static Uri answersBaseUri = new Uri("http://localhost:55418/api/Answers/");
        private static HttpClient httpClient = new HttpClient();

        public static async Task<Answer[]> GetAnswerList()
        {
            var result = await httpClient.GetAsync(answersBaseUri);
            var json = await result.Content.ReadAsStringAsync();
            var answerList = JsonConvert.DeserializeObject<Answer[]>(json);
            return answerList;
        }

        public static async Task<Answer[]> GetAnswerListByQuestionId(int id)
        {
            UriBuilder answersUri = new UriBuilder(answersBaseUri + "questionId/" + id);

            var result = await httpClient.GetAsync(answersUri.Uri);
            var json = await result.Content.ReadAsStringAsync();
            var answerList = JsonConvert.DeserializeObject<Answer[]>(json);
            return answerList;
        }

        public static async Task<Answer> GetAnswerByIdAsync(int id)
        {
            Uri answersUri = new Uri(answersBaseUri + id.ToString(CultureInfo.InvariantCulture));

            var result = await httpClient.GetAsync(answersUri);
            var json = await result.Content.ReadAsStringAsync();
            var answer = JsonConvert.DeserializeObject<Answer>(json);
            return answer;
        }

        public static async Task<bool> AnswerExists(int id)
        {
            var a = await GetAnswerByIdAsync(id).ConfigureAwait(true);
            if (a == null) return false;
            return true;
        }

        public static async Task<bool> AddAnswerToDbAsync(Answer answer)
        {
            answer.BelongingQuestion = null;
            var json = JsonConvert.SerializeObject(answer, Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            var result = await httpClient.PostAsync(answersBaseUri, new HttpStringContent(json, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));

            return result.IsSuccessStatusCode;
        }

        public static async Task<bool> UpdateAnswer(Answer answer)
        {
            if (answer.ToBeDeleted)
            {
                bool deleteAnswerSucceeded = await DeleteAnswerAsync(answer).ConfigureAwait(true);
                return deleteAnswerSucceeded;
            }
            else
            {
                //Adds new question if it's new
                if (answer.AnswerId == 0)
                {
                    bool addAnswerSucceeded = await AddAnswerToDbAsync(answer).ConfigureAwait(true);
                    return addAnswerSucceeded;
                }

                else
                {
                    //Updates question
                    Uri answerUri = new Uri(answersBaseUri + answer.AnswerId.ToString(CultureInfo.InvariantCulture));

                    var json = JsonConvert.SerializeObject(answer, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
                    var httpContent = new HttpStringContent(json, Windows.Storage.Streams.UnicodeEncoding.Utf8,
                        "application/json");
                    var result = await httpClient.PutAsync(answerUri, httpContent);
                    return result.IsSuccessStatusCode;
                }
            }
        }
        public static async Task<bool> DeleteAnswerAsync(Answer answer)
        {
            Uri uri = new Uri(answersBaseUri + answer.AnswerId.ToString(CultureInfo.InvariantCulture));
            var result = await httpClient.DeleteAsync(uri);

            return result.IsSuccessStatusCode;
        }
    }
}
