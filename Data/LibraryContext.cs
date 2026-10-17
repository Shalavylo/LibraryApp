using LibraryApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Data
{
    public class LibraryContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;
                                        Database=LibraryAppDb;
                                        Trusted_Connection=True;
                                        TrustServerCertificate=True;");
        }

        public DbSet<Author> Authors => Set<Author>();
        public DbSet<Book> Books => Set<Book>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, FullName = "Тарас Шевченко" },
                new Author { Id = 2, FullName = "Леся Українка" },
                new Author { Id = 3, FullName = "Іван Франко" },
                new Author { Id = 4, FullName = "Михайло Коцюбинський" },
                new Author { Id = 5, FullName = "Ольга Кобилянська" },
                new Author { Id = 6, FullName = "Джордж Орвелл" },
                new Author { Id = 7, FullName = "Джейн Остін" },
                new Author { Id = 8, FullName = "Ернест Гемінґвей" }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "Кобзар", Year = 1840, AuthorId = 1 },
                new Book { Id = 2, Title = "Гайдамаки", Year = 1841, AuthorId = 1 },
                new Book { Id = 3, Title = "Катерина", Year = 1838, AuthorId = 1 },
                new Book { Id = 4, Title = "Лісова пісня", Year = 1911, AuthorId = 2 },
                new Book { Id = 5, Title = "Бояриня", Year = 1910, AuthorId = 2 },
                new Book { Id = 6, Title = "Кассандра", Year = 1908, AuthorId = 2 },
                new Book { Id = 7, Title = "Захар Беркут", Year = 1883, AuthorId = 3 },
                new Book { Id = 8, Title = "Мойсей", Year = 1905, AuthorId = 3 },
                new Book { Id = 9, Title = "Перехресні стежки", Year = 1900, AuthorId = 3 },
                new Book { Id = 10, Title = "Тіні забутих предків", Year = 1911, AuthorId = 4 },
                new Book { Id = 11, Title = "Fata morgana", Year = 1903, AuthorId = 4 },
                new Book { Id = 12, Title = "Земля", Year = 1902, AuthorId = 5 },
                new Book { Id = 13, Title = "1984", Year = 1949, AuthorId = 6 },
                new Book { Id = 14, Title = "Колгосп тварин", Year = 1945, AuthorId = 6 },
                new Book { Id = 15, Title = "Гордість і упередження", Year = 1813, AuthorId = 7 },
                new Book { Id = 16, Title = "Розум і почуття", Year = 1811, AuthorId = 7 },
                new Book { Id = 17, Title = "Старий і море", Year = 1952, AuthorId = 8 },
                new Book { Id = 18, Title = "Прощавай, зброє", Year = 1929, AuthorId = 8 }
            );
        }
    }
}
