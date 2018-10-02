using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Periferia
{
    public interface IPiirrettävä
    {
        int X { get; set; } 
        int Y { get; set; }
        char Merkki { get; set; }
        ConsoleColor Väri { get; set; }
        string Nimi { get; set; }

        void Piirrä(Kartta k);
    }
}