using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Web.Http;
using Newtonsoft.Json;
using Quisco.Model;

namespace Quisco.DataAccess
{
    public class QuizRequest
    {
        static Uri quizzesBaseUri = new Uri("http://localhost:55418/api/Quizzes");
        HttpClient httpClient = new HttpClient();

        public async Task<bool> AddQuizToDbAsync(Quiz quiz)
        {
            var json = JsonConvert.SerializeObject(quiz, Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            var result = await httpClient.PostAsync(quizzesBaseUri, new HttpStringContent(json, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));

            return result.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateQuizAsync(Quiz quiz)
        {
            var json = JsonConvert.SerializeObject(quiz, Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            var result = await httpClient.PutAsync(quizzesBaseUri,
                new HttpStringContent(json, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));
            return result.IsSuccessStatusCode;
        }

        public async Task<Quiz[]> GetQuizzesFromIdHash(string userIdHash)
        {
            UriBuilder quizzesBaseUriBuilder = new UriBuilder("http://localhost:55418/api/Quizzes/userIdHash/" + userIdHash);
            //            quizzesBaseUriBuilder.Query = "UserIdHash=" + userIdHash + ";

            var result = await httpClient.GetAsync(quizzesBaseUriBuilder.Uri);
            var json = await result.Content.ReadAsStringAsync();
            var quizList = JsonConvert.DeserializeObject<Quiz[]>(json);
            return quizList;
        }

        public async Task<ObservableCollection<Quiz>> GetCompleteQuizzes(Quiz[] quizzes)
        {
            ObservableCollection<Quiz> result = new ObservableCollection<Quiz>();
            foreach (Quiz quiz in quizzes)
            {
                result.Add(await GetCompleteQuiz(quiz));
            }

            return result;
        }

        //returns the quiz with all its questions and answers
        public async Task<Quiz> GetCompleteQuiz(Quiz quiz)
        {
            QuestionRequest questionRequest = new QuestionRequest();
            AnswerRequest answerRequest = new AnswerRequest();

            var questionsTask = await questionRequest.GetQuestionListByQuizId(quiz.QuizId).ConfigureAwait(true);
            Question[] questions = questionsTask;
            foreach (Question question in questions)
            {
                var answersTask = await answerRequest.GetAnswerListByQuestionId(question.QuestionId).ConfigureAwait(true);
                Answer[] answers = answersTask;

                foreach (Answer answer in answers)
                {
                    question.Answers.Add(answer);
                }

                quiz.Questions.Add(question);
            }
            return quiz;
        }

        public async Task<Quiz[]> GetQuizList()
        {
            var result = await httpClient.GetAsync(quizzesBaseUri);
            var json = await result.Content.ReadAsStringAsync();
            var quizList = JsonConvert.DeserializeObject<Quiz[]>(json);
            return quizList;
        }
    }
}
