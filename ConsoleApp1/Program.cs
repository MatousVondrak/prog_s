using System.Runtime.InteropServices;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

         Film film = new Film("kebab prvni dil", "SG", "diu", 2009);
            Console.WriteLine(film.Prijmeni);
         Film film1 = new Film("kebab druhy dil", "PV", "nig", 2006);
            Console.WriteLine(film.Prijmeni);

            
        }
    }
    class Film
       
    {
        public Film(string nazev, string jmeno, string primeni, int rok)
        { 
                NazevFilmu = nazev;
                
        }
       public string NazevFilmu { get; }
       public string JemenoRezisera { get; }
       public string Prijmeni { get; }
       public int RokVzniku { get; }
       public double Hodnoceni { get; private set; }

       private  List<double> hodnoceni;
        



    }
}
