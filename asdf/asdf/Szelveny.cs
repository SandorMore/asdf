using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asdf
{
    public class Szelveny
    {
        public int JatekTipus { get; set; }
        public int TetSzorzo { get; set; }
        public List<int> Tippek { get; set; }
        public string EredetiSor { get; set; }

        public Szelveny(string sor)
        {
            EredetiSor = sor;
            Tippek = new List<int>();

            try
            {
                string[] reszek = sor.Split('!');

                string elsoResz = reszek[0].Substring(2);
                string[] elsoReszAdatok = elsoResz.Split(',');

                JatekTipus = int.Parse(elsoReszAdatok[0]);
                TetSzorzo = int.Parse(elsoReszAdatok[1]);

                string[] tippek = reszek[1].Split(',');
                foreach (string tipp in tippek)
                {
                    if (int.TryParse(tipp, out int szam))
                    {
                        Tippek.Add(szam);
                    }
                }
            }
            catch (Exception)
            {
                JatekTipus = 0;
                TetSzorzo = 1;
            }
        }

        public override string ToString()
        {
            return $"{EredetiSor} ({JatekTipus}/10, {TetSzorzo}x)";
        }
    }
}
