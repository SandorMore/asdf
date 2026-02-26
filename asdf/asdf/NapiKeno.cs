using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asdf
{
    internal class NapiKeno
    {
        public int Ev { get; set; }
        public int Het { get; set; }
        public int Nap { get; set; }
        public string HuzasDatum { get; set; }
        public List<int> HuzottSzamok { get; set; }

        public NapiKeno(string sor)
        {
            string[] adatok = sor.Split(';');

            if (adatok.Length >= 5)
            {
                Ev = int.Parse(adatok[0]);
                Het = int.Parse(adatok[1]);
                Nap = int.Parse(adatok[2]);
                HuzasDatum = adatok[3];

                HuzottSzamok = new List<int>();
                for (int i = 4; i < adatok.Length; i++)
                {
                    if (int.TryParse(adatok[i], out int szam))
                    {
                        HuzottSzamok.Add(szam);
                    }
                }
            }
        }

        public int TalalatSzam(List<int> tippek)
        {
            int talalat = 0;
            foreach (int tipp in tippek)
            {
                if (HuzottSzamok.Contains(tipp))
                {
                    talalat++;
                }
            }
            return talalat;
        }

        public bool Helyes()
        {
            if (HuzottSzamok.Count != 20)
                return false;

            return HuzottSzamok.Distinct().Count() == 20;
        }
    }
}
