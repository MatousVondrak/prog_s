using System;

namespace vztahy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Zadejte počet mužů a žen:");
            int n = int.Parse(Console.ReadLine());

            int[,] wp = new int[n, n];
            int[,] mp = new int[n, n];

            Console.WriteLine("\nZadejte preference žen (každý řádek musí mít přesně " + n + " čísel 1.." + n + "):");
            for (int i = 0; i < n; i++)
            {
                int[] row = ReadRow(n, $"Žena {i + 1}: ");
                for (int j = 0; j < n; j++) wp[i, j] = row[j] - 1;
            }

            Console.WriteLine("\nZadejte preference mužů (každý řádek musí mít přesně " + n + " čísel 1.." + n + "):");
            for (int i = 0; i < n; i++)
            {
                int[] row = ReadRow(n, $"Muž {i + 1}: ");
                for (int j = 0; j < n; j++) mp[i, j] = row[j] - 1;
            }

            int[] nextIdx = new int[n];
            int[] manToWoman = new int[n];
            int[] womanToMan = new int[n];
            for (int i = 0; i < n; i++) { manToWoman[i] = -1; womanToMan[i] = -1; }

            while (true)
            {
                int w = -1;
                for (int i = 0; i < n; i++)
                {
                    if (womanToMan[i] == -1 && nextIdx[i] < n) { w = i; break; }
                }
                if (w == -1) break;

                int m = wp[w, nextIdx[w]];
                nextIdx[w]++;

                if (manToWoman[m] == -1)
                {
                    manToWoman[m] = w;
                    womanToMan[w] = m;
                        }
                else
                {
                    int oldW = manToWoman[m];
                    bool prefersNew = false;
                    for (int k = 0; k < n; k++)
                    {
                        if (mp[m, k] == w) { prefersNew = true; break; }
                        if (mp[m, k] == oldW) { prefersNew = false; break; }
                    }
                    if (prefersNew)
                    {
                        manToWoman[m] = w;
                        womanToMan[w] = m;
                         womanToMan[oldW] = -1;
                    }
                }
             }

            Console.WriteLine("\n--- Výsledek (ženy → muži) ---");
            for (int i = 0; i < n; i++)
                Console.WriteLine(womanToMan[i] + 1);
        }
        static int[] ReadRow(int n, string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string line = Console.ReadLine() ?? "";
                var parts = line.Split(new char[] { ' ', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length != n)
                {
                    Console.WriteLine($"⚠ Potřebuji přesně {n} čísel oddělených mezerou/čárkou. Zkus to znovu.");
                    continue;
                }

                int[] vals = new int[n];
                bool ok = true;

                for (int i = 0; i < n; i++)
                {
                    if (!int.TryParse(parts[i], out vals[i]))
                    {
                        ok = false; break;
                    }
                    if (vals[i] < 1 || vals[i] > n)
                    {
                        ok = false; break;
                    }
                }

                if (!ok)
                {
                    Console.WriteLine($" Každé číslo musí být v rozsahu 1..{n}. Zkus to znovu.");
                    continue;
                }

                return vals;
            }
        }
    }
}
