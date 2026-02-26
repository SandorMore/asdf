using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asdf
{
    public class NapiKenoGUI
    {
        public List<int> HuzottSzamok { get; set; }

        public NapiKenoGUI()
        {
            HuzottSzamok = new List<int>();
        }

        public void Sorsolas()
        {
            Random rnd = new Random();
            HuzottSzamok.Clear();

            while (HuzottSzamok.Count < 20)
            {
                int szam = rnd.Next(1, 81);
                if (!HuzottSzamok.Contains(szam))
                {
                    HuzottSzamok.Add(szam);
                }
            }

            HuzottSzamok.Sort();
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
    }
}
