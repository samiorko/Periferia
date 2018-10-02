using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Periferia
{
    public class Tavara : ITavara, IPiirrettävä
    {
        public int X { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Y { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public char Merkki { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ConsoleColor Väri { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Nimi { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Piirrä(Kartta k)
        {
            throw new NotImplementedException();
        }
    }
}