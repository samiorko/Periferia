using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Periferia
{
    public class Hahmoruutu
    {
        const int hahmoruudunMaxLeveys = 25;
        const int hahmoruudunMaxKorkeus = 31;

        public Hahmoruutu()
        {
            Moottori.Pelaaja.HpMuuttunut += PelaajanHPMuuttunut;
            Moottori.Pelaaja.NesteMuuttunut += PelaajanNesteytysMuuttunut;
        }

        public void PelaajanNesteytysMuuttunut(object sender, EventArgs e)
        {
            Console.SetCursorPosition(Konsoli.HahmoRuutuOffset_Vasen + 12, Konsoli.HahmoRuutuOffset_Ylä + 6);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(PiirräPalkki(Moottori.Pelaaja.Nesteytys));
            Console.ResetColor();

            if (Moottori.Pelaaja.Nesteytys < 25 && Moottori.Pelaaja.Nesteytys > 0)
                Konsoli.Viestiloki.Lisää("Kuolet kohta janoon. Etsi vettä!", ConsoleColor.DarkBlue);
        }

        public void PelaajanHPMuuttunut(object sender, EventArgs e)
        {
            Console.SetCursorPosition(Konsoli.HahmoRuutuOffset_Vasen + 12, Konsoli.HahmoRuutuOffset_Ylä + 5);               // ESSI TSEKKAA KUN HP TOIMII
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(PiirräPalkki(Moottori.Pelaaja.HP));
            Console.ResetColor();

            if (Moottori.Pelaaja.HP < 25 && Moottori.Pelaaja.HP > 0)            
                Konsoli.Viestiloki.Lisää("HP vähissä.", ConsoleColor.DarkRed);
        }

        public void VihollisenHPMuuttunut(object sender, EventArgs e)
        {
            // Tätä kutsutaan kun vihollisen HP on muuttunut!
            // PiirräEntiteettienTiedot();
                                                                                                                            // ESSI TSEKKAA KUN HP TOIMII
        }

        public void Piirrä(int kursoriVasen, int kursoriYlä)
        {

            Console.SetCursorPosition(kursoriVasen, kursoriYlä);                                        // Asetetaan kursorin aloituspaikka
            Konsoli.PiirräReunatStringBuilder(kursoriVasen, kursoriYlä, hahmoruudunMaxKorkeus, hahmoruudunMaxLeveys);

            PiirräPelaajanTiedot(kursoriVasen, kursoriYlä);                                             // Piirretään pelaajan tiedot

            PiirräEntiteettienTiedot(kursoriVasen);

            foreach (Tavara entiteetti in Moottori.NykyinenKartta.Entiteetit.Where(e => e is Tavara))
            {
                entiteetti.Piirrä();
                Console.Write(" = " + entiteetti.Nimi);
                Konsoli.UusiRivi(kursoriVasen);
            }

        }

        public void PiirräPelaajanTiedot(int kursoriVasen, int kursoriYlä)
        {
            Console.SetCursorPosition(kursoriVasen, kursoriYlä);

            Console.Write("Pelaaja " + Moottori.Pelaaja.Merkki + ": " + Moottori.Pelaaja.Nimi);
            Konsoli.UusiRivi(kursoriVasen);
            Console.Write("LVL: " + Moottori.Pelaaja.Taso);
            Konsoli.UusiRivi(kursoriVasen);
            Console.Write("Voima: " + Moottori.Pelaaja.Voima);
            Konsoli.UusiRivi(kursoriVasen);
            Console.Write("Nopeus: " + Moottori.Pelaaja.Nopeus);
            Konsoli.UusiRivi(kursoriVasen);
            Console.Write("Onnekkuus: " + Moottori.Pelaaja.Onnekkuus);
            Konsoli.UusiRivi(kursoriVasen);
            Console.Write("HP:        \u2502");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(PiirräPalkki(Moottori.Pelaaja.HP));
            Console.ResetColor();
            Console.Write("\u2502");
            Console.ResetColor();
            Konsoli.UusiRivi(kursoriVasen);
            Console.Write("Nesteytys: \u2502");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(PiirräPalkki(Moottori.Pelaaja.Nesteytys));
            Console.ResetColor();
            Console.Write("\u2502");
            Konsoli.UusiRivi(kursoriVasen);
            Console.Write("Repun sisältö: ");
            NäytäRepunSisältö(kursoriVasen);
            Konsoli.UusiRivi(kursoriVasen);
            Konsoli.UusiRivi(kursoriVasen);
        }

        public void PiirräEntiteettienTiedot(int kursoriVasen)
        {
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
                else if (PropertynKoko < 10 && PropertynKoko > 0)
                {
                    palkki += '\u258C';
                    PropertynKoko -= PropertynKoko;
                }
                else
                    palkki += " ";
            }
            //Console.ResetColor();

            return palkki;
        }

        public void NäytäRepunSisältö(int kursoriVasen)
        {
            // Miten tulostus, jos repussa paljon tavaraa??

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
    }

}

