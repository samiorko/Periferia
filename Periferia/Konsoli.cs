﻿using System;
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

        public static void PiirräGameOver()
        {
            TyhjennäKonsoli();
            Console.ResetColor();
            string teksti = @"
    ___                              ___         ___       ___  
   (   )                            (   )  .-.  (   )     (   ) 
    | |   ___    ___  ___    .--.    | |  ( __)  | |_      | |  
    | |  (   )  (   )(   )  /    \   | |  (   ) (   __)    | |
    | |  ' /     | |  | |  |  .-. ;  | |   | |   | |       | |  
    | |,' /      | |  | |  | |  | |  | |   | |   | | ___   | |  
    | .  '.      | |  | |  | |  | |  | |   | |   | |(   )  | |  
    | | `. \     | |  | |  | |  | |  | |   | |   | | | |   | |
    | |   \ \    | |  ; '  | '  | |  | |   | |   | ' | |   |_|  
    | |    \ .   ' `-' / '    `-' /  | |   | |   ' `-';    .-.
   (___)  (___)   '.__.'    `.__.'  (___) (___)   `.__.   (   ) 
                                                           '-'
            ";
            PiirräReunatStringBuilder(4, 2, KonsoliKorkeus-3, KonsoliLeveys-5);
            Console.SetCursorPosition(10, 5);
            string[] rivit = teksti.Split('\n');
            foreach (var rivi in rivit)
            {
                Console.Write(rivi);
                Konsoli.UusiRivi(10);
            }
            Konsoli.UusiRivi(15);
            Console.Write($"{Moottori.Pelaaja.Nimi} heitti veivit seuraavilla statseilla:");
            Konsoli.UusiRivi(15);
            Console.Write($"\tTapetut:\t{Moottori.Pelaaja.MontakoTapettu}");
            Konsoli.UusiRivi(15);
            Console.Write($"\tPisteet:\t{Moottori.Pelaaja.Pisteet}");
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
            string nimi = "Pekka";
            Moottori.Vaikeustaso vaikeustaso = Moottori.Vaikeustaso.HELPPO;
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
                PiirräStatsPalkit(voima, nopeus, onnekkus, 8, 20, valinta);
                PiirräJatkaNappi(voima, nopeus, onnekkus, 15, 5, valinta == 5);

                ConsoleKeyInfo näppäin = Console.ReadKey(true);
                switch (näppäin.Key)
                {
                    case ConsoleKey.DownArrow:
                        if(valinta < maxValinta && !(valinta == maxValinta - 1 && voima + nopeus + onnekkus != 10))
                            
                                valinta++;
                        else
                            Console.Beep();
                        break;
                    case ConsoleKey.UpArrow:
                        if (valinta > 0)
                            valinta--;
                        else
                            Console.Beep();
                        break;
                    case ConsoleKey.LeftArrow:
                        if(valinta == 1)
                        {
                            switch (vaikeustaso)
                            {
                                case Moottori.Vaikeustaso.HELPPO:
                                    Console.Beep();
                                    break;
                                case Moottori.Vaikeustaso.VAIKEA:
                                    vaikeustaso = Moottori.Vaikeustaso.HELPPO;
                                    break;
                                case Moottori.Vaikeustaso.VAIKEIN:
                                    vaikeustaso = Moottori.Vaikeustaso.VAIKEA;
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
                                        Console.Beep();
                                        break;
                                    }
                                    voima--;
                                    break;
                                case 3: // nopeus
                                    if (nopeus == 1)
                                    {
                                        Console.Beep();
                                        break;
                                    }
                                    nopeus--;
                                    break;
                                case 4: // onnekkuus
                                    if (onnekkus == 1)
                                    {
                                        Console.Beep();
                                        break;
                                    }
                                    onnekkus--;
                                    break;
                            }
                        }
                        else
                        {
                            Console.Beep();
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (valinta == 1)
                        {
                            switch (vaikeustaso)
                            {
                                case Moottori.Vaikeustaso.HELPPO:
                                    vaikeustaso = Moottori.Vaikeustaso.VAIKEA;
                                    break;
                                case Moottori.Vaikeustaso.VAIKEA:
                                    vaikeustaso = Moottori.Vaikeustaso.VAIKEIN;
                                    break;
                                case Moottori.Vaikeustaso.VAIKEIN:
                                    Console.Beep();
                                    break;
                            }
                        }
                        else if (valinta == 2 || valinta == 3 || valinta == 4)
                        {
                            if(voima+nopeus+onnekkus == 10)
                            {
                                Console.Beep();
                                break;
                            }
                            switch (valinta)
                            {
                                case 2: // voima
                                    if (voima >= 5)
                                    {
                                        Console.Beep();
                                        break;
                                    }
                                    voima++;
                                    break;
                                case 3: // nopeus
                                    if (nopeus >= 5)
                                    {
                                        Console.Beep();
                                        break;
                                    }
                                    nopeus++;
                                    break;
                                case 4: // onnekkuus
                                    if (onnekkus >= 5)
                                    {
                                        Console.Beep();
                                        break;
                                    }
                                    onnekkus++;
                                    break;
                            }
                        }
                        else
                        {
                            Console.Beep();
                        }
                        break;
                    case ConsoleKey.Enter:
                        if (valinta == maxValinta)
                        {
                            Moottori.Pelaaja.Voima = voima;
                            Moottori.Pelaaja.Nopeus = nopeus;
                            Moottori.Pelaaja.Onnekkuus = onnekkus;
                            Moottori.Pelaaja.Nimi = nimi;
                            
                            flag = false;
                        }
                        else
                        {
                            Console.Beep();
                        }
                        break;

                }

                
            }

            
            

        }

        private static void PiirräJatkaNappi(int voima, int nopeus, int onnekkus, int y, int x, bool valinta)
        {
            Console.SetCursorPosition(x, y);
            if (voima + nopeus + onnekkus != 10)
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

        private static void PiirräStatsPalkit(int voima, int nopeus, int onnekkuus, int y, int x, int valinta)
        {
            Console.SetCursorPosition(x, y);
            piirräYksiPalkki(voima, valinta==2);
            Console.SetCursorPosition(x, y + 1);
            piirräYksiPalkki(nopeus, valinta == 3);
            Console.SetCursorPosition(x, y + 2);
            piirräYksiPalkki(onnekkuus, valinta == 4);

            Console.SetCursorPosition(x + 1, y + 3);
            Console.Write(10 - (voima+nopeus+onnekkuus));
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

        private static void piirräVaikeustaso(Moottori.Vaikeustaso vaikeustaso, int y, int x, bool valittu)
        {
            Console.SetCursorPosition(x, y);
            piirräVaikustasoPalkki("Helppo", vaikeustaso == Moottori.Vaikeustaso.HELPPO, valittu);
            piirräVaikustasoPalkki("Vaikea", vaikeustaso == Moottori.Vaikeustaso.VAIKEA, valittu);
            piirräVaikustasoPalkki("Ei kannata", vaikeustaso == Moottori.Vaikeustaso.VAIKEIN, valittu);
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