using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Periferia
{
    public class Karttaruutu : IPiirrettävä
    {
        public bool Käveltävä
        {
            get
            {
                if (Tyyppi != Ruututyypit.TYHJÄ)
                    return false;
                if (Entiteetti is Hahmo)
                    return false;
                return true;
            }
        }

        public Ruututyypit Tyyppi { get; set; }
        public IPiirrettävä Entiteetti { get; set; }
        public int Sarake { get; set; }
        public int Rivi { get; set; }
        public char Merkki { get; set; }
        public ConsoleColor Väri { get; set; } = ConsoleColor.White;
        public string Nimi { get; set; }

        public void Piirrä()
        {
            Console.ForegroundColor = Väri;
            Console.Write(Merkki);
        }

        public void Päivitä()
        {
            int KursoriYlä = Rivi + Konsoli.KarttaOffset_Ylä;
            int KursoriVasen = Sarake + Konsoli.KarttaOffset_Vasen;
            Console.SetCursorPosition(KursoriVasen, KursoriYlä);
            
            if(Entiteetti != null)
            {
                Entiteetti.Piirrä();
            }
            else
            {
                Piirrä();
            }
        }

        public enum Ruututyypit
        {
            SISÄÄN,
            ULOS,
            PELASTUS,
            SEINÄ,
            PUU,
            PUU_HAJOAMASSA,
            TYHJÄ
        }
    }
}