using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Periferia
{
    public class Karttaruutu : IPiirrettävä
    {
        public bool Käveltävä { get; set; }
        public Ruututyypit Tyyppi { get; set; }
        public IPiirrettävä Entiteetti { get; set; }
        public int X { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Y { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public char Merkki { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ConsoleColor Väri { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Nimi { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Piirrä(Kartta k)
        {
            throw new NotImplementedException();
        }

        public enum Ruututyypit
        {
            SISÄÄN,
            ULOS,
            PELASTUS,
            SEINÄ,
            PUU,
            TYHJÄ
        }
    }
}