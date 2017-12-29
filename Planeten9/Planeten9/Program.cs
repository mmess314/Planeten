using System;

public class Program
{
    //Konstanten
    const double G = 6.67408E-11; //Gravitationskonstante [m^3/(kg s^2)] 
    const double MSonne = 1.98892E30; // Sonnenmasse [kg]
    const double K = G * MSonne;
    const double AE = 1.49597870700E11; // Astronomische Einheit [m]
    const double J = 31536000; // 1 Jahr

    const int TEnde = 31536000; // 1 Jahr
    const int TBahnIntervall = 86400; // 1 Tag

    public struct BahnPunkt
    {
        public int t;
        public double rx, ry, vx, vy, ax, ay, d;
    }

    public static void Main()
    {

        // Array für Bahnpunkte der Erdbahn
        BahnPunkt[] Bahn = new BahnPunkt[366];

        // Variablen für Simulation
        double rx, ry, vx, vy, ax, ay, d, dd;
        int t = 0; // Sekunden


        // r und v für eine Kreisbahn berechnen
        double rKreis = Math.Pow(G * MSonne * J * J / 4 / Math.PI / Math.PI, 1.0 / 3.0);
        double vKreis = 2 * Math.PI * rKreis / TEnde;

        Console.WriteLine("Konstanten");
        Console.Write("G = ");
        Console.WriteLine(G);
        Console.Write("MSonne = ");
        Console.WriteLine(MSonne);
        Console.WriteLine();
        Console.WriteLine("Werte für Kreisbahn");
        Console.Write("rKreis = ");
        Console.WriteLine(rKreis);
        Console.Write("vKreis = ");
        Console.WriteLine(vKreis);
        Console.WriteLine();

        // Anfangswerte für Erde
        rx = rKreis;
        ry = 0;
        vx = 0;
        vy = vKreis;
        d = rKreis;

        // Anfangswerte in Array schreiben. ax, ay noch nicht bekannt/benötigt
        Bahn[0].t = 0;
        Bahn[0].rx = rx;
        Bahn[0].ry = ry;
        Bahn[0].vx = vx;
        Bahn[0].vy = vy;
        Bahn[0].d = d;

        while (t <= TEnde)
        {
            t++;
            rx += vx;
            ry += vy;
            dd = rx * rx + ry * ry;
            d = Math.Sqrt(dd);
            ax = -K * rx / dd / d;
            ay = -K * ry / dd / d;
            vx += ax;
            vy += ay;

            if (t % TBahnIntervall == 0) // jeden Tag
            {
                int index = t / TBahnIntervall;
                Bahn[index].t = t / TBahnIntervall;
                Bahn[index].rx = rx;
                Bahn[index].ry = ry;
                Bahn[index].vx = vx;
                Bahn[index].vy = vy;
                Bahn[index].ax = ax;
                Bahn[index].ay = ay;
                Bahn[index].d = d;
            }
        }

        for (int i = 0; i <= TEnde / TBahnIntervall; i++)
        {
            Console.Write("t = ");
            Console.WriteLine(Bahn[i].t);
            Console.Write("rx = ");
            Console.WriteLine(Bahn[i].rx);
            Console.Write("ry = ");
            Console.WriteLine(Bahn[i].ry);
            Console.Write("vx = ");
            Console.WriteLine(Bahn[i].vx);
            Console.Write("vy = ");
            Console.WriteLine(Bahn[i].vy);
            Console.Write("d = ");
            Console.WriteLine(Bahn[i].d);
            Console.WriteLine();
        }
        Console.WriteLine("Differenz vom Ausgangspunkt in m:");
        Console.WriteLine(Math.Sqrt((Bahn[365].rx - Bahn[0].rx) * (Bahn[365].rx - Bahn[0].rx) 
                                  + (Bahn[365].ry - Bahn[0].ry) * (Bahn[365].ry - Bahn[0].ry)));
        Console.WriteLine();
    }
}
