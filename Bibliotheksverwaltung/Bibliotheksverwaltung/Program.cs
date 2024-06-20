using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Threading;

namespace Bibliotheksverwaltung
{
    public enum Language
    {
        English,
        German
    }

    class Program
    {
        static ResourceManager? resourceManager;
        static CultureInfo? cultureInfo;

        static void SetLanguage(Language language)
        {
            switch (language)
            {
                case Language.English:
                    cultureInfo = new CultureInfo("en");
                    break;
                case Language.German:
                    cultureInfo = new CultureInfo("de");
                    break;
                default:
                    cultureInfo = new CultureInfo("en");
                    break;
            }
            
        }

        public class Buch
        {
            public string Title { get; set; }
            public string Author { get; set; }
            public string ISBN { get; set; }
            public string Amount { get; set; }

            public Buch()
            {
                Title = "Titel";
                Author = "Autor";
                ISBN = "ISBN";
                Amount = "1";
            }

            public Buch(string title, string author, string isbn, string amount)
            {
                Title = title;
                Author = author;
                ISBN = isbn;
                Amount = amount;
            }

            public static bool check_isbn(string isbn)
            {
                return Regex.IsMatch(isbn, @"^97[89]-\d{1,5}-\d{1,7}-\d{1,7}-\d{1,7}$");
            }
        }

        public class Bibliothek
        {
            public List<Buch> books = new List<Buch>();
            private string filePath = @"C:\Users\FP2402389\source\repos\Bibliotheksverwaltung\Bibverwaltung_Save_File.txt";

            public Bibliothek()
            {
                load_from_file();
            }

