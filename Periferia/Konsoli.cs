using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Periferia
{
    public class Konsoli
    {
        public int KarttaOffset_X { get; set; }
        public int KarttaOffset_Y { get; set; }

        public int HahmoRuutuOffset_X { get; set; }
        public int HahmoRuutuOffset_Y { get; set; }

        public int ViestiLokiOffset_X { get; set; }
        public int ViestiLokiOffset_Y { get; set; }

        public Viestiloki Viestiloki = new Viestiloki();

        public void PiirräTyhjäKartta(Kartta k)
        {
            k.Piirrä(0, 0);
        }

        public void PiirräKartalle(Kartta k, IPiirrettävä p)
        {

        }

        public void PiirräLoki()
        {
            // alustetaan kursorin sijainti
            int KursoriVasen = 2;
            int KursoriYlä = 0;

            Viestiloki.Piirrä(KursoriVasen, KursoriYlä);
            
        }
        
        public void PiirräHahmoRuutu(Hahmoruutu r)
        {
            
        }


        static public void UusiRivi(int offsetVasen)
        {
            Console.SetCursorPosition(offsetVasen, Console.CursorTop + 1);
        }
    }
}