using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Periferia
{
    public class Vihollinen : IHahmo, IPiirrettävä
    {
        public int Voima { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int HP { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool OnkoYstävä { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool OnkoTekoäly { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Sarake { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Rivi { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public char Merkki { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ConsoleColor Väri { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Nimi { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Piirrä(Kartta k)
        {
            throw new NotImplementedException();
        }

        public void Tekoäly()
        {
            throw new System.NotImplementedException();
        }
    }
}