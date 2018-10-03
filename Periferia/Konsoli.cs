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
               
        public const int KonsoliLeveys  = 150;
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
            Console.SetCursorPosition(offsetVasen, Console.CursorTop + 1);
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
        public static void Testi()
        {
            System.Diagnostics.Trace.WriteLine("test");
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

        public static void PiirräReunatStringWriter(int vasenYläkulma_SarakeNro, int vasenYläkulma_RiviNro, int rivimäärä, int sarakemäärä)
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