using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Periferia
{
    public class Vihollinen : Hahmo, IPiirrettävä
    {

        public Vihollinen()
        {
            OnkoTekoäly = true;
        }

        public void Tekoäly()
        {
            bool vuoroKäytetty = false;
            int etäisyysX = this.Sarake - Moottori.Pelaaja.Sarake;
            int etäisyysY = this.Rivi - Moottori.Pelaaja.Rivi;


            if (Math.Abs(etäisyysX) >= Math.Abs(etäisyysY))
            { // x suunta isompi kuin y
                if (etäisyysX < 0 && this.LiikuOikealle())
                    vuoroKäytetty = true;
                else if (this.LiikuVasemmalle())
                    vuoroKäytetty = true;
            }
            if (!vuoroKäytetty)
            {
                if (etäisyysY < 0 && this.LiikuAlas())
                    vuoroKäytetty = true;
                else if (this.LiikuYlös())
                    vuoroKäytetty = true;
            }

            if (!vuoroKäytetty)
            {
                if (this.LiikuVasemmalle()){ }
                else if (this.LiikuYlös()) { }
                else if (this.LiikuOikealle()) { }
                else if (this.LiikuAlas()) { }
                vuoroKäytetty = true;
            }
        }
    }
}