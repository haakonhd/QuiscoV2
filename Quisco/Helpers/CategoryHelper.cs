using System.ComponentModel.DataAnnotations;

namespace Quisco.Helpers
{
    public class CategoryHelper
    {
        public string[] QuizCategories { get; set; }

        public CategoryHelper()
        {
            this.QuizCategories = new string[]
            {
                "General Knowledge",
                "Books",
                "Film",
                "Music",
                "Theatres",
                "Television",
                "Video Games",
                "BoardGames",
                "Science & Nature",
                "Computers",
                "Math",
                "Mythology",
                "Sports",
                "Geography",
                "History",
                "Politics",
                "Art",
                "Celebrities",
                "Animals",
                "Vehicles",
                "Comics",
                "Gadgets",
                "Anime And Manga",
                "Cartoons And Animations"
            };
        }

        public int GetCategoryNumberFromString(string category)
        {

            return 0;
        }

        public enum Category
        {
            GeneralKnowledge = 9,
            Books,
            Film,
            Music,
            Theatres,
            Television,
            VideoGames,
            BoardGames,
            ScienceAndNature,
            ScienceComputers,
            ScienceMath,
            Mythology,
            Sports,
            Geography,
            History,
            Politics,
            Art,
            Celebrities,
            Animals,
            Vehicles,
            Comics,
            Gadgets,
            AnimeAndManga,
            CartoonsAndAnimations
        }

    }
}
