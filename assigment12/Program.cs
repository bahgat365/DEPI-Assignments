using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Assignment01_LINQ
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var products = ListGenerators.ProductList;
            var customers = ListGenerators.CustomerList;

            var outOfStock = products
                .Where(p => p.UnitsInStock == 0)
                .ToList();

            var inStockOver3 = products
                .Where(p => p.UnitsInStock > 0 && p.UnitPrice > 3.00m)
                .ToList();

            string[] digits =
            {
                "zero", "one", "two", "three", "four",
                "five", "six", "seven", "eight", "nine"
            };

            var shorterThanValue = digits
                .Where((name, value) => name.Length < value)
                .ToList();

            var firstOutOfStock = products
                .FirstOrDefault(p => p.UnitsInStock == 0);

            var firstOver1000 = products
                .FirstOrDefault(p => p.UnitPrice > 1000);

            int[] numbers =
            {
                5, 4, 1, 3, 9, 8, 6, 7, 2, 0
            };

            var secondGreaterThan5 = numbers
                .Where(n => n > 5)
                .Skip(1)
                .FirstOrDefault();

            var oddCount = numbers
                .Count(n => n % 2 != 0);

            var customerOrderCounts = customers
                .Select(c => new
                {
                    c.CompanyName,
                    OrderCount = c.Orders.Count()
                })
                .ToList();

            var categoryProductCounts = products
                .GroupBy(p => p.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    ProductCount = g.Count()
                })
                .ToList();

            var totalNumbers = numbers.Sum();

            var words = File.ReadAllLines("dictionary_english.txt");

            var totalCharacters = words.Sum(w => w.Length);
            var shortestWordLength = words.Min(w => w.Length);
            var longestWordLength = words.Max(w => w.Length);
            var averageWordLength = words.Average(w => w.Length);

            var productsByName = products
                .OrderBy(p => p.ProductName)
                .ToList();

            string[] mixedWords =
            {
                "aPPLE", "AbAcUs", "bRaNcH",
                "BlUeBeRrY", "ClOvEr", "cHeRry"
            };

            var caseInsensitiveSort = mixedWords
                .OrderBy(w => w, StringComparer.OrdinalIgnoreCase)
                .ToList();

            var productsByStockDescending = products
                .OrderByDescending(p => p.UnitsInStock)
                .ToList();

            var digitsByLengthThenName = digits
                .OrderBy(d => d.Length)
                .ThenBy(d => d)
                .ToList();

            var wordsByLengthThenCaseInsensitive = mixedWords
                .OrderBy(w => w.Length)
                .ThenBy(w => w, StringComparer.OrdinalIgnoreCase)
                .ToList();

            var productsByCategoryThenPrice = products
                .OrderBy(p => p.Category)
                .ThenByDescending(p => p.UnitPrice)
                .ToList();

            var wordsByLengthThenCaseInsensitiveDescending = mixedWords
                .OrderBy(w => w.Length)
                .ThenByDescending(w => w, StringComparer.OrdinalIgnoreCase)
                .ToList();

            var reversedDigitsWithSecondLetterI = digits
                .Where(d => d.Length > 1 && d[1] == 'i')
                .Reverse()
                .ToList();

            var productNames = products
                .Select(p => p.ProductName)
                .ToList();

            var wordCases = mixedWords
                .Select(w => new
                {
                    Upper = w.ToUpper(),
                    Lower = w.ToLower()
                })
                .ToList();

            var productProperties = products
                .Select(p => new
                {
                    p.ProductName,
                    p.Category,
                    Price = p.UnitPrice
                })
                .ToList();

            var numberMatchesPosition = numbers
                .Select((number, index) => number == index)
                .ToList();

            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };

            var pairs = numbersA
                .SelectMany(a => numbersB
                    .Where(b => a < b)
                    .Select(b => new
                    {
                        A = a,
                        B = b
                    }))
                .ToList();

            var ordersUnder500 = customers
                .SelectMany(c => c.Orders)
                .Where(o => o.Total < 500.00m)
                .ToList();

            var ordersFrom1998 = customers
                .SelectMany(c => c.Orders)
                .Where(o => o.OrderDate.Year >= 1998)
                .ToList();

            Console.WriteLine("1. Products out of stock:");
            foreach (var p in outOfStock)
                Console.WriteLine(p.ProductName);

            Console.WriteLine("\n2. Products in stock and price > 3:");
            foreach (var p in inStockOver3)
                Console.WriteLine($"{p.ProductName} - {p.UnitPrice}");

            Console.WriteLine("\n3. Digits whose name is shorter than their value:");
            foreach (var d in shorterThanValue)
                Console.WriteLine(d);

            Console.WriteLine("\n4. First product out of stock:");
            Console.WriteLine(firstOutOfStock?.ProductName);

            Console.WriteLine("\n5. First product with price > 1000:");
            Console.WriteLine(firstOver1000?.ProductName);

            Console.WriteLine("\n6. Second number greater than 5:");
            Console.WriteLine(secondGreaterThan5);

            Console.WriteLine("\n7. Number of odd numbers:");
            Console.WriteLine(oddCount);

            Console.WriteLine("\n8. Customers and order count:");
            foreach (var c in customerOrderCounts)
                Console.WriteLine($"{c.CompanyName} - {c.OrderCount}");

            Console.WriteLine("\n9. Categories and product count:");
            foreach (var c in categoryProductCounts)
                Console.WriteLine($"{c.Category} - {c.ProductCount}");

            Console.WriteLine("\n10. Total of numbers:");
            Console.WriteLine(totalNumbers);

            Console.WriteLine("\n11. Total characters in dictionary:");
            Console.WriteLine(totalCharacters);

            Console.WriteLine("\n12. Shortest word length:");
            Console.WriteLine(shortestWordLength);

            Console.WriteLine("\n13. Longest word length:");
            Console.WriteLine(longestWordLength);

            Console.WriteLine("\n14. Average word length:");
            Console.WriteLine(averageWordLength);

            Console.WriteLine("\n15. Products sorted by name:");
            foreach (var p in productsByName)
                Console.WriteLine(p.ProductName);

            Console.WriteLine("\n16. Case-insensitive word sort:");
            foreach (var w in caseInsensitiveSort)
                Console.WriteLine(w);

            Console.WriteLine("\n17. Products sorted by stock descending:");
            foreach (var p in productsByStockDescending)
                Console.WriteLine($"{p.ProductName} - {p.UnitsInStock}");

            Console.WriteLine("\n18. Digits sorted by length then name:");
            foreach (var d in digitsByLengthThenName)
                Console.WriteLine(d);

            Console.WriteLine("\n19. Words sorted by length then case-insensitive:");
            foreach (var w in wordsByLengthThenCaseInsensitive)
                Console.WriteLine(w);

            Console.WriteLine("\n20. Products sorted by category then price descending:");
            foreach (var p in productsByCategoryThenPrice)
                Console.WriteLine($"{p.Category} - {p.ProductName} - {p.UnitPrice}");

            Console.WriteLine("\n21. Words sorted by length then case-insensitive descending:");
            foreach (var w in wordsByLengthThenCaseInsensitiveDescending)
                Console.WriteLine(w);

            Console.WriteLine("\n22. Digits whose second letter is i, reversed:");
            foreach (var d in reversedDigitsWithSecondLetterI)
                Console.WriteLine(d);

            Console.WriteLine("\n23. Product names:");
            foreach (var name in productNames)
                Console.WriteLine(name);

            Console.WriteLine("\n24. Uppercase and lowercase words:");
            foreach (var w in wordCases)
                Console.WriteLine($"{w.Upper} - {w.Lower}");

            Console.WriteLine("\n25. Selected product properties:");
            foreach (var p in productProperties)
                Console.WriteLine($"{p.ProductName} - {p.Category} - {p.Price}");

            Console.WriteLine("\n26. Numbers matching their position:");
            foreach (var result in numberMatchesPosition)
                Console.WriteLine(result);

            Console.WriteLine("\n27. Pairs where A < B:");
            foreach (var pair in pairs)
                Console.WriteLine($"{pair.A} is less than {pair.B}");

            Console.WriteLine("\n28. Orders with total < 500:");
            foreach (var order in ordersUnder500)
                Console.WriteLine($"{order.OrderDate:d} - {order.Total}");

            Console.WriteLine("\n29. Orders from 1998 or later:");
            foreach (var order in ordersFrom1998)
                Console.WriteLine($"{order.OrderDate:d} - {order.Total}");
        }
    }
}
