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
            int hahmoruudunMaxLeveys = 25;

            Console.SetCursorPosition(kursoriVasen, kursoriYlä); // Asetetaan kursorin aloituspaikka
            Konsoli.PiirräReunatStringWriter(kursoriVasen, kursoriYlä, 19, hahmoruudunMaxLeveys);
            Console.SetCursorPosition(kursoriVasen, kursoriYlä);

            Console.Write("Pelaaja " + Moottori.Pelaaja.Merkki + ": " + Moottori.Pelaaja.Nimi);
            Konsoli.UusiRivi(kursoriVasen);
            Console.Write("Voima: " + Moottori.Pelaaja.Voima);
            Konsoli.UusiRivi(kursoriVasen);
            Console.Write("HP:        \u2502");
            // värin valinta
            if (Moottori.Pelaaja.HP < 25)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Konsoli.Viestiloki.Lisää("HP vähissä.", ConsoleColor.DarkRed);
            }
            else if (Moottori.Pelaaja.HP == 0)
            {
                // GAME OVER
            }
            else
                Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(PiirräPalkki(Moottori.Pelaaja.HP));
            Console.ResetColor();
            Console.Write("\u2502");
            Console.ResetColor();
            Konsoli.UusiRivi(kursoriVasen);
            Console.Write("Nesteytys: \u2502");
            // värin valinta
            if (Moottori.Pelaaja.Nesteytys < 25)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Konsoli.Viestiloki.Lisää("Kuolet kohta janoon. Etsi vettä!", ConsoleColor.DarkBlue);
            }
            else if (Moottori.Pelaaja.Nesteytys == 0)
            {
                // GAME OVER
            }
            else
                Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(PiirräPalkki(Moottori.Pelaaja.Nesteytys));
            Console.ResetColor();
            Console.Write("\u2502");
            Konsoli.UusiRivi(kursoriVasen);
            Console.Write("Repun sisältö: ");
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
                    Console.ResetColor();
                    Console.Write($"LVL:{entiteetti.Taso}  V:{entiteetti.Voima}  N:{entiteetti.Nopeus}  O:{entiteetti.Onnekkuus}");
                    Konsoli.UusiRivi(kursoriVasen);
                    Console.Write($"HP: {entiteetti.HP}/  \u2502");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write(PiirräPalkki(entiteetti.HP));
                    Console.ResetColor();
                    Console.Write("\u2502");
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
            //Console.OutputEncoding = System.Text.Encoding.UTF8;
            string palkki = "";
            //Console.ForegroundColor = ConsoleColor.Magenta;

            for (int i = 0; i < 10; i++)
            {
                if (PropertynKoko >= 10)
                {
                    palkki += '\u2588';
                    PropertynKoko -= 10;
                }
                else if (PropertynKoko < 10 && PropertynKoko > 5)
                {
                    palkki += '\u258C';
                    PropertynKoko -= PropertynKoko;
                }
                //else if (PropertynKoko < 10 && PropertynKoko > 5)
                //{
                //    palkki += " ";
                //    PropertynKoko -= PropertynKoko;
                //}
                else
                    palkki += " ";
            }
            //Console.ResetColor();

            return palkki;
        }

        public void NäytäSisältö(int kursoriVasen)
        {
            // Miten tulostus, jos repussa paljon tavaraa??
            if (Moottori.Pelaaja.Reppu.Count > 0)
            {
                int i = 0;
                while (i < 3)
                {
                    Konsoli.UusiRivi(kursoriVasen);
                    if (i < Moottori.Pelaaja.Reppu.Count)
                    {
                        Console.Write("  [ " + Moottori.Pelaaja.Reppu[i].Nimi + " ]");
                    }
                    else
                    {
                        Console.Write("  [        ]");
                    }

                    i++;
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

