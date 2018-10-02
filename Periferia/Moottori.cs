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
            HP = 100,
            Nesteytys = 100,
            Nimi = "Pekka",
            Voima = 50,
            Reppu = new List<Tavara>(),
            Sarake = 2,
            Rivi = 2
        };
    }
}
