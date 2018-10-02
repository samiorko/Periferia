using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Periferia
{
    public class Vihollinen : Hahmo, IHahmo, IPiirrettävä
    {

        public Vihollinen()
        {
            OnkoTekoäly = true;
        }

        public void Tekoäly()
        {
            int etäisyysX = Math.Abs(Moottori.Pelaaja.Sarake - this.Sarake);
            int etäisyysY = Math.Abs(Moottori.Pelaaja.Rivi - this.Rivi);
            
        }
    }
}