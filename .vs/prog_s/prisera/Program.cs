namespace prisera
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var pl = Nacti();                 
            var m = new M(pl);                
            m.Tisk();                         
            m.Beh();                          
        }

        static char[,] Nacti()
        {
            Console.WriteLine("Zadejte šířku:");
            int s = int.Parse(Console.ReadLine());
            Console.WriteLine("Zadejte výšku:");
            int v = int.Parse(Console.ReadLine());
            Console.WriteLine(" bludiště:");

            var g = new char[v, s];

            
            for (int i = 0; i < v; i++)
                for (int j = 0; j < s; j++)
                    g[i, j] = '.';

            for (int i = 0; i < v; i++)
            {
                string line = Console.ReadLine();
                
                int lim = line.Length < s ? line.Length : s;
                for (int j = 0; j < lim; j++)
                {
                    
                    g[i, j] = line[j];
                }
            }
            return g;
        }
    }

    class M
    {

        public int W, H;
        public char[,] G;

     
        int[] dx = { -1, 0, 1, 0 };
        int[] dy = { 0, 1, 0, -1 };
        char[] sip = { '^', '>', 'v', '<' };

        public M(char[,] z)
        {
            H = z.GetLength(0);
            W = z.GetLength(1);
            G = new char[H, W];

         
            for (int i = 0; i < H; i++)
                for (int j = 0; j < W; j++)
                    G[i, j] = z[i, j];
        }

        public void Beh()
        {
            var p = Najdi();
            if (p.Count == 0)
            {
                Console.WriteLine("Šipka neni!");
                return;
            }

            int x = p[0], y = p[1], d = p[2];
            int sx = x, sy = y, sd = d;

            int k = 0;
            int lim = 20;
            bool prvni = true;

            while (k < lim)
            {
                k++;

               
                int rd = (d + 1) % 4;
                int rx = x + dx[rd];
                int ry = y + dy[rd];

                int fx = x + dx[d];
                int fy = y + dy[d];

               
                G[x, y] = '.';

                if (!Z(rx, ry))              
                {
                    d = rd;
                    x = x + dx[d];
                    y = y + dy[d];
                }
                else if (!Z(fx, fy))   
                {
                    x = fx;
                    y = fy;
                }
                else        
                {
                    d = (d + 3) % 4;
                   
                }

                G[x, y] = sip[d];

                Console.WriteLine($"{k}. krok");
                Tisk();
                Console.WriteLine();

                if (!prvni && x == sx && y == sy && d == sd)
                {
                    Console.WriteLine($"Šipka se vrátila na start po {k} krocích!");
                    break;
                }
                prvni = false;
            }

            if (k >= lim)
            {
                Console.WriteLine("Dosažen maximální počet kroků!");
            }
        }

   
        List<int> Najdi()
        {
            var outp = new List<int>(3);
            for (int i = 0; i < H; i++)
            {
                for (int j = 0; j < W; j++)
                {
                    int d = -1;
                    char c = G[i, j];
                    if (c == '^') d = 0;
                    else if (c == '>') d = 1;
                    else if (c == 'v') d = 2;
                    else if (c == '<') d = 3;

                    if (d != -1)
                    {
                        outp.Add(i);
                        outp.Add(j);
                        outp.Add(d);
                        return outp; 
                    }
                }
            }
            return outp;
        }
    
          bool Z(int x, int y)
        {
            if (x < 0 || x >= H || y < 0 || y >= W) return true;
            char c = G[x, y];
            return c == 'X' || c == '#' || c == '*' || c == '█';
        }

        public void Tisk()
        {
            for (int i = 0; i < H; i++)
            {
                for (int j = 0; j < W; j++)
                    Console.Write(G[i, j]);
                Console.WriteLine();
            }
        }
    }
}