            private void load_from_file()
            {
                if (File.Exists(filePath))
                {
                    string[] lines = File.ReadAllLines(filePath);
                    foreach (string line in lines)
                    {
                        string[] parts = line.Split('|');
                        if (parts.Length == 4)
                        {
                            string title = parts[0];
                            string author = parts[1];
                            string isbn = parts[2];
                            string amount = parts[3];
                            Buch book = new Buch(title, author, isbn, amount);
                            books.Add(book);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("File not found!");
                }
            }

            private void save_books_to_file()
            {
                List<string> lines = new List<string>();
                foreach (Buch book in books)
                {
                    string line = $"{book.Title}|{book.Author}|{book.ISBN}|{book.Amount}";
                    lines.Add(line);
                }
                File.WriteAllLines(filePath, lines);
            }

            public bool buch_add(Buch book)
            {
                if (Buch.check_isbn(book.ISBN))
                {
                    books.Add(book);
                    save_books_to_file();
                    return true;
                }
                return false;
            }
            public void buch_change_amount(Buch book)
            {
                if (resourceManager != null && cultureInfo != null)
                {
                    if (books.Contains(book))
                    {
                        int current_amount = Convert.ToInt32(book.Amount);

                        Console.WriteLine(resourceManager.GetString("OldAmount", cultureInfo) + $" {current_amount}");
                        Console.WriteLine();
                        Console.WriteLine(resourceManager.GetString("EnterChangeAmount", cultureInfo));
                        int change_amount;
                        if (!int.TryParse(input_reader(), out change_amount))
                        {
                            Console.WriteLine(resourceManager.GetString("ErrorInvalidAmount", cultureInfo));
                            return;
                        }

                        int new_amount = current_amount + change_amount;
                        if (new_amount < 0)
                        {
                            new_amount = 0;
                        }

                        book.Amount = new_amount.ToString();

                        Console.WriteLine(resourceManager.GetString("AmountChanged", cultureInfo) + $" {new_amount}");
                        save_books_to_file();
                    }
                    else
                    {
                        Console.WriteLine(resourceManager.GetString("ErrorBookNotFound", cultureInfo));
                    }
                }
                else
                {
                    Console.WriteLine("ResourceManager or CultureInfo is null.");
                }
            }
            public bool buch_delete(string title)
            {
                Buch? book = books.FirstOrDefault(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
                if (book != null)
                {
                    books.Remove(book);
                    save_books_to_file();
                    return true;
                }
                return false;
            }

            public List<Buch> buch_sort_by_title()
            {
                return books.OrderBy(book => book.Title).ToList();
            }

            public List<Buch> buch_sort_by_author()
            {
                return books.OrderBy(book => book.Author).ToList();
            }

            public List<Buch> buch_sort_by_isbn()
            {
                return books.OrderBy(book => book.ISBN).ToList();
            }

            public Buch? buch_search_title_or_isbn(string title_or_isbn)
            {
                return books.FirstOrDefault(book => book.Title.Equals(title_or_isbn, StringComparison.OrdinalIgnoreCase) || book.ISBN.Equals(title_or_isbn, StringComparison.OrdinalIgnoreCase));
            }

            public List<Buch> buch_search_author(string author)
            {
                return books.Where(book => book.Author.Equals(author, StringComparison.OrdinalIgnoreCase)).ToList();
            }
        }

        static void library_add(Bibliothek bibliothek)
        {
            if (resourceManager != null && cultureInfo != null)
            {
                Console.WriteLine();
                Console.Write(resourceManager.GetString("PromptTitle", cultureInfo) + ": ");
                string title = input_reader();
                Console.Write(resourceManager.GetString("PromptAuthor", cultureInfo) + ": ");
                string author = input_reader();
                Console.Write(resourceManager.GetString("PromptISBN", cultureInfo) + ": ");
                string isbn = input_reader();
                Console.Write(resourceManager.GetString("PromptAmount", cultureInfo) + ": ");
                string amount = input_reader();
                Console.WriteLine();

                bool already_exists = bibliothek.books.Any(book => book.ISBN.Equals(isbn, StringComparison.OrdinalIgnoreCase) ||
                                                                   book.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

                if (already_exists)
                {
                    Console.WriteLine(resourceManager.GetString("ErrorBookExists", cultureInfo));
                    library_change_amount(bibliothek);
                    Console.WriteLine();
                    return;
                }

                if (bibliothek.buch_add(new Buch(title, author, isbn, amount)))
                {
                    Console.WriteLine(resourceManager.GetString("SuccessBookAdded", cultureInfo));
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine(resourceManager.GetString("ErrorInvalidISBN", cultureInfo));
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("ResourceManager or CultureInfo is null.");
            }

        }

        static void library_delete(Bibliothek bibliothek)
        {
            if (resourceManager != null && cultureInfo != null)
            {
                Console.Write(resourceManager.GetString("PromptDeleteTitle", cultureInfo) + ": ");
                string title = input_reader();
                if (bibliothek.buch_delete(title))
                {
                    Console.WriteLine(resourceManager.GetString("SuccessBookDeleted", cultureInfo));
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine(resourceManager.GetString("ErrorBookNotFound", cultureInfo));
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("ResourceManager or CultureInfo is null.");
            }

        }

        static void library_show(Bibliothek bibliothek)
        {
            if (resourceManager != null && cultureInfo != null)
            {
                Console.WriteLine();
                Console.WriteLine(resourceManager.GetString("PromptSortBooks", cultureInfo));
                Console.WriteLine();
                Console.WriteLine("1. " + resourceManager.GetString("SortByTitle", cultureInfo));
                Console.WriteLine("2. " + resourceManager.GetString("SortByAuthor", cultureInfo));
                Console.WriteLine("3. " + resourceManager.GetString("SortByISBN", cultureInfo));
                Console.WriteLine();
                string choice = input_reader();
                Console.WriteLine();

                List<Buch> books = new List<Buch>();
                switch (choice)
                {
                    case "1":
                        books = bibliothek.buch_sort_by_title();
                        break;
                    case "2":
                        books = bibliothek.buch_sort_by_author();
                        break;
                    case "3":
                        books = bibliothek.buch_sort_by_isbn();
                        break;
                    default:
                        Console.WriteLine(resourceManager.GetString("ErrorInvalidChoice", cultureInfo));
                        books = bibliothek.buch_sort_by_title();
                        break;
                }

                foreach (var book in books)
                {
                    Console.WriteLine($"{book.Title} {resourceManager.GetString("By", cultureInfo)} {book.Author}, ISBN: {book.ISBN}, {resourceManager.GetString("DisplayAmount", cultureInfo)} {book.Amount}");
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("ResourceManager or CultureInfo is null.");
            }

        }

        static void library_search(Bibliothek bibliothek)
        {
            if (resourceManager != null && cultureInfo != null)
            {
                Console.WriteLine(resourceManager.GetString("PromptSearch", cultureInfo));
                Console.WriteLine();
                Console.WriteLine("1. " + resourceManager.GetString("SearchByTitle", cultureInfo));
                Console.WriteLine("2. " + resourceManager.GetString("SearchByAuthor", cultureInfo));
                Console.WriteLine("3. " + resourceManager.GetString("SearchByISBN", cultureInfo));
                Console.WriteLine();
                switch (input_reader())
                {
                    case "1":
                        Console.Write(resourceManager.GetString("PromptTitle", cultureInfo) + ": ");
                        string title = input_reader();
                        Buch? book_by_title = bibliothek.buch_search_title_or_isbn(title);
                        if (book_by_title != null)
                        {
                            Console.WriteLine();
                            Console.WriteLine($"{book_by_title.Title} {resourceManager.GetString("By", cultureInfo)} {book_by_title.Author}, ISBN: {book_by_title.ISBN}");
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine(resourceManager.GetString("ErrorBookNotFound", cultureInfo));
                            Console.WriteLine();
                        }
                        break;
                    case "2":
                        Console.Write(resourceManager.GetString("PromptAuthor", cultureInfo) + ": ");
                        string author = input_reader();
                        List<Buch> books_by_author = bibliothek.buch_search_author(author);
                        if (books_by_author.Count > 0)
                        {
                            Console.WriteLine();
                            foreach (var b in books_by_author)
                            {
                                Console.WriteLine($"{b.Title} {resourceManager.GetString("By", cultureInfo)} {b.Author}, ISBN: {b.ISBN}");
                            }
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine(resourceManager.GetString("ErrorNoBooksByAuthor", cultureInfo));
                            Console.WriteLine();
                        }
                        break;
                    case "3":
                        Console.Write(resourceManager.GetString("PromptISBN", cultureInfo) + ": ");
                        string isbn = input_reader();
                        Buch? book_by_isbn = bibliothek.buch_search_title_or_isbn(isbn);
                        if (book_by_isbn != null)
                        {
                            Console.WriteLine();
                            Console.WriteLine($"{book_by_isbn.Title} {resourceManager.GetString("By", cultureInfo)} {book_by_isbn.Author}, ISBN: {book_by_isbn.ISBN}");
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine(resourceManager.GetString("ErrorBookNotFound", cultureInfo));
                            Console.WriteLine();
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Console.WriteLine("ResourceManager or CultureInfo is null.");
            }

        }
        static void library_change_amount(Bibliothek bibliothek)
        {
            if (resourceManager != null && cultureInfo != null)
            {
                Console.WriteLine(resourceManager.GetString("PromptChangeAmount", cultureInfo));
                Console.WriteLine(resourceManager.GetString("PromptTitle", cultureInfo) + ": ");
                string title = input_reader();
                Console.WriteLine();
                Buch? book = bibliothek.books.FirstOrDefault(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

                if (book != null)
                {
                    Console.WriteLine($"{book.Title} {resourceManager.GetString("By", cultureInfo)} {book.Author}, ISBN: {book.ISBN}");
                    bibliothek.buch_change_amount(book);
                }
                else
                {
                    Console.WriteLine(resourceManager.GetString("ErrorBookNotFound", cultureInfo));
                }
            }
            else
            {
                Console.WriteLine("ResourceManager or CultureInfo is null.");
            }
        }
        static void language_select()
        {
            if (resourceManager != null && cultureInfo != null)
            {
                Console.WriteLine(resourceManager.GetString("SelectLanguage", cultureInfo));
                Console.WriteLine();
                Console.WriteLine("1. English");
                Console.WriteLine("2. Deutsch");
                Console.WriteLine();
                string choice = input_reader();

                switch (choice)
                {
                    case "1":
                        SetLanguage(Language.English);
                        Console.WriteLine("Now I use: English");
                        break;
                    case "2":
                        SetLanguage(Language.German);
                        Console.WriteLine("Nun benutze ich: Deutsch");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Defaulting to English.");
                        SetLanguage(Language.English);
                        break;
                }
            }
            else
            {
                Console.WriteLine("ResourceManager or CultureInfo is null.");
            }

        }

        public static string input_reader()
        {
            string? input = Console.ReadLine();
            if (input == null || string.IsNullOrWhiteSpace(input))
            {
                return input = "Invalid input";
            }
            return input;
        }

        static void menue_main()
        {
            if (resourceManager != null && cultureInfo != null)
            {
                Bibliothek bibliothek = new Bibliothek();
                bool quit = false;
                while (!quit)
                {
                    Console.WriteLine();
                    Console.WriteLine(resourceManager.GetString("LibraryManagement", cultureInfo));
                    Console.WriteLine(resourceManager.GetString("ChooseOption", cultureInfo));
                    Console.WriteLine();
                    Console.WriteLine("0. " + resourceManager.GetString("SelectLanguage", cultureInfo));
                    Console.WriteLine("1. " + resourceManager.GetString("MenuAddBook", cultureInfo));
                    Console.WriteLine("2. " + resourceManager.GetString("MenuDeleteBook", cultureInfo));
                    Console.WriteLine("3. " + resourceManager.GetString("MenuShowBooks", cultureInfo));
                    Console.WriteLine("4. " + resourceManager.GetString("MenuSearchBook", cultureInfo));
                    Console.WriteLine("5. " + resourceManager.GetString("MenuChangeAmount", cultureInfo));
                    Console.WriteLine("6. " + resourceManager.GetString("MenuExit", cultureInfo));
                    Console.WriteLine();

                    string choice = input_reader();
                    switch (choice)
                    {
                        case "0":
                            language_select();
                            break;
                        case "1":
                            library_add(bibliothek);
                            break;
                        case "2":
                            library_delete(bibliothek);
                            break;
                        case "3":
                            library_show(bibliothek);
                            break;
                        case "4":
                            library_search(bibliothek);
                            break;
                        case "5":
                            library_change_amount(bibliothek);
                            break;
                        case "6":
                            quit = true;
                            break;
                        default:
                            Console.WriteLine(resourceManager.GetString("ErrorInvalidChoice", cultureInfo));
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("ResourceManager or CultureInfo is null.");
            }
        }

        static void Main(string[] args)
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("de-DE");
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("de-DE");

            resourceManager = new ResourceManager("Bibliotheksverwaltung.Resources.Resources", typeof(Program).Assembly);
            SetLanguage(Language.German);
            menue_main();
        }
    }
}