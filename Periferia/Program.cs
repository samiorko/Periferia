using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Periferia
{
    class Program
    {
        static void Main(string[] args)
        {
            Konsoli k = new Konsoli();
            Konsoli.AlustaKonsoli();
            Moottori.NykyinenKartta = Kartta.LuoKartta();
            

            bool pelijatkuu = true;
            while(pelijatkuu){
                Konsoli.Tyhjenn‰Konsoli();


                k.Piirr‰Kartta(Moottori.NykyinenKartta);


                Konsoli.NollaaKursori();

                ConsoleKeyInfo n‰pp‰in = Console.ReadKey();
                switch (n‰pp‰in.Key)
                {
                    case ConsoleKey.RightArrow:
                        Moottori.Pelaaja.LiikuOikealle();
                        break;
                    case ConsoleKey.LeftArrow:
                        Moottori.Pelaaja.LiikuVasemmalle();
                        break;
                    case ConsoleKey.UpArrow:
                        Moottori.Pelaaja.LiikuYlˆs();
                        break;
                    case ConsoleKey.DownArrow:
                        Moottori.Pelaaja.LiikuAlas();
                        break;

                }

                
            }

            k.Piirr‰HahmoRuutu();

            //for (int i = 0; i < 10; i++)
            //{
            //    Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXX");
            //}
            //Console.SetWindowSize(150, 35);
            //Console.SetCursorPosition(1, 1);
            //Console.BackgroundColor = ConsoleColor.DarkCyan;
            //Console.ForegroundColor = ConsoleColor.Magenta;
            //Console.Write("Y");


            //Console.SetCursorPosition(2, 2);

            //
            //k.Viestiloki.Lis‰‰("Moi");
            //k.Viestiloki.Lis‰‰("Moi 2");
            //k.Viestiloki.Lis‰‰("Moi 3");

            //k.Piirr‰Loki();




            
            Console.ReadKey();
        }
    }
}
