using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Periferia
{
    public interface IPiirrettävä
    {
        int Rivi { get; set; }
        int Sarake { get; set; } 
        char Merkki { get; set; }
        ConsoleColor Väri { get; set; }
        string Nimi { get; set; }

    }
}