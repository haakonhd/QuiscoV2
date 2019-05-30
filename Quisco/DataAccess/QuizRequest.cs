using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;
using Newtonsoft.Json;
using Quisco.Model;
using HttpClient = Windows.Web.Http.HttpClient;

namespace Quisco.DataAccess
{
    public class QuizRequest
    {
        static Uri quizzesBaseUri = new Uri("http://localhost:55418/api/Quizzes/");
        HttpClient httpClient = new HttpClient();

        public async Task<bool> AddQuizToDbAsync(Quiz quiz)
        {
            var json = JsonConvert.SerializeObject(quiz, Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            var result = await httpClient.PostAsync(quizzesBaseUri,
                new HttpStringContent(json, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));

            return result.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateQuizCascadingAsync(Quiz quiz)
        {
            QuestionRequest questionRequest = new QuestionRequest();
            AnswerRequest answerRequest = new AnswerRequest();

            bool updateQuizSucceeded = await UpdateQuiz(quiz).ConfigureAwait(true);
            if (!updateQuizSucceeded) return false;
            foreach (var question in quiz.Questions)
            {
                //Updates question
                bool updateQuestionSucceeded = await questionRequest.UpdateQuestion(question).ConfigureAwait(true);
                if (!updateQuestionSucceeded) return false;
                // updateQuestion() also creates new answers for new questions
                if (question.QuestionId != 0)
                {
                    foreach (var answer in question.Answers)
                    {
                        //Updates answer
                        bool updateAnswerSucceeded = await answerRequest.UpdateAnswer(answer).ConfigureAwait(true);
                        if (!updateAnswerSucceeded) return false;
                    }

                }
            }

            return true;
        }



        public async Task<bool> UpdateQuiz(Quiz quiz)
        {
            //Updates quiz
            Uri quizUri = new Uri(quizzesBaseUri + quiz.QuizId.ToString());
            string json;

            json = JsonConvert.SerializeObject(quiz, Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            var httpContent = new HttpStringContent(json, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json");
            var result = await httpClient.PutAsync(quizUri, httpContent);
            return result.IsSuccessStatusCode;
        }


        public async Task<Quiz[]> GetQuizzesFromIdHashAsync(string userIdHash)
        {
            UriBuilder quizzesBaseUriBuilder = new UriBuilder("http://localhost:55418/api/Quizzes/userIdHash/" + userIdHash);
            //            quizzesBaseUriBuilder.Query = "UserIdHash=" + userIdHash + ";

            var result = await httpClient.GetAsync(quizzesBaseUriBuilder.Uri);
            var json = await result.Content.ReadAsStringAsync();
            var quizList = JsonConvert.DeserializeObject<Quiz[]>(json);
            return quizList;
        }

        public async Task<ObservableCollection<Quiz>> GetCompleteQuizzesAsync(Quiz[] quizzes)
        {
            ObservableCollection<Quiz> result = new ObservableCollection<Quiz>();
            foreach (Quiz quiz in quizzes)
            {
                result.Add(await GetCompleteQuizAsync(quiz));
            }

            return result;
        }

        //returns the quiz with all its questions and answers
        public async Task<Quiz> GetCompleteQuizAsync(Quiz quiz)
        {
            QuestionRequest questionRequest = new QuestionRequest();
            AnswerRequest answerRequest = new AnswerRequest();

            var questionsTask = await questionRequest.GetQuestionListByQuizIdAsync(quiz.QuizId).ConfigureAwait(true);
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

        public async Task<Quiz[]> GetQuizListAsync()
        {
            var result = await httpClient.GetAsync(quizzesBaseUri);
            var json = await result.Content.ReadAsStringAsync();
            var quizList = JsonConvert.DeserializeObject<Quiz[]>(json);
            return quizList;
        }

        public async Task<bool> DeleteQuizAsync(Quiz quiz)
        {
            Uri uri = new Uri(quizzesBaseUri  + quiz.QuizId.ToString());
            var result = await httpClient.DeleteAsync(uri);

            return result.IsSuccessStatusCode;
        }
    }
}
