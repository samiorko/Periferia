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
        public ConsoleColor Väri { get; set; } = ConsoleColor.White;
        public string Nimi { get; set; }

        public int PlusVoima { get; set; } = 0;
        public int PlusNopeus { get; set; } = 0;
        public int PlusOnnekkuus { get; set; } = 0;

        public bool Poimittava { get; set; } = false;

        public bool BoostaaStatseja
        {
            get {
                return (PlusNopeus + PlusOnnekkuus + PlusVoima > 0);
            }
        }



        public void Piirrä()
        {
            Console.ForegroundColor = this.Väri;
            Console.Write(this.Merkki);
        }
        public void PiirräNimi()
        {
            Console.ForegroundColor = this.Väri;
            Console.Write(this.Nimi);
        }

    }
}