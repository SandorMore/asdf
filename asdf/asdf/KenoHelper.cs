using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asdf
{
    public static class KenoHelper
    {
        public static int Szorzo(List<int> tippek, NapiKenoGUI huzas)
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
    }
}
