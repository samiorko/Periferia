using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Periferia
{
    public class Konsoli
    {
        public static int KarttaOffset_Vasen { get; } = 2;
        public static int KarttaOffset_Ylä { get;  } = 2;

        public static int HahmoRuutuOffset_Vasen { get; } = 25;
        public static int HahmoRuutuOffset_Ylä { get; } = 2;

        public static int ViestiLokiOffset_Vasen { get; } = 2;
        public static int ViestiLokiOffset_Ylä { get; } = 16;

        public static int KonsoliLeveys { get; } = 150;
        public static int KonsoliKorkeus { get; } = 35;

        public Viestiloki Viestiloki = new Viestiloki();
        public Hahmoruutu Hahmoruutu = new Hahmoruutu();

        public void PiirräTyhjäKartta(Kartta k)
        {
            k.Piirrä(0, 0);
        }
        

        public void PiirräLoki()
        {
            // alustetaan kursorin sijainti
            int KursoriVasen = 2;
            int KursoriYlä = 0;

            Viestiloki.Piirrä(KursoriVasen, KursoriYlä);
            
        }
        
        public void PiirräHahmoRuutu()
        {
            Hahmoruutu.Piirrä(22,2);
        }


        static public void UusiRivi(int offsetVasen)
        {
            Console.SetCursorPosition(offsetVasen, Console.CursorTop + 1);
        }

        static public void AlustaKonsoli()
        {
            Console.SetWindowSize(KonsoliLeveys, KonsoliKorkeus);
            Console.SetBufferSize(KonsoliLeveys, KonsoliKorkeus);
            Console.Clear();
            Console.SetCursorPosition(0,0);
        }

        static public void NollaaKursori()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(KonsoliLeveys-1, KonsoliKorkeus-1);
        }
    }
}