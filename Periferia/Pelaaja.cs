using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Periferia
{
    public class Pelaaja : Hahmo, IPiirrettävä
    {
       
        public int Nesteytys { get; set; }

        private int _kokemus;

        public int Kokemus {
            get
            {
                return _kokemus;
            }
            set {
                _kokemus = _kokemus + value;
                if(_kokemus > 10)
                {
                    Taso++;
                    _kokemus = _kokemus - 10;
                    Voima++;
                    Nopeus++;
                    Onnekkuus++;
                }
            }
        }


        public List<Tavara> Reppu { get; set ; }


    }
}