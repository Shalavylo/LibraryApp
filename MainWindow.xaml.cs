using LibraryApp.Data;
using LibraryApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LibraryApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _isInitializing;
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _isInitializing = true;
            try
            {
                using var db = new LibraryContext();
                var authors = await db.Authors.AsNoTracking().OrderBy(a => a.FullName)
                    .ToListAsync();

                cmbAuthors.ItemsSource = authors;

                if(authors.Count > 0)
                {
                    cmbAuthors.SelectedIndex = 0;
                }
                else
                {
                    txtStatus.Text = "У базі даних немає авторів.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не вдалося завантажити авторів.\n" + ex.Message,
                    "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);

                txtStatus.Text = "Помилка завантаження авторів.";
            }
            finally { 
                _isInitializing = false; 
            }

            if(cmbAuthors.SelectedItem is Author author)
            {
                await LoadBooksByAuthorAsync(author.Id);
            }
        }

        private async Task LoadBooksByAuthorAsync(int authorId)
        {
            try
            {
                txtStatus.Text = "Завантаження книг...";
                using var db = new LibraryContext();

                var books = await db.Books.AsNoTracking().
                    Include(a => a.Author).Where(a => a.AuthorId == authorId).
                    OrderBy(a => a.Title).ToListAsync();

                dgBooks.ItemsSource = books;
                txtStatus.Text = $"Знайдено книг: {books.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                   "Не вдалося завантажити книги.\n" + ex.Message,
                   "Помилка",
                   MessageBoxButton.OK,
                   MessageBoxImage.Error);

                txtStatus.Text = "Помилка завантаження книг.";
            }
        }

        private async void CmbAuthors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_isInitializing)
                return;

            if(cmbAuthors.SelectedItem is Author author)
            {
                txtSearch.Clear();
                await LoadBooksByAuthorAsync(author.Id);
            }
        }

        private async void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtSearch.Text.Trim();
            if(searchText.Length < 3)
            {
                MessageBox.Show(
                    "Введіть щонайменше 3 символи для пошуку.",
                    "Увага",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            try
            {
                txtStatus.Text = "Пошук книг...";
                using var db = new LibraryContext();

                var books = await db.Books
                    .AsNoTracking()
                    .Include(b => b.Author)
                    .Where(b => b.Title.Contains(searchText))
                    .OrderBy(b => b.Title)
                    .ToListAsync();

                dgBooks.ItemsSource = books;

                txtStatus.Text = $"Результат пошуку: {books.Count} книг.";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Помилка пошуку книг.\n" + ex.Message,
                    "Помилка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

                txtStatus.Text = "Помилка пошуку.";
            }
        }
    }
}