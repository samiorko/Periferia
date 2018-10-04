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
            //Moottori.Pelaaja.HpMuuttunut += PelaajanHPMuuttunut;
            //Moottori.Pelaaja.NesteMuuttunut += PelaajanNesteytysMuuttunut;
        }

        public void PelaajanNesteytysMuuttunut(object sender, EventArgs e)
        {
            Console.SetCursorPosition(Konsoli.HahmoRuutuOffset_Vasen + 12, Konsoli.HahmoRuutuOffset_Ylä + 6);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"{PiirräPalkki(Moottori.Pelaaja.Nesteytys),10}");
            Console.ResetColor();

            if (Moottori.Pelaaja.Nesteytys < 25 && Moottori.Pelaaja.Nesteytys > 0)
                Konsoli.Viestiloki.Lisää("Kuolet kohta janoon. Etsi vettä!", ConsoleColor.DarkBlue);
        }

        public void PelaajanHPMuuttunut(object sender, EventArgs e)
        {
            float hpProsentti = (float)Moottori.Pelaaja.HP / (float)Moottori.Pelaaja.MaksimiHP * 100.0f;

            Console.SetCursorPosition(Konsoli.HahmoRuutuOffset_Vasen, Konsoli.HahmoRuutuOffset_Ylä + 5);
            Console.ResetColor();
            Console.Write($"HP:{Moottori.Pelaaja.HP, 3}/{Moottori.Pelaaja.MaksimiHP,3} \u2502");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write($"{PiirräPalkki((int)hpProsentti),10}");
            Console.ResetColor();

            if (hpProsentti < 25.0f && Moottori.Pelaaja.HP > 0f)
                Konsoli.Viestiloki.Lisää("HP vähissä.", ConsoleColor.DarkRed);
        }

        public void VihollisenHPMuuttunut(object sender, EventArgs e)
        {
            //PiirräEntiteettienTiedot(Konsoli.HahmoRuutuOffset_Vasen);
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

            Console.Write($"Pelaaja {Moottori.Pelaaja.Merkki,1}: {Moottori.Pelaaja.Nimi,10}");
            Konsoli.UusiRivi(kursoriVasen);
            Console.Write("LVL: " + Moottori.Pelaaja.Taso);
            Konsoli.UusiRivi(kursoriVasen);
            Console.Write("Voima: " + Moottori.Pelaaja.Voima);
            Konsoli.UusiRivi(kursoriVasen);
            Console.Write("Nopeus: " + Moottori.Pelaaja.Nopeus);
            Konsoli.UusiRivi(kursoriVasen);
            Console.Write("Onnekkuus: " + Moottori.Pelaaja.Onnekkuus);
            Konsoli.UusiRivi(kursoriVasen);
            Console.Write($"HP:{Moottori.Pelaaja.HP,3}/{Moottori.Pelaaja.MaksimiHP,3} \u2502");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write($"{PiirräPalkki(Moottori.Pelaaja.HP),10}");
            Console.ResetColor();
            Console.Write("\u2502");
            Console.ResetColor();
            Konsoli.UusiRivi(kursoriVasen);
            Console.Write("Nesteytys: \u2502");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"{PiirräPalkki(Moottori.Pelaaja.Nesteytys),10}");
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
                    Console.Write($"HP: {entiteetti.HP}/{entiteetti.MaksimiHP}  \u2502");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write($"{PiirräPalkki(entiteetti.HP),12}");
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

