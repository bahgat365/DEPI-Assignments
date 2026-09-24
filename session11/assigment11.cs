using System;
using System.Collections.Generic;

namespace Assignment08
{
    public class Book
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string[] Authors { get; set; }
        public DateTime PublicationDate { get; set; }
        public decimal Price { get; set; }

        public Book(string isbn, string title, string[] authors, DateTime publicationDate, decimal price)
        {
            ISBN = isbn;
            Title = title;
            Authors = authors;
            PublicationDate = publicationDate;
            Price = price;
        }

        public override string ToString()
        {
            return $"{Title} | {ISBN} | {Price:C} | {PublicationDate:d}";
        }
    }

    public class BookFunctions
    {
        public static string GetTitle(Book book)
        {
            return book.Title;
        }

        public static string GetAuthors(Book book)
        {
            return string.Join(", ", book.Authors);
        }

        public static decimal GetPrice(Book book)
        {
            return book.Price;
        }

        public static string GetISBN(Book book)
        {
            return book.ISBN;
        }

        public static DateTime GetPublicationDate(Book book)
        {
            return book.PublicationDate;
        }
    }

    public delegate string BookFunction(Book book);

    public class LibraryEngine
    {
        public static void ProcessBooks(List<Book> books, BookFunction function)
        {
            foreach (Book book in books)
                Console.WriteLine(function(book));

            Console.WriteLine();
        }

        public static void ProcessBooks(List<Book> books, Func<Book, decimal> function)
        {
            foreach (Book book in books)
                Console.WriteLine(function(book));

            Console.WriteLine();
        }
    }

    public class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public Order(int id, string customerName, decimal price, int quantity)
        {
            Id = id;
            CustomerName = customerName;
            Price = price;
            Quantity = quantity;
        }

        public override string ToString()
        {
            return $"Order {Id} - Customer: {CustomerName}, Price: {Price:C}, Quantity: {Quantity}";
        }
    }

    public delegate decimal PriceCalculator(Order order);

    public class OrderService
    {
        public event Action<Order> OrderProcessed;

        public decimal CalculateOrderPrice(Order order, PriceCalculator calculator)
        {
            return calculator(order);
        }

        public decimal CalculateOrderPrice(Order order, Func<Order, decimal> calculator)
        {
            return calculator(order);
        }

        public bool ValidateOrder(Order order, Predicate<Order> validationRule)
        {
            return validationRule(order);
        }

        public void ProcessOrder(Order order, Action<Order> action)
        {
            action(order);
            OrderProcessed?.Invoke(order);
        }

        public decimal CalculatePrice(Order order, Func<Order, decimal> pricingStrategy)
        {
            return pricingStrategy(order);
        }
    }

    internal class Program
    {
        static decimal CalculateTotal(Order order)
        {
            return order.Price * order.Quantity;
        }

        static decimal CalculateTotalWithDiscount(Order order)
        {
            decimal total = order.Price * order.Quantity;
            return total - total * 0.10m;
        }

        static void PrintOrder(Order order)
        {
            Console.WriteLine(order);
        }

        static void SendConfirmation(Order order)
        {
            Console.WriteLine($"Confirmation sent to {order.CustomerName}.");
        }

        static void WriteAudit(Order order)
        {
            Console.WriteLine($"Audit: Order {order.Id} was processed.");
        }

        static void Handler1(Order order)
        {
            Console.WriteLine($"Handler 1: Order {order.Id} completed.");
        }

        static void Handler2(Order order)
        {
            Console.WriteLine($"Handler 2: Notification sent for order {order.Id}.");
        }

        static void Handler3(Order order)
        {
            Console.WriteLine($"Handler 3: Audit recorded for order {order.Id}.");
        }

        static void Main(string[] args)
        {
            List<Book> books = new List<Book>
            {
                new Book(
                    "978-0132350884",
                    "Clean Code",
                    new[] { "Robert C. Martin" },
                    new DateTime(2008, 8, 1),
                    35m),

                new Book(
                    "978-0201633610",
                    "Design Patterns",
                    new[] { "Erich Gamma", "Richard Helm", "Ralph Johnson", "John Vlissides" },
                    new DateTime(1994, 10, 31),
                    45m),

                new Book(
                    "978-0135957059",
                    "The Pragmatic Programmer",
                    new[] { "David Thomas", "Andrew Hunt" },
                    new DateTime(2019, 9, 13),
                    40m)
            };

            Console.WriteLine("Book Titles");
            LibraryEngine.ProcessBooks(books, BookFunctions.GetTitle);

            Console.WriteLine("Book Authors");
            LibraryEngine.ProcessBooks(books, BookFunctions.GetAuthors);

            Console.WriteLine("Book Prices");
            LibraryEngine.ProcessBooks(books, BookFunctions.GetPrice);

            Console.WriteLine("Book ISBNs");
            LibraryEngine.ProcessBooks(books, delegate (Book book)
            {
                return BookFunctions.GetISBN(book);
            });

            Console.WriteLine("Publication Dates");
            LibraryEngine.ProcessBooks(books, book => BookFunctions.GetPublicationDate(book).ToShortDateString());

            Order order = new Order(101, "Mohammed", 250m, 3);
            OrderService orderService = new OrderService();

            Console.WriteLine("User Defined Delegate");

            PriceCalculator normalPrice = CalculateTotal;
            PriceCalculator discountPrice = CalculateTotalWithDiscount;

            Console.WriteLine(orderService.CalculateOrderPrice(order, normalPrice));
            Console.WriteLine(orderService.CalculateOrderPrice(order, discountPrice));

            Console.WriteLine();
            Console.WriteLine("Func");

            Func<Order, decimal> price = x => x.Price * x.Quantity;
            Func<Order, decimal> priceWithDiscount = x => x.Price * x.Quantity * 0.90m;

            Console.WriteLine(orderService.CalculateOrderPrice(order, price));
            Console.WriteLine(orderService.CalculateOrderPrice(order, priceWithDiscount));

            Console.WriteLine();
            Console.WriteLine("Predicate");

            bool validQuantity = orderService.ValidateOrder(order, x => x.Quantity > 0);
            bool validPrice = orderService.ValidateOrder(order, x => x.Price > 0);
            bool validCustomer = orderService.ValidateOrder(order, x => !string.IsNullOrWhiteSpace(x.CustomerName));

            Console.WriteLine($"Quantity valid: {validQuantity}");
            Console.WriteLine($"Price valid: {validPrice}");
            Console.WriteLine($"Customer valid: {validCustomer}");

            Console.WriteLine();
            Console.WriteLine("Actions");

            Action<Order> printOrder = PrintOrder;
            Action<Order> confirmation = SendConfirmation;
            Action<Order> audit = WriteAudit;

            orderService.ProcessOrder(order, printOrder);
            orderService.ProcessOrder(order, confirmation);
            orderService.ProcessOrder(order, audit);

            Console.WriteLine();
            Console.WriteLine("Events");

            orderService.OrderProcessed += Handler1;
            orderService.OrderProcessed += Handler2;
            orderService.OrderProcessed += Handler3;

            orderService.ProcessOrder(order, x => Console.WriteLine($"Order {x.Id} was processed."));

            Console.WriteLine();
            Console.WriteLine("After Unsubscribe");

            orderService.OrderProcessed -= Handler1;

            orderService.ProcessOrder(order, x => Console.WriteLine($"Order {x.Id} was processed again."));

            Console.WriteLine();
            Console.WriteLine("Pricing Strategies");

            Func<Order, decimal> normalPricing = x => x.Price * x.Quantity;
            Func<Order, decimal> tenPercentDiscount = x => x.Price * x.Quantity * 0.90m;
            Func<Order, decimal> twentyPercentDiscount = x => x.Price * x.Quantity * 0.80m;
            Func<Order, decimal> vipPricing = x => x.Price * x.Quantity * 0.75m;

            Console.WriteLine($"Normal: {orderService.CalculatePrice(order, normalPricing):C}");
            Console.WriteLine($"10% Discount: {orderService.CalculatePrice(order, tenPercentDiscount):C}");
            Console.WriteLine($"20% Discount: {orderService.CalculatePrice(order, twentyPercentDiscount):C}");
            Console.WriteLine($"VIP: {orderService.CalculatePrice(order, vipPricing):C}");

            Console.WriteLine();
            Console.WriteLine("Questions");

            Console.WriteLine("PriceCalculator is a user-defined delegate with a specific name and signature.");
            Console.WriteLine("Func<Order, decimal> is a built-in generic delegate that returns decimal.");
            Console.WriteLine("Action<Order> accepts an Order and does not return a value.");
            Console.WriteLine("Func<Order, decimal> accepts an Order and returns a decimal.");
            Console.WriteLine("Predicate<Order> is intended for validation and returns bool.");
            Console.WriteLine("A delegate can be called directly, while an event controls how subscribers receive notifications.");
            Console.WriteLine("External code cannot normally invoke an event because only the declaring class can raise it.");
            Console.WriteLine("Multiple event handlers are invoked when the event is raised.");
            Console.WriteLine("+= adds a handler to the event invocation list.");
            Console.WriteLine("event Action<Order> protects the delegate from being invoked directly by outside code.");
        }
    }
}
