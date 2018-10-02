using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Periferia
{
    static class Moottori
    {
        static public List<Kartta> Kartat;
        static public Kartta NykyinenKartta;
        static public Pelaaja Pelaaja = new Pelaaja()
        {
            Väri = ConsoleColor.Yellow,
            Merkki = '@',
            HP = 25,
            Nesteytys = 100,
            Nimi = "Pekka",
            Voima = 50,
            Reppu = new List<Tavara>(),
            Sarake = 2,
            Rivi = 2
        };


        static public void Peli()
        {
            Konsoli k = new Konsoli();
            Konsoli.AlustaKonsoli();
            Moottori.NykyinenKartta = Kartta.LuoKartta();


            bool pelijatkuu = true;
            while (pelijatkuu)
            {
                Konsoli.TyhjennäKonsoli();


                k.PiirräKartta(Moottori.NykyinenKartta);
                k.PiirräHahmoRuutu();

                Konsoli.NollaaKursori();

                pelaajanVuoro();
                vihollistenVuoro();

            }
        }

        static void pelaajanVuoro()
        {
            ConsoleKeyInfo näppäin = Console.ReadKey();
            switch (näppäin.Key)
            {
                case ConsoleKey.RightArrow:
                    Moottori.Pelaaja.LiikuOikealle();
                    break;
                case ConsoleKey.LeftArrow:
                    Moottori.Pelaaja.LiikuVasemmalle();
                    break;
                case ConsoleKey.UpArrow:
                    Moottori.Pelaaja.LiikuYlös();
                    break;
                case ConsoleKey.DownArrow:
                    Moottori.Pelaaja.LiikuAlas();
                    break;

            }
        }

        static void vihollistenVuoro()
        {
            foreach (Vihollinen v in NykyinenKartta.Entiteetit.Where(v => v is Vihollinen))
            {
                if (!v.OnkoTekoäly)
                    continue; // älytön tyyppi, mennään seuraavaan

                v.Tekoäly();
            }

        }
    }
}
