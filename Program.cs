using System;
using System.Collections.Generic;

namespace MathApp
{
 
    public struct Interval
    {
        public double min;
        public double max;
        public bool minOpen; 
        public bool maxOpen;


        public string VypisInterval()
        {
            string s = "";
            if (minOpen)
            {
                s = s + "(";
            }
            else
            {
                s = s + "<";
            }

            if (min == double.MinValue) s = s + "-nekonecno";
            else s = s + min;

            s = s + "; ";

            if (max == double.MaxValue) s = s + "nekonecno";
            else s = s + max;

            if (maxOpen)
            {
                s = s + ")";
            }
            else
            {
                s = s + ">";
            }
            return s;
        }
    }

    interface IDerivace
    {
        string Derivuj();
    }

    interface IInverze
    {
        string Inverzni();
    }

    public class MathFunction
    {
        public string Name;
        public string Description;
        public Interval Domain;
        public Interval Range;


        public virtual double Spocitej(double x)
        {
            return 0;
        }

        public virtual void VypisInfo()
        {
            Console.WriteLine("Funkce: " + Name);
            Console.WriteLine("Vzorec: " + Description);
            Console.WriteLine("Definicni obor: " + Domain.VypisInterval());
   
            Console.WriteLine("Obor hodnot: " + Range.VypisInterval());
        }
    }

  
    public class LinearniFunkce : MathFunction, IDerivace, IInverze
    {
        public double a;
        public double b;


        public LinearniFunkce(double a, double b)
        {
            this.a = a;
            this.b = b;
            Name = "Linearni funkce";
            Description = "f(x) = " + a + "x + " + b;

     
            Domain.min = double.MinValue;
            Domain.max = double.MaxValue;
            Domain.minOpen = true;
            Domain.maxOpen = true;

            Range.min = double.MinValue;
            Range.max = double.MaxValue;
            Range.minOpen = true;
            Range.maxOpen = true;
        }

        public override double Spocitej(double x)
        {
            return a * x + b;
        }

        public string Derivuj()
        {
            return "f'(x) = " + a;
        }

        public string Inverzni()
        {
            return "f-1(x) = (x - " + b + ") / " + a;
        }
    }

    public class LinearniAbsFunkce : MathFunction
    {
        public double a;
        public double b;

        public LinearniAbsFunkce(double a, double b)
        {
            this.a = a;
            this.b = b;
            Name = "Linearni s absolutni hodnotou";
            Description = "f(x) = " + a + " * |x + " + b + "|";

            Domain.min = double.MinValue;
            Domain.max = double.MaxValue;
            Domain.minOpen = true;
            Domain.maxOpen = true;



            Range.min = 0;

            Range.max = double.MaxValue;
            Range.minOpen = false; // <0
            Range.maxOpen = true;
        }

        public override double Spocitej(double x)
        {
            return a * Math.Abs(x + b);
        }
    }


    public class KvadratickaFunkce : MathFunction, IDerivace
    {
        public double a, b, c;

        public KvadratickaFunkce(double a, double b, double c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            Name = "Kvadraticka funkce";
            Description = "f(x) = " + a + "x^2 + " + b + "x + " + c;

            Domain.min = double.MinValue;
            Domain.max = double.MaxValue;
            Domain.minOpen = true;
            Domain.maxOpen = true;

          
            Range.min = double.MinValue;
            Range.max = double.MaxValue;
            Range.minOpen = true;
            Range.maxOpen = true;
        }

        public override double Spocitej(double x)
        {
            return a * (x * x) + b * x + c;
        }

        public override void VypisInfo()
        {
            base.VypisInfo();
            Console.WriteLine("Poznamka: Funkce ma parabolicky prubeh.");
        }

        public string Derivuj()
        {
            return "f'(x) = " + (2 * a) + "x + " + b;
        }
    }

    public class LomenaFunkce : MathFunction, IDerivace
    {
        public double a;
        public double b; // posun

        public LomenaFunkce(double a, double b)
        {
            this.a = a;
            this.b = b;
            Name = "Linearni lomena funkce";
            Description = "f(x) = " + a + "/x + " + b;

            Domain.min = double.MinValue;
            Domain.max = double.MaxValue;
            Domain.minOpen = true;
            Domain.maxOpen = true;

            Range.min = double.MinValue;
            Range.max = double.MaxValue;
            Range.minOpen = true;
            Range.maxOpen = true;
        }

        public override double Spocitej(double x)
        {
  
            if (x == 0)
            {
                Console.WriteLine("Chyba: deleni nulou!");
                return 0;
            }
            return (a / x) + b;
        }

        public override void VypisInfo()
        {
            base.VypisInfo();
            Console.WriteLine("Poznamka: Funkce ma hyperbolicky prubeh. (x nesmi byt 0)");
        }

        public string Derivuj()
        {
            return "f'(x) = -" + a + " / x^2";
        }
    }


    class Program
    {
        static void Main(string[] args)
        {


            List<MathFunction> seznamFunkci = new List<MathFunction>();

            seznamFunkci.Add(new LinearniFunkce(2, 3));        
            seznamFunkci.Add(new LinearniAbsFunkce(1, -2));    
            seznamFunkci.Add(new KvadratickaFunkce(1, 0, -4)); 
            seznamFunkci.Add(new LomenaFunkce(1, 0));         

            double x = 2; 
            Console.WriteLine("Vybrane x = " + x);
            Console.WriteLine("------------------------");



            foreach (MathFunction f in seznamFunkci)
            {

                
                f.VypisInfo();

               
                double vysledek = f.Spocitej(x);
                Console.WriteLine("f(" + x + ") = " + vysledek);

            
                if (f is IDerivace)
                {
                    IDerivace d = (IDerivace)f;
                    Console.WriteLine("Derivace: " + d.Derivuj());
                }

                if (f is IInverze)
                {
                    IInverze i = (IInverze)f;
                    Console.WriteLine("Inverzni fce: " + i.Inverzni());
                }

              
                Console.WriteLine("----------------------");
            }

            Console.ReadLine();
        }
    }
}
