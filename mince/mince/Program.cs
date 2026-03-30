using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {

        string radek1 = Console.ReadLine()!;
        string[] casti = radek1.Split(' ');

        
        int[] mince = new int[casti.Length];
        for (int i = 0; i < casti.Length; i++)
        {
            mince[i] = int.Parse(casti[i]);
        }

        string radek2 = Console.ReadLine()!;
        int suma = int.Parse(radek2);

        if (suma == 0)
        {
            Console.WriteLine("Nepoužije se žádná mince.");
            return;
        }

      
        List<int> pokus = new List<int>();

        
        Hledej(suma, mince, 0, pokus);
    }

    static void Hledej(int zbyvaSuma, int[] mince, int index, List<int> pokus)
    {
       
        if (zbyvaSuma == 0)
        {
            Console.WriteLine(string.Join(" ", pokus));
            return;
        }

       
        for (int i = index; i < mince.Length; i++)
        {
            
            if (zbyvaSuma >= mince[i])
            {
                pokus.Add(mince[i]); 

               
                Hledej(zbyvaSuma - mince[i], mince, i, pokus);

                
                pokus.RemoveAt(pokus.Count - 1);
            }
        }
    }
}