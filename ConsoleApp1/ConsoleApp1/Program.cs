namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("KENO Konzol alkalmazás");
            Console.WriteLine("=======================\n");

            List<NapiKeno> huzasok = new List<NapiKeno>();

            try
            {
                string[] sorok = File.ReadAllLines("huzasok.csv");

                foreach (string sor in sorok)
                {
                    if (!string.IsNullOrWhiteSpace(sor))
                    {
                        huzasok.Add(new NapiKeno(sor));
                    }
                }

                Console.WriteLine($"Beolvasott sorok száma: {huzasok.Count}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba a fájl beolvasásakor: {ex.Message}");
                return;
            }

            var hibasHuzasok = huzasok.Where(h => !h.Helyes()).ToList();
            var joHuzasok = huzasok.Where(h => h.Helyes()).ToList();

            Console.WriteLine($"\n7. feladat: Hibás napi adatok");
            Console.WriteLine($"Hibás húzások száma: {hibasHuzasok.Count}");
            Console.WriteLine($"Helyes húzások száma: {joHuzasok.Count}");

            Console.WriteLine("\n8. feladat: Szorzo metódus definiálva");

            if (joHuzasok.Count > 0)
            {
                UtolsoNapiNyeremeny(joHuzasok);
            }

            IsmerosEredmenyei(joHuzasok);

            Console.WriteLine("\nNyomjon Enter-t a kilépéshez...");
            Console.ReadLine();
        }

        static int Szorzo(List<int> tippek, NapiKeno huzas)
        {
            int talalat = huzas.TalalatSzam(tippek);
            int jatekTipus = tippek.Count;

            int[,] nyeremenySzorzok = new int[11, 11]
            {
                {0,0,0,0,0,0,0,0,0,0,0},
                {0,2,0,0,0,0,0,0,0,0,0},
                {0,0,1,0,0,0,0,0,0,0,0},
                {0,0,0,5,1,0,0,0,0,0,0},
                {0,0,0,0,20,5,1,0,0,0,0},
                {0,0,0,0,0,50,10,1,0,0,0},
                {0,0,0,0,0,0,200,20,5,1,0},
                {0,0,0,0,0,0,0,1000,50,10,2},
                {0,0,0,0,0,0,0,0,5000,100,10},
                {0,0,0,0,0,0,0,0,0,20000,100},
                {0,0,0,0,0,0,0,0,0,0,100000}
            };

            if (jatekTipus >= 1 && jatekTipus <= 10 && talalat >= 0 && talalat <= 10)
            {
                return nyeremenySzorzok[jatekTipus, talalat];
            }
            return 0;
        }

        static void UtolsoNapiNyeremeny(List<NapiKeno> joHuzasok)
        {
            var utolsoHuzas = joHuzasok.OrderByDescending(h => DateTime.ParseExact(h.HuzasDatum, "yyyy.MM.dd", null)).First();

            Console.WriteLine($"\n9. feladat: Utolsó napi nyeremény");
            Console.WriteLine($"Utolsó húzás dátuma: {utolsoHuzas.HuzasDatum}");

            // Tippek bekérése
            List<int> tippek = new List<int>();
            bool joTipp = false;

            while (!joTipp)
            {
                Console.Write("Adja meg a tippjeit (vesszővel elválasztva, 1-10 szám): ");
                string input = Console.ReadLine() ?? "";
                string[] tippSzamok = input.Split(',');

                if (tippSzamok.Length < 1 || tippSzamok.Length > 10)
                {
                    Console.WriteLine("Hiba: 1-10 számot adhat meg!");
                    continue;
                }

                joTipp = true;
                tippek.Clear();
                foreach (string tipp in tippSzamok)
                {
                    if (int.TryParse(tipp.Trim(), out int szam))
                    {
                        if (szam >= 1 && szam <= 80)
                        {
                            tippek.Add(szam);
                        }
                        else
                        {
                            Console.WriteLine($"Hiba: {szam} nincs 1-80 között!");
                            joTipp = false;
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Hiba: '{tipp}' nem szám!");
                        joTipp = false;
                        break;
                    }
                }
            }

            int tet = 0;
            while (tet < 1 || tet > 5)
            {
                Console.Write("Adja meg a tét szorzóját (1-5): ");
                if (!int.TryParse(Console.ReadLine(), out tet) || tet < 1 || tet > 5)
                {
                    Console.WriteLine("Hiba: 1-5 közötti számot adjon meg!");
                    tet = 0;
                }
            }

            int osszeg = 200 * tet;
            Console.WriteLine($"Fogadási összeg: {osszeg} Ft");

            int szorzo = Szorzo(tippek, utolsoHuzas);
            int nyeremeny = osszeg * szorzo;

            Console.WriteLine($"Találatok: {utolsoHuzas.TalalatSzam(tippek)}");
            Console.WriteLine($"Nyeremény szorzó: {szorzo}");
            Console.WriteLine($"Nyeremény: {nyeremeny} Ft");
        }

        static void IsmerosEredmenyei(List<NapiKeno> joHuzasok)
        {
            Console.WriteLine($"\n10. feladat: Ismerősünk 2020-as eredményei");

            List<int> ismerosTippjei = new List<int> { 17, 28, 32, 44, 54, 63, 72, 75 };
            int tetSzorzo = 4;
            int alapertek = 200;
            int tetOsszeg = alapertek * tetSzorzo;

            var _2020asHuzasok = joHuzasok.Where(h => h.Ev == 2020).OrderBy(h => DateTime.ParseExact(h.HuzasDatum, "yyyy.MM.dd", null)).ToList();

            Console.WriteLine($"2020-as húzások száma: {_2020asHuzasok.Count}");
            Console.WriteLine("Nyeremények:");

            int osszNyeremeny = 0;
            int nyertesNapok = 0;

            foreach (var huzas in _2020asHuzasok)
            {
                int szorzo = Szorzo(ismerosTippjei, huzas);

                if (szorzo > 0)
                {
                    int nyeremeny = tetOsszeg * szorzo;
                    osszNyeremeny += nyeremeny;
                    nyertesNapok++;
                    Console.WriteLine($"{huzas.HuzasDatum}: {huzas.TalalatSzam(ismerosTippjei)} találat, nyeremény: {nyeremeny} Ft");
                }
            }

            int befizetettOsszeg = _2020asHuzasok.Count * tetOsszeg;
            int eredmeny = osszNyeremeny - befizetettOsszeg;

            Console.WriteLine($"\nÖsszes befizetés: {befizetettOsszeg} Ft");
            Console.WriteLine($"Összes nyeremény: {osszNyeremeny} Ft");
            Console.WriteLine($"Nyertes napok száma: {nyertesNapok}");
            Console.WriteLine($"Év végi eredmény: {eredmeny} Ft ({(eredmeny >= 0 ? "nyereség" : "veszteség")})");
        
        }
    }
}
