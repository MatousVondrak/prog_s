using System;
using System.Collections.Generic;
using System.Linq;

namespace Minimax_Nim_Simple
{
    class Program
    {
       
        class Tah
        {
            public int IndexHromadky;
            public int PocetSirek;
        }

        static void Main(string[] args)
        {
           
            List<int> hromadky = new List<int> { 3, 4, 5 };
            bool hrajePocitac = false; 

            while (hromadky.Sum() > 0)
            {
                VypisHru(hromadky);

                if (hrajePocitac)
                {
                    Console.WriteLine("Hraje počítač...");
                    Tah nejlepsi = NajdiNejlepsiTah(hromadky);
                    hromadky[nejlepsi.IndexHromadky] -= nejlepsi.PocetSirek;
                    Console.WriteLine($"Počítač vzal {nejlepsi.PocetSirek} ze {nejlepsi.IndexHromadky + 1}. hromádky.");
                }
                else
                {
                     Console.WriteLine("Hraješ ty.");
                    Console.Write("Zadej číslo hromádky (1-3): ");
                    int h = int.Parse(Console.ReadLine()) - 1;

                    Console.Write("Zadej počet sirek (1-2): ");
                    int p = int.Parse(Console.ReadLine());

                    if (h >= 0 && h < hromadky.Count && p >= 1 && p <= 2 && hromadky[h] >= p)
                    {
                        hromadky[h] -= p;
                    }
                    else
                    {
                        Console.WriteLine("Neplatný tah, zkus to znovu.");
                        continue;
                    }
                }

                hrajePocitac = !hrajePocitac;
                Console.WriteLine();
            }

       
            if (hrajePocitac)
                Console.WriteLine("Vyhrál Počítač!");
            else
                Console.WriteLine("Vyhrál jsi!");

            Console.ReadKey();
        }

        static void VypisHru(List<int> h)
        {
            for (int i = 0; i < h.Count; i++)
                Console.Write($"[{i + 1}]: {h[i]}  ");
            Console.WriteLine();
        }

        static Tah NajdiNejlepsiTah(List<int> hromadky)
        {
            int nejlepsiSkore = -1000;
            Tah nejlepsiTah = new Tah();

            for (int i = 0; i < hromadky.Count; i++)
            {
                if (hromadky[i] == 0) continue;


                for (int p = 1; p <= 2; p++)
                {
                    if (hromadky[i] >= p)
                    {
                        
                        hromadky[i] -= p;

                        
                        int skore = Minimax(hromadky, 10, false);

                    


                        hromadky[i] += p;

                        if (skore > nejlepsiSkore)
                        {
                            nejlepsiSkore = skore;
                            nejlepsiTah.IndexHromadky = i;
                            nejlepsiTah.PocetSirek = p;
                        }
                    }
                }
            }
            return nejlepsiTah;
        }

        static int Minimax(List<int> hromadky, int hloubka, bool maximalizujiciHrac)
        {
            
              if (hromadky.Sum() == 0)
            {
                return maximalizujiciHrac ? 10 : -10;
            }

            if (hloubka == 0) return 0;

            if (maximalizujiciHrac) 
            {
                int maxSkore = -1000;
                for (int i = 0; i < hromadky.Count; i++)
                {
                    if (hromadky[i] == 0) continue;
                    for (int p = 1; p <= 2; p++)
                    {
                        if (hromadky[i] >= p)
                        {
                            hromadky[i] -= p;
                            int skore = Minimax(hromadky, hloubka - 1, false);
                            hromadky[i] += p;
                            maxSkore = Math.Max(maxSkore, skore);
                        }
                    }
                }
                return maxSkore;
            }
            else 



            {
                int minSkore = 1000;
                for (int i = 0; i < hromadky.Count; i++)
                {
                    if (hromadky[i] == 0) continue;

                    for (int p = 1; p <= 2; p++)
                    {
                        if (hromadky[i] >= p)
                        {
                            hromadky[i] -= p;
                            int skore = Minimax(hromadky, hloubka - 1, true);
                            hromadky[i] += p;
                            minSkore = Math.Min(minSkore, skore);
                        }
                    }
                }
                return minSkore;
            }
        }
    }
}
