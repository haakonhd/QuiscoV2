using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Quisco.Model;

namespace Quisco.DataAccess
{
	public class QuiscoContext : DbContext
	{
		public DbSet<Quiz> Quizes { get; set; }
		public DbSet<Question> Questions { get; set; }
		public DbSet<Answer> Answers { get; set; }

		public QuiscoContext(DbContextOptions<QuiscoContext> options) : base(options) { }

		public QuiscoContext() { }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			SqlConnectionStringBuilder connStringBuilder = new SqlConnectionStringBuilder();
			connStringBuilder = getConnStringBuilder();

			optionsBuilder.UseSqlServer(connStringBuilder.ConnectionString.ToString());
		}

		public static SqlConnectionStringBuilder getConnStringBuilder()
		{
			SqlConnectionStringBuilder connStringBuilder = new SqlConnectionStringBuilder
			{
				DataSource = "donau.hiof.no",
				InitialCatalog = "haakonhd",
				UserID = "haakonhd",
				Password = "BJe8ER3h"
			};
			return connStringBuilder;
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Question>()
				.HasOne(question => question.BelongingQuiz)
				.WithMany(quiz => quiz.Questions);

			modelBuilder.Entity<Answer>()
				.HasOne(a => a.BelongingQuestion)
				.WithMany(question => question.Answers);

		}
	}
}
