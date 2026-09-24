using System;
using System.Collections;
using System.Collections.Generic;

namespace AdvancedCSharpAssignment
{
    class Range<T> where T : IComparable<T>
    {
        public T Minimum { get; set; }
        public T Maximum { get; set; }

        public Range(T minimum, T maximum)
        {
            Minimum = minimum;
            Maximum = maximum;
        }

        public bool IsInRange(T value)
        {
            return value.CompareTo(Minimum) >= 0 &&
                   value.CompareTo(Maximum) <= 0;
        }

        public double Length()
        {
            return Convert.ToDouble(Maximum) - Convert.ToDouble(Minimum);
        }

        public override string ToString()
        {
            return $"Range: {Minimum} - {Maximum}";
        }
    }

    class FixedSizeList<T>
    {
        private T[] items;
        private int count;

        public FixedSizeList(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentException("Capacity cannot be negative.");

            items = new T[capacity];
            count = 0;
        }

        public void Add(T item)
        {
            if (count == items.Length)
                throw new InvalidOperationException("The list is full.");

            items[count] = item;
            count++;
        }

        public T Get(int index)
        {
            if (index < 0 || index >= count)
                throw new IndexOutOfRangeException("Invalid index.");

            return items[index];
        }

        public int Count
        {
            get { return count; }
        }
    }

    internal class Program
    {
        static void OptimizedBubbleSort(int[] numbers)
        {
            for (int i = 0; i < numbers.Length - 1; i++)
            {
                bool swapped = false;

                for (int j = 0; j < numbers.Length - 1 - i; j++)
                {
                    if (numbers[j] > numbers[j + 1])
                    {
                        int temp = numbers[j];
                        numbers[j] = numbers[j + 1];
                        numbers[j + 1] = temp;
                        swapped = true;
                    }
                }

                if (!swapped)
                    break;
            }
        }

        static void ReverseArrayList(ArrayList list)
        {
            int left = 0;
            int right = list.Count - 1;

            while (left < right)
            {
                object temp = list[left];
                list[left] = list[right];
                list[right] = temp;

                left++;
                right--;
            }
        }

        static List<int> GetEvenNumbers(List<int> numbers)
        {
            List<int> evenNumbers = new List<int>();

            foreach (int number in numbers)
            {
                if (number % 2 == 0)
                    evenNumbers.Add(number);
            }

            return evenNumbers;
        }

        static int FirstNonRepeatedCharacter(string text)
        {
            Dictionary<char, int> frequency = new Dictionary<char, int>();

            foreach (char character in text)
            {
                if (frequency.ContainsKey(character))
                    frequency[character]++;
                else
                    frequency.Add(character, 1);
            }

            for (int i = 0; i < text.Length; i++)
            {
                if (frequency[text[i]] == 1)
                    return i;
            }

            return -1;
        }

        static void Main(string[] args)
        {
            int[] numbers = { 5, 3, 8, 4, 2, 1 };

            OptimizedBubbleSort(numbers);

            Console.WriteLine("Optimized Bubble Sort:");
            foreach (int number in numbers)
                Console.Write(number + " ");

            Console.WriteLine();
            Console.WriteLine();

            Range<int> range = new Range<int>(10, 50);

            Console.WriteLine(range);
            Console.WriteLine($"Is 25 in range: {range.IsInRange(25)}");
            Console.WriteLine($"Is 60 in range: {range.IsInRange(60)}");
            Console.WriteLine($"Length: {range.Length()}");

            Console.WriteLine();

            ArrayList list = new ArrayList { 1, 2, 3, 4, 5 };

            ReverseArrayList(list);

            Console.WriteLine("Reversed ArrayList:");
            foreach (object item in list)
                Console.Write(item + " ");

            Console.WriteLine();
            Console.WriteLine();

            List<int> values = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };

            List<int> evenNumbers = GetEvenNumbers(values);

            Console.WriteLine("Even Numbers:");
            foreach (int number in evenNumbers)
                Console.Write(number + " ");

            Console.WriteLine();
            Console.WriteLine();

            FixedSizeList<string> fixedList = new FixedSizeList<string>(3);

            fixedList.Add("C#");
            fixedList.Add("SQL");
            fixedList.Add("ASP.NET");

            Console.WriteLine("Fixed Size List:");
            for (int i = 0; i < fixedList.Count; i++)
                Console.WriteLine(fixedList.Get(i));

            Console.WriteLine();

            try
            {
                fixedList.Add("Angular");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                Console.WriteLine(fixedList.Get(5));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine();

            string text = "swiss";
            int index = FirstNonRepeatedCharacter(text);

            Console.WriteLine($"First non-repeated character index in \"{text}\": {index}");
        }
    }
}
