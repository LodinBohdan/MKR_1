using System;
using System.Linq;
using System.Collections.Generic;

namespace mkr
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            try
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                string inputFilePath = args.Length > 0 ? args[0] : Path.Combine(AppContext.BaseDirectory, "INPUT.TXT");
                string outputFilePath = Path.Combine(AppContext.BaseDirectory, "OUTPUT.TXT");

                string[] lines = System.IO.File.ReadAllLines(inputFilePath);

                Console.WriteLine("Input data:");
                Console.WriteLine(string.Join(Environment.NewLine, lines).Trim());

                ValidateInput(lines);

                string result = ProcessLines(lines);
                System.IO.File.WriteAllText(outputFilePath, result.Trim());


                Console.WriteLine("\nOutput data:");
                Console.WriteLine(result.Trim());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public static void ValidateInput(string[] lines)
        {
            if (lines.Length != 6)
            {
                throw new InvalidOperationException("The number of input lines must be exactly 6.");
            }

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line) || line.Split().Length != 2)
                {
                    throw new InvalidOperationException("Each line must contain exactly two numbers separated by a space.");
                }

                string[] parts = line.Split();
                if (!int.TryParse(parts[0], out int width) || width <= 0 ||
                    !int.TryParse(parts[1], out int height) || height <= 0)
                {
                    throw new InvalidOperationException("Both values on each line must be positive integers.");
                }
            }
        }

        public static string ProcessLines(string[] lines)
        {
            int[] lst = new int[12]; // Масив для збереження розмірів листів
            HashSet<int> ls = new HashSet<int>(); // Для підрахунку різних розмірів
            int kv = 0; // Кількість квадратів

            // Обробка введених даних
            for (int i = 0; i < 6; i++)
            {
                string[] parts = lines[i].Split();
                lst[i * 2] = int.Parse(parts[0]);
                lst[i * 2 + 1] = int.Parse(parts[1]);
            }

            // Додавання розмірів у HashSet
            foreach (int size in lst)
            {
                ls.Add(size);
            }

            // Підрахунок кількості квадратів
            for (int z = 0; z < 11; z += 2)
            {
                if (lst[z] == lst[z + 1]) kv++;
            }

            Array.Sort(lst); // Сортування за розмірами

            if (ls.Count == 1)
            {
                return "POSSIBLE";
            }

            if (ls.Count == 2 &&
                kv == 2 &&
                (lst[0] == lst[7] || lst[11] == lst[4]))
            {
                return "POSSIBLE";
            }

            if (lst[0] == lst[3] &&
                lst[4] == lst[7] &&
                lst[8] == lst[11] &&
                ls.Count == 3 &&
                kv == 0)
            {
                return "POSSIBLE";
            }

            return "IMPOSSIBLE";
        }
    }
}
