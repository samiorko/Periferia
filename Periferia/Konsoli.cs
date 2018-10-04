using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Periferia
{
    public static class Konsoli
    {
        public const int KarttaOffset_Vasen = 4;
        public const int KarttaOffset_Ylä = 2;

        public const int HahmoRuutuOffset_Vasen = 4 + Kartta.KARTTALEVEYS + 5;
        public const int HahmoRuutuOffset_Ylä = 2;
               
        public const int ViestiLokiOffset_Vasen = 4;
        public const int ViestiLokiOffset_Ylä  = 18;
               
        public const int KonsoliLeveys  = 95;
        public const int KonsoliKorkeus = 35;
               
        public static Viestiloki Viestiloki = new Viestiloki();
        public static Hahmoruutu Hahmoruutu = new Hahmoruutu();

        public static void PiirräKartta(Kartta k)
        {
            Console.ResetColor();
            k.Piirrä();
        }

        public static void PiirräLoki()
        {
            Console.ResetColor();

            Viestiloki.Piirrä();

        }

        public static void PiirräHahmoRuutu()
        {
            Console.ResetColor();
            Hahmoruutu.Piirrä(HahmoRuutuOffset_Vasen, HahmoRuutuOffset_Ylä);
        }

        public static void UusiRivi(int offsetVasen)
        {
            try
            {
                Console.SetCursorPosition(offsetVasen, Console.CursorTop + 1);
            } catch(Exception ex) { } // HV virheelle
        }

        public static void AlustaKonsoli()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.CursorVisible = false;
            Console.SetWindowSize(KonsoliLeveys, KonsoliKorkeus);
            Console.SetBufferSize(KonsoliLeveys, KonsoliKorkeus);
            Console.SetCursorPosition(0, 0);
            TyhjennäKonsoli();
            Console.ResetColor();
        }

        public static void piirräAloitusnäyttö()
        {
            TyhjennäKonsoli();
            Console.ResetColor();
            string kuva = @"
               ,@@@@@@@,
       ,,,.   ,@@@@@@/@@,  .oo8888o.
    ,&%%&%&&%,@@@@@/@@@@@@,8888\88/8o
   ,%&\%&&%&&%,@@@\@@@/@@@88\88888/88'
   %&&%&%&/%&&%@@\@@/ /@@@88888\88888'
   %&&%/ %&%%&&@@\ V /@@' `88\8 `/88'
   `&%\ ` /%&'    |.|        \ '|8'
       |o|        | |         | |
       |.|        | |         | |
    \\/ ._\//_/__/  ,\_//__\\/.  \_//__/_";
            string pelinNimi = @"
 __, __, __, _ __, __, __, _  _,
 |_) |_  |_) | |_  |_  |_) | /_\
 |   |   | \ | |   |   | \ | | |
 ^   ^^^ ^ ^ ^ ^   ^^^ ^ ^ ^ ^ ^";
            PiirräReunatStringBuilder(4, 2, KonsoliKorkeus - 3, KonsoliLeveys - 5);
            Console.SetCursorPosition(30, 5);
            string[] rivit = pelinNimi.Split('\n');
            foreach (var rivi in rivit)
            {
                Console.Write(rivi);
                Konsoli.UusiRivi(30);
            }
            Konsoli.UusiRivi(25);
            string[] kuvaRivit = kuva.Split('\n');
            foreach (var rivi in kuvaRivit)
            {
                Console.Write(rivi);
                Konsoli.UusiRivi(25);
            }

            Konsoli.UusiRivi(20);
            Konsoli.UusiRivi(20);
            Console.Write("Huomaat eksyneesi tiheäkasvuiseen metsään. Asutuksesta");
            Konsoli.UusiRivi(20);
            Console.Write("ei ole havaittavissa minkäänlaisia merkkejä. Huomaat, ");
            Konsoli.UusiRivi(20);
            Console.Write("ettei sinulla ole mukanasi vesileiliä, ja repustasi");
            Konsoli.UusiRivi(20);
            Console.Write("löytyy vain retkisaha. Löydätköhän koskaan pois?");

            Konsoli.UusiRivi(33);
            Konsoli.UusiRivi(33);
            Console.Write("Paina välilyönti tai ENTER");
            Konsoli.UusiRivi(25);
            ConsoleKey jatka;
            do
            {
                jatka = Console.ReadKey(true).Key;
            } while (jatka != ConsoleKey.Spacebar && jatka != ConsoleKey.Enter);
            Console.Clear();
        }

        public static void PiirräGameOver()
        {
            TyhjennäKonsoli();
            Console.ResetColor();
            string teksti = @"
[##   [##    [##     [##      [####       [##       [##  [###[######
[##  [##     [##     [##    [##    [##    [##       [##      [##    
[## [##      [##     [##  [##        [##  [##       [##      [##    
[# [#        [##     [##  [##        [##  [##       [##      [##    
[##  [##     [##     [##  [##        [##  [##       [##      [##    
[##   [##    [##     [##    [##     [##   [##       [##      [##    
[##     [##    [#####         [####       [######## [##      [##
            ";
            PiirräReunatStringBuilder(4, 2, KonsoliKorkeus-3, KonsoliLeveys-5);
            Console.SetCursorPosition(14, 8);
            string[] rivit = teksti.Split('\n');
            foreach (var rivi in rivit)
            {
                Console.Write(rivi);
                Konsoli.UusiRivi(14);
            }
            Konsoli.UusiRivi(20);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(Moottori.Pelaaja.Nimi);
            Console.ResetColor();
            Console.Write($" heitti veivit seuraavilla statseilla:");
            TulostaLopetusStatsit();
            Konsoli.UusiRivi(20);
            Konsoli.UusiRivi(20);
            Console.Write("Paina ESC lopettaaksesi");
        }
        
        public static void PiirräPelastus()
        {
            TyhjennäKonsoli();
            Console.ResetColor();
            string teksti = @"
 _    _     _      _                     _ _       _ 
| |  | |   (_)_   (_)_                  | (_)     | |
| |  | |__  _| |_  _| |_     ____   ____| |_ ____ | |
 \ \/ / _ \| |  _)| |  _)   |  _ \ / _  ) | |  _ \|_|
  \  / |_| | | |__| | |__   | | | ( (/ /| | | | | |_ 
   \/ \___/|_|\___)_|\___)  | ||_/ \____)_|_|_| |_|_|
                            |_|                      
";
            PiirräReunatStringBuilder(4, 2, KonsoliKorkeus - 3, KonsoliLeveys - 5);
            Console.SetCursorPosition(14, 8);
            string[] rivit = teksti.Split('\n');
            foreach (var rivi in rivit)
            {
                Console.Write(rivi);
                Konsoli.UusiRivi(14);
            }
            Konsoli.UusiRivi(20);
            Console.Write($"{Moottori.Pelaaja.Nimi} voitti pelin seuraavilla statseilla:");
            TulostaLopetusStatsit();
        }

        public static void TulostaLopetusStatsit()
        {
            Konsoli.UusiRivi(20);
            Konsoli.UusiRivi(20);
            Console.Write($"\tTaso:\t\t\t{Moottori.Pelaaja.Taso}");
            Konsoli.UusiRivi(20);
            Console.Write($"\tVaelletut kartat:\t{Moottori.Kartat.Count}");
            Konsoli.UusiRivi(20);
            Console.Write($"\tTapetut:\t\t{Moottori.Pelaaja.MontakoTapettu}");
            Konsoli.UusiRivi(20);
            Console.Write($"\tPisteet:\t\t{Moottori.Pelaaja.Pisteet}");
            for (int i = 0; i < 5; i++)
            {
                Konsoli.UusiRivi(20);
            }
        }
        public static void TyhjennäKonsoli()
        {
            Console.Clear();
        }

        public static void NollaaKursori()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            //Console.SetCursorPosition(KonsoliLeveys - 1, KonsoliKorkeus - 1);
            Console.SetCursorPosition(0, 0);
        }

        static public void PiirräReunatStringBuilder(int vasenYläkulma_SarakeNro, int vasenYläkulma_RiviNro, int rivimäärä, int sarakemäärä)
        {  // Piirtää reunat objekteille StringWriterin avulla

            StringBuilder reunanpiirtäjä = new StringBuilder();

            Console.SetCursorPosition(vasenYläkulma_SarakeNro - 2, vasenYläkulma_RiviNro - 1);
            reunanpiirtäjä.Append("╔");
            for (int i = 0; i < sarakemäärä; i++)
            {
                reunanpiirtäjä.Append("═");                         // yläreuna
            }
            reunanpiirtäjä.Append("╗" + Environment.NewLine);
            LisääSisennys(vasenYläkulma_SarakeNro - 2);
            for (int j = 0; j < rivimäärä; j++)
            {
                reunanpiirtäjä.Append("║");                         // vasen reuna
                for (int k = 0; k < sarakemäärä; k++)
                {
                    reunanpiirtäjä.Append(" ");
                }
                reunanpiirtäjä.Append("║" + Environment.NewLine);   // oikea reuna
                LisääSisennys(vasenYläkulma_SarakeNro - 2);
            }
            reunanpiirtäjä.Append("╚");
            for (int i = 0; i < sarakemäärä; i++)
            {
                reunanpiirtäjä.Append("═");                         // alarauna
            }
            reunanpiirtäjä.Append("╝");
            Console.Write(reunanpiirtäjä);

            // apumuuttuja sisennysten tekoon:
            void LisääSisennys(int sisennys)
            {
                for (int i = 0; i < vasenYläkulma_SarakeNro - 2; i++)
                    reunanpiirtäjä.Append(" ");
            }
        }

        public static void HahmonLuonti()
        {
            int valinta = 0;
            int maxValinta = 5;
            int voima = 1;
            int nopeus = 1;
            int onnekkus = 1;
            int maksimiStatsit = 10;
            int hp = 100;
            string nimi = "";
            Moottori.Vaikeustasot vaikeustaso = Moottori.Vaikeustasot.HELPPO;
            Console.Clear();

            Console.SetCursorPosition(5, 2);
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.Write("Luo hahmo");
            Konsoli.UusiRivi(5);
            Konsoli.UusiRivi(5);
            Console.ResetColor();
            Console.Write("Nimi: ");
            Konsoli.UusiRivi(5);
            Konsoli.UusiRivi(5);
            Console.Write("Vaikeustaso: ");
            Konsoli.UusiRivi(5);
            Konsoli.UusiRivi(5);
            Console.Write("Voima: ");
            Konsoli.UusiRivi(5);
            Console.Write("Nopeus: ");
            Konsoli.UusiRivi(5);
            Console.Write("Onnekkuus: ");
            Konsoli.UusiRivi(20);
            Console.Write(" X jäljellä");

            

            bool flag = true;
            while (flag)
            {
                piirräNimiKenttä(nimi, 4, 20, valinta == 0);
                piirräVaikeustaso(vaikeustaso, 6, 20, valinta == 1);
                PiirräStatsPalkit(voima, nopeus, onnekkus, 8, 20, valinta, maksimiStatsit);
                PiirräJatkaNappi(voima, nopeus, onnekkus, 15, 5, valinta == 5, maksimiStatsit);

                ConsoleKeyInfo näppäin = Console.ReadKey(true);
                switch (näppäin.Key)
                {
                    case ConsoleKey.DownArrow:
                        if(valinta == 0 && nimi.Length < 1)
                        {
                            nimi = "Pekka";
                        }
                        if(valinta == 0 && nimi.ToLower() == "maija")
                        {
                            maksimiStatsit = 15;
                            hp = 1000;
                            voima = 5;
                            nopeus = 5;
                            onnekkus = 5;
                        }
                        if (valinta == 0 && nimi.ToLower() == "hannu")
                        {
                            maksimiStatsit = 22;
                            voima = 1;
                            nopeus = 1;
                            onnekkus = 20;
                        }

                        if (valinta < maxValinta && !(valinta == maxValinta - 1 && voima + nopeus + onnekkus != maksimiStatsit))
                                valinta++;
                        
                        break;
                    case ConsoleKey.UpArrow:
                        if (valinta > 0)
                            valinta--;
                        if (valinta == 0 && nimi == "Pekka")
                            nimi = "";
                        
                        break;
                    case ConsoleKey.LeftArrow:
                        if(valinta == 1)
                        {
                            switch (vaikeustaso)
                            {
                                case Moottori.Vaikeustasot.HELPPO:
                                    break;
                                case Moottori.Vaikeustasot.VAIKEA:
                                    vaikeustaso = Moottori.Vaikeustasot.HELPPO;
                                    break;
                                case Moottori.Vaikeustasot.VAIKEIN:
                                    vaikeustaso = Moottori.Vaikeustasot.VAIKEA;
                                    break;
                            }
                        }
                        else if (valinta == 2 || valinta == 3 || valinta == 4)
                        {

                            switch (valinta)
                            {
                                case 2: // voima
                                    if (voima == 1)
                                    {
                                        break;
                                    }
                                    voima--;
                                    break;
                                case 3: // nopeus
                                    if (nopeus == 1)
                                    {
                                        break;
                                    }
                                    nopeus--;
                                    break;
                                case 4: // onnekkuus
                                    if (onnekkus == 1)
                                    {
                                        break;
                                    }
                                    onnekkus--;
                                    break;
                            }
                        }
                
                        break;
                    case ConsoleKey.RightArrow:
                        if (valinta == 1)
                        {
                            switch (vaikeustaso)
                            {
                                case Moottori.Vaikeustasot.HELPPO:
                                    vaikeustaso = Moottori.Vaikeustasot.VAIKEA;
                                    break;
                                case Moottori.Vaikeustasot.VAIKEA:
                                    vaikeustaso = Moottori.Vaikeustasot.VAIKEIN;
                                    break;
                                case Moottori.Vaikeustasot.VAIKEIN:
                                    break;
                            }
                        }
                        else if (valinta == 2 || valinta == 3 || valinta == 4)
                        {
                            if(voima+nopeus+onnekkus == maksimiStatsit)
                            {
                                break;
                            }
                            switch (valinta)
                            {
                                case 2: // voima
                                    if (voima >= 5)
                                    {
                                        break;
                                    }
                                    voima++;
                                    break;
                                case 3: // nopeus
                                    if (nopeus >= 5)
                                    {
                                        break;
                                    }
                                    nopeus++;
                                    break;
                                case 4: // onnekkuus
                                    if (onnekkus >= 5)
                                    {
                                        break;
                                    }
                                    onnekkus++;
                                    break;
                            }
                        }
                     
                        break;
                    case ConsoleKey.Enter:
                        if (valinta == maxValinta)
                        {
                            Moottori.Pelaaja.Voima = voima;
                            Moottori.Pelaaja.Nopeus = nopeus;
                            Moottori.Pelaaja.Onnekkuus = onnekkus;
                            Moottori.Pelaaja.Nimi = nimi;
                            Moottori.Pelaaja.HP = hp;
                            Moottori.Pelaaja.MaksimiHP = hp;
                            Moottori.Vaikeustaso = vaikeustaso;

                            
                            flag = false;
                        }
                    
                        break;
                    case ConsoleKey.Backspace:
                        if (valinta == 0 && nimi.Length > 0)
                        {
                            nimi = nimi.Substring(0, nimi.Length - 1);
                        }
                        break;
                    default:
                        if (char.IsLetter(näppäin.KeyChar) && nimi.Length < 10)
                        {
                            nimi += näppäin.KeyChar;
                        }
                        break;
                }

                
            }

            
            

        }

        private static void PiirräJatkaNappi(int voima, int nopeus, int onnekkus, int y, int x, bool valinta, int maksimiStatsit)
        {
            Console.SetCursorPosition(x, y);
            if (voima + nopeus + onnekkus != maksimiStatsit)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("[ Valitse kaikki statsit jatkaaksesi! ]");
            }
            else
            {
                if (valinta)
                    Console.BackgroundColor = ConsoleColor.Magenta;
                Console.Write("[ Jatka > ]");
                Console.ResetColor();
                Console.Write("                                                ");
            }
            
        }

        private static void PiirräStatsPalkit(int voima, int nopeus, int onnekkuus, int y, int x, int valinta, int maksimiStatsit)
        {
            Console.SetCursorPosition(x, y);
            piirräYksiPalkki(voima, valinta==2);
            Console.SetCursorPosition(x, y + 1);
            piirräYksiPalkki(nopeus, valinta == 3);
            Console.SetCursorPosition(x, y + 2);
            piirräYksiPalkki(onnekkuus, valinta == 4);

            Console.SetCursorPosition(x + 1, y + 3);
            Console.Write(maksimiStatsit - (voima+nopeus+onnekkuus));
            Console.ResetColor();
        }

        private static void piirräYksiPalkki(int maara, bool valittu)
        {
            Console.ResetColor();
            string palkki = "";
            for (int i = 0; i < maara; i++)
            {
                palkki += '\u2588';
                palkki += '\u2588';
            }

            Console.Write("|");
            if (valittu)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
            }
            Console.Write($"{palkki,-10}");
            Console.ResetColor();
            Console.Write("| ");
            Console.Write(maara);
        }

        private static void piirräVaikeustaso(Moottori.Vaikeustasot vaikeustaso, int y, int x, bool valittu)
        {
            Console.SetCursorPosition(x, y);
            piirräVaikustasoPalkki("Helppo", vaikeustaso == Moottori.Vaikeustasot.HELPPO, valittu);
            piirräVaikustasoPalkki("Vaikea", vaikeustaso == Moottori.Vaikeustasot.VAIKEA, valittu);
            piirräVaikustasoPalkki("Ei kannata", vaikeustaso == Moottori.Vaikeustasot.VAIKEIN, valittu);
        }

        private static void piirräVaikustasoPalkki(string str, bool nykyinen, bool valinta)
        {
            Console.ResetColor();
            if (nykyinen)
            {
                if (!valinta)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Magenta;
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.Write($" {str} ");



            Console.ResetColor();
            Console.Write(" ");
        }

        private static void piirräNimiKenttä(string nimi, int y, int x, bool valittu)
        {
            Console.ResetColor();
            Console.SetCursorPosition(x, y);

            Console.ResetColor();
            if (valittu)
            {
                Console.BackgroundColor = ConsoleColor.Magenta;
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.Write($" {nimi,-15}");

            Console.ResetColor();
        }

        public static void PiirräReunatConsole(int vasenYläkulma_SarakeNro, int vasenYläkulma_RiviNro, int rivimäärä, int sarakemäärä)
        {  // Piirtää reunat objekteille suoraan Consolella

            Console.SetCursorPosition(vasenYläkulma_SarakeNro - 2, vasenYläkulma_RiviNro - 1);

            Console.Write("╔");                             
            for (int i = 0; i < sarakemäärä; i++)
            {
                Console.Write("═");                         // yläreuna
            }
            Console.Write("╗");
            for (int j = 0; j < rivimäärä; j++)
            {
                Konsoli.UusiRivi(vasenYläkulma_SarakeNro - 2);
                Console.Write("║");                         // vasen reuna

                for (int k = 0; k < sarakemäärä; k++)
                {
                    Console.Write(" ");
                }
                Console.Write("║");                         // oikea reuna
            }
            Konsoli.UusiRivi(vasenYläkulma_SarakeNro - 2);
            Console.Write("╚");
            for (int i = 0; i < sarakemäärä; i++)
            {
                Console.Write("═");                         // alareuna
            }
            Console.Write("╝");
        }
    }



}