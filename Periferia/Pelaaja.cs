using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Periferia
{
    public class Pelaaja : Hahmo, IHahmo, IPiirrettävä
    {
       
        public int Nesteytys { get; set; }

        public List<Tavara> Reppu { get; set; }


    }
}