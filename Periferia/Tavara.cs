using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Periferia
{
    public class Tavara : ITavara, IPiirrettävä
    {
        public int Sarake { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Rivi { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public char Merkki { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ConsoleColor Väri { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Nimi { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Piirrä()
        {
            throw new NotImplementedException();
        }

    }
}