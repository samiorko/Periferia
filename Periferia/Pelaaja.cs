using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Periferia
{
    public class Pelaaja : IHahmo, IPiirrettävä
    {
        public int Nesteytys { get; set; }

        public List<Tavara> Reppu { get; set; }
        public int Voima { get; set; }
        public int HP { get; set; }
        public bool OnkoYstävä { get; set; }
        public bool OnkoTekoäly { get; set; }
        public int Sarake { get; set; }
        public int Rivi { get; set; }
        public char Merkki { get; set; }
        public ConsoleColor Väri { get; set; }
        public string Nimi { get; set; }


    }
}