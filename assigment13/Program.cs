using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Assignment02_LINQ
{
    class AnagramComparer : IEqualityComparer<string>
    {
        private string Normalize(string value)
        {
            return new string(
                value.Trim()
                    .ToLower()
                    .OrderBy(c => c)
                    .ToArray());
        }

        public bool Equals(string x, string y)
        {
            return Normalize(x) == Normalize(y);
        }

        public int GetHashCode(string obj)
        {
            return Normalize(obj).GetHashCode();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var products = ListGenerators.ProductList;
            var customers = ListGenerators.CustomerList;

            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            var firstOutOfStock = products
                .FirstOrDefault(p => p.UnitsInStock == 0);

            var firstOver1000 = products
                .FirstOrDefault(p => p.UnitPrice > 1000);

            var secondGreaterThan5 = numbers
                .Where(n => n > 5)
                .Skip(1)
                .FirstOrDefault();

            var oddCount = numbers.Count(n => n % 2 != 0);

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

            var categoryStockTotals = products
                .GroupBy(p => p.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    TotalUnitsInStock = g.Sum(p => p.UnitsInStock)
                })
                .ToList();

            var cheapestByCategory = products
                .GroupBy(p => p.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    CheapestPrice = g.Min(p => p.UnitPrice)
                })
                .ToList();

            var cheapestProductsByCategory = products
                .GroupBy(p => p.Category)
                .SelectMany(g =>
                    from p in g
                    let minPrice = g.Min(x => x.UnitPrice)
                    where p.UnitPrice == minPrice
                    select new
                    {
                        Category = g.Key,
                        p.ProductName,
                        p.UnitPrice
                    })
                .ToList();

            var highestByCategory = products
                .GroupBy(p => p.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    HighestPrice = g.Max(p => p.UnitPrice)
                })
                .ToList();

            var highestProductsByCategory = products
                .GroupBy(p => p.Category)
                .SelectMany(g =>
                    from p in g
                    let maxPrice = g.Max(x => x.UnitPrice)
                    where p.UnitPrice == maxPrice
                    select new
                    {
                        Category = g.Key,
                        p.ProductName,
                        p.UnitPrice
                    })
                .ToList();

            var averageByCategory = products
                .GroupBy(p => p.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    AveragePrice = g.Average(p => p.UnitPrice)
                })
                .ToList();

            var uniqueCategories = products
                .Select(p => p.Category)
                .Distinct()
                .ToList();

            var productFirstLetters = products
                .Select(p => p.ProductName[0]);

            var customerFirstLetters = customers
                .Select(c => c.CompanyName[0]);

            var uniqueFirstLetters = productFirstLetters
                .Union(customerFirstLetters)
                .ToList();

            var commonFirstLetters = productFirstLetters
                .Intersect(customerFirstLetters)
                .ToList();

            var productOnlyFirstLetters = productFirstLetters
                .Except(customerFirstLetters)
                .ToList();

            var lastThreeCharacters = products
                .Select(p => p.ProductName.Substring(Math.Max(0, p.ProductName.Length - 3)))
                .Concat(
                    customers.Select(c =>
                        c.CompanyName.Substring(Math.Max(0, c.CompanyName.Length - 3))))
                .ToList();

            var washingtonOrders = customers
                .Where(c => c.Region == "WA")
                .SelectMany(c => c.Orders)
                .Take(3)
                .ToList();

            var washingtonOrdersExceptFirstTwo = customers
                .Where(c => c.Region == "WA")
                .SelectMany(c => c.Orders)
                .Skip(2)
                .ToList();

            var numbersUntilLessThanPosition = numbers
                .TakeWhile((n, index) => n >= index)
                .ToList();

            var numbersFromFirstDivisibleBy3 = numbers
                .SkipWhile(n => n % 3 != 0)
                .ToList();

            var numbersFromFirstLessThanPosition = numbers
                .SkipWhile((n, index) => n >= index)
                .ToList();

            var containsEi = words.Any(w =>
                w.Contains("ei", StringComparison.OrdinalIgnoreCase));

            var categoriesWithOutOfStock = products
                .GroupBy(p => p.Category)
                .Where(g => g.Any(p => p.UnitsInStock == 0))
                .ToList();

            var categoriesWithAllInStock = products
                .GroupBy(p => p.Category)
                .Where(g => g.All(p => p.UnitsInStock > 0))
                .ToList();

            var groupedNumbers = numbers
                .Concat(Enumerable.Range(10, 6))
                .GroupBy(n => n % 5)
                .ToList();

            var groupedWordsByFirstLetter = words
                .GroupBy(w => w[0])
                .OrderBy(g => g.Key)
                .ToList();

            string[] anagramWords =
            {
                "from", "salt", "earn", " last", "near", "form"
            };

            var groupedAnagrams = anagramWords
                .GroupBy(w => w, new AnagramComparer())
                .ToList();

            Console.WriteLine("1. First product out of stock:");
            Console.WriteLine(firstOutOfStock?.ProductName);

            Console.WriteLine("\n2. First product with price > 1000:");
            Console.WriteLine(firstOver1000?.ProductName);

            Console.WriteLine("\n3. Second number greater than 5:");
            Console.WriteLine(secondGreaterThan5);

            Console.WriteLine("\n4. Number of odd numbers:");
            Console.WriteLine(oddCount);

            Console.WriteLine("\n5. Customers and order count:");
            foreach (var item in customerOrderCounts)
                Console.WriteLine($"{item.CompanyName} - {item.OrderCount}");

            Console.WriteLine("\n6. Categories and product count:");
            foreach (var item in categoryProductCounts)
                Console.WriteLine($"{item.Category} - {item.ProductCount}");

            Console.WriteLine("\n7. Total of numbers:");
            Console.WriteLine(totalNumbers);

            Console.WriteLine("\n8. Total characters in dictionary:");
            Console.WriteLine(totalCharacters);

            Console.WriteLine("\n9. Shortest word length:");
            Console.WriteLine(shortestWordLength);

            Console.WriteLine("\n10. Longest word length:");
            Console.WriteLine(longestWordLength);

            Console.WriteLine("\n11. Average word length:");
            Console.WriteLine(averageWordLength);

            Console.WriteLine("\n12. Total units in stock by category:");
            foreach (var item in categoryStockTotals)
                Console.WriteLine($"{item.Category} - {item.TotalUnitsInStock}");

            Console.WriteLine("\n13. Cheapest price by category:");
            foreach (var item in cheapestByCategory)
                Console.WriteLine($"{item.Category} - {item.CheapestPrice}");

            Console.WriteLine("\n14. Cheapest products by category:");
            foreach (var item in cheapestProductsByCategory)
                Console.WriteLine($"{item.Category} - {item.ProductName} - {item.UnitPrice}");

            Console.WriteLine("\n15. Highest price by category:");
            foreach (var item in highestByCategory)
                Console.WriteLine($"{item.Category} - {item.HighestPrice}");

            Console.WriteLine("\n16. Highest products by category:");
            foreach (var item in highestProductsByCategory)
                Console.WriteLine($"{item.Category} - {item.ProductName} - {item.UnitPrice}");

            Console.WriteLine("\n17. Average price by category:");
            foreach (var item in averageByCategory)
                Console.WriteLine($"{item.Category} - {item.AveragePrice}");

            Console.WriteLine("\n18. Unique categories:");
            foreach (var item in uniqueCategories)
                Console.WriteLine(item);

            Console.WriteLine("\n19. Unique first letters:");
            foreach (var item in uniqueFirstLetters)
                Console.WriteLine(item);

            Console.WriteLine("\n20. Common first letters:");
            foreach (var item in commonFirstLetters)
                Console.WriteLine(item);

            Console.WriteLine("\n21. Product first letters not found in customer names:");
            foreach (var item in productOnlyFirstLetters)
                Console.WriteLine(item);

            Console.WriteLine("\n22. Last three characters of all names:");
            foreach (var item in lastThreeCharacters)
                Console.WriteLine(item);

            Console.WriteLine("\n23. First 3 orders from Washington customers:");
            foreach (var order in washingtonOrders)
                Console.WriteLine($"{order.OrderDate:d} - {order.Total}");

            Console.WriteLine("\n24. Washington orders except first 2:");
            foreach (var order in washingtonOrdersExceptFirstTwo)
                Console.WriteLine($"{order.OrderDate:d} - {order.Total}");

            Console.WriteLine("\n25. Numbers until a number is less than its position:");
            foreach (var number in numbersUntilLessThanPosition)
                Console.WriteLine(number);

            Console.WriteLine("\n26. Numbers starting from first number divisible by 3:");
            foreach (var number in numbersFromFirstDivisibleBy3)
                Console.WriteLine(number);

            Console.WriteLine("\n27. Numbers starting from first number less than its position:");
            foreach (var number in numbersFromFirstLessThanPosition)
                Console.WriteLine(number);

            Console.WriteLine("\n28. Any word contains 'ei':");
            Console.WriteLine(containsEi);

            Console.WriteLine("\n29. Categories with at least one out of stock product:");
            foreach (var group in categoriesWithOutOfStock)
            {
                Console.WriteLine(group.Key);
                foreach (var product in group)
                    Console.WriteLine(product.ProductName);
            }

            Console.WriteLine("\n30. Categories with all products in stock:");
            foreach (var group in categoriesWithAllInStock)
            {
                Console.WriteLine(group.Key);
                foreach (var product in group)
                    Console.WriteLine(product.ProductName);
            }

            Console.WriteLine("\n31. Numbers grouped by remainder when divided by 5:");
            foreach (var group in groupedNumbers)
            {
                Console.WriteLine($"Numbers with a remainder of {group.Key} when divided by 5:");
                foreach (var number in group)
                    Console.WriteLine(number);
            }

            Console.WriteLine("\n32. Words grouped by first letter:");
            foreach (var group in groupedWordsByFirstLetter)
            {
                Console.WriteLine(group.Key);
                foreach (var word in group)
                    Console.WriteLine(word);
            }

            Console.WriteLine("\n33. Words grouped by same characters:");
            foreach (var group in groupedAnagrams)
            {
                foreach (var word in group)
                    Console.WriteLine(word);

                Console.WriteLine("----");
            }
        }
    }
}
