using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Periferia
{
    public class Hahmoruutu
    {
        public void Piirrä(int kursoriVasen, int kursoriYlä)
        {
            Console.SetCursorPosition(kursoriVasen, kursoriYlä); // Asetetaan kursorin aloituspaikka

            Console.Write("Pelaaja " + Moottori.Pelaaja.Merkki + ": " + Moottori.Pelaaja.Nimi);
            Konsoli.UusiRivi(kursoriVasen);
            Console.Write("Voima: " + Moottori.Pelaaja.Voima);
            Konsoli.UusiRivi(kursoriVasen);
            Console.Write("HP:        " + PiirräPalkki(Moottori.Pelaaja.HP));
            Konsoli.UusiRivi(kursoriVasen);
            Console.Write("Nesteytys: " + PiirräPalkki(Moottori.Pelaaja.Nesteytys));
            //Console.ResetColor();
            Konsoli.UusiRivi(kursoriVasen);
            Console.Write("Repun sisältö: ");
            NäytäSisältö(kursoriVasen);
            NäytäSisältö(kursoriVasen);
            NäytäSisältö(kursoriVasen);
            Konsoli.UusiRivi(kursoriVasen);
            Konsoli.UusiRivi(kursoriVasen);

            foreach (Hahmo entiteetti in Moottori.NykyinenKartta.Entiteetit.Where(e => e is Hahmo))
            {
                entiteetti.Piirrä();
                Console.Write(" = " + entiteetti.Nimi);
                Konsoli.UusiRivi(kursoriVasen);

                if (entiteetti is Vihollinen)
                {
                    Console.Write("Voima: " + entiteetti.Voima);
                    Konsoli.UusiRivi(kursoriVasen);
                    Console.Write("HP:        " + PiirräPalkki(entiteetti.HP));
                    Konsoli.UusiRivi(kursoriVasen);
                }
            }

            foreach (Tavara entiteetti in Moottori.NykyinenKartta.Entiteetit.Where(e => e is Tavara))
            {
                entiteetti.Piirrä();
                Console.Write(" = " + entiteetti.Nimi);
                Konsoli.UusiRivi(kursoriVasen);
            }
        }

        public string PiirräPalkki(int PropertynKoko)
        {
            string palkki = "[";
            //Console.ForegroundColor = ConsoleColor.Magenta;

            for (int i = 0; i < 10; i++)
            {
                if (i < PropertynKoko / 10)
                {
                    palkki += '▮';
                }
                else
                    palkki += " ";
            }
            //Console.ResetColor();

            return palkki += "]";
        }

        public void NäytäSisältö(int kursoriVasen)
        {
            // Miten tulostus, jos repussa paljon tavaraa??
            if (Moottori.Pelaaja.Reppu.Count > 0)
            {
                foreach (Tavara item in Moottori.Pelaaja.Reppu)
                {
                    Konsoli.UusiRivi(kursoriVasen);
                    Console.Write("  [ " + item.Nimi + " ]");
                    Konsoli.UusiRivi(kursoriVasen);
                }
            }
            else
            {
                Konsoli.UusiRivi(kursoriVasen);
                Console.Write("  [        ]");
            }
        }
    }

}

