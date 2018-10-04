using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Periferia
{
    public class Tavara : ITavara, IPiirrettävä
    {
        public Tavara(string nimi)
        {
            Nimi = nimi;
        }

        public int Sarake { get; set; }
        public int Rivi { get; set; }
        public char Merkki { get; set; }
        public ConsoleColor Väri { get; set; }
        public string Nimi { get; set; }

        public void Piirrä()
        {
            Console.ForegroundColor = this.Väri;
            Console.Write(this.Merkki);
        }

    }
}