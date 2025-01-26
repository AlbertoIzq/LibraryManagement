using LibraryManagement.Business.Models.Domain;
using System.Net.Http.Json;

namespace LibraryManagementConsoleApp
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient { BaseAddress = new Uri("https://localhost:7206") };

        static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome to the Library Management System!");

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\nSelect an option:");
                Console.WriteLine("1. Add a Book");
                Console.WriteLine("2. Add a Member");
                Console.WriteLine("3. Borrow a Book");
                Console.WriteLine("4. Return a Book");
                Console.WriteLine("5. Display Overdue Books");
                Console.WriteLine("6. Exit");

                Console.Write("Enter your choice: ");
                var choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            await AddBookAsync();
                            break;
                        case "2":
                            await AddMemberAsync();
                            break;
                        case "3":
                            await BorrowBookAsync();
                            break;
                        case "4":
                            //await ReturnBookAsync();
                            break;
                        case "5":
                            //await DisplayOverdueBooksAsync();
                            break;
                        case "6":
                            exit = true;
                            Console.WriteLine("Exiting the application. Goodbye!");
                            break;
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }

        private static async Task AddBookAsync()
        {
            try
            {
                Console.Write("Enter book title: ");
                var title = Console.ReadLine();

                Console.Write("Enter book author: ");
                var author = Console.ReadLine();

                Console.Write("Enter book category: ");
                var category = Console.ReadLine();

                var book = new Book { Title = title, Author = author, Category = category };
                var response = await client.PostAsJsonAsync("/api/Books", book);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Book added successfully!");
                }
                else
                {
                    Console.WriteLine($"Failed to add the book. Status Code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while adding book: {ex.Message}");
            }
        }

        private static async Task AddMemberAsync()
        {
            try
            {
                Console.Write("Enter member name: ");
                var name = Console.ReadLine();

                var member = new Member { Name = name };
                var response = await client.PostAsJsonAsync("/api/Members", member);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Member added successfully!");
                }
                else
                {
                    Console.WriteLine($"Failed to add the member. Status Code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while adding member: {ex.Message}");
            }
        }

        private static async Task BorrowBookAsync()
        {
            try
            {
                Console.Write("Enter Member ID: ");
                if (!int.TryParse(Console.ReadLine(), out int memberId))
                {
                    Console.WriteLine("Invalid Member ID.");
                    return;
                }

                Console.Write("Enter Book ID: ");
                if (!int.TryParse(Console.ReadLine(), out int bookId))
                {
                    Console.WriteLine("Invalid Book ID.");
                    return;
                }

                var transaction = new Transaction { MemberId = memberId, BookId = bookId };
                var response = await client.PostAsJsonAsync("/api/Transactions/Borrow", transaction);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Book borrowed successfully!");
                }
                else
                {
                    Console.WriteLine($"Failed to borrow the book. Status Code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while borrowing book: {ex.Message}");
            }
        }
    }
}