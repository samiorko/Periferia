using System;
using System.Collections.Generic;
using System.Diagnostics;
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


        private bool liikeX(int etäisyys)
        {
            if(etäisyys > 0)
            {
                // Yritetään liikkua oikealle
                Trace.WriteLine("AI Yritys: Oikealle");
                return this.LiikuOikealle();

            }
            else
            {
                // Yritetään liikkua vasemmalle
                Trace.WriteLine("AI Yritys: Vasemmalle");
                return this.LiikuVasemmalle();
            }
        }
        private bool liikeY(int etäisyys)
        {
            if (etäisyys > 0)
            {
                // Yritetään liikkua alas
                Trace.WriteLine("AI Yritys: Alas");
                return this.LiikuAlas();

            }
            else
            {
                // Yritetään liikkua ylös
                Trace.WriteLine("AI Yritys: Ylös");
                return this.LiikuYlös();
            }
        }

        public void Tekoäly()
        {
            bool vuoroKäytetty = false;
            int etäisyysX =  Moottori.Pelaaja.Sarake - this.Sarake;
            int etäisyysY = Moottori.Pelaaja.Rivi - this.Rivi;

            if (Math.Abs(etäisyysX) + Math.Abs(etäisyysY) == 1)
            {
                // Karhu on vieressä, HYÖKKÄÄ!
            }
            else
            {
                // Karhu ei ole vieressä, pyritään lähemmäs7

                if (Math.Abs(etäisyysX) > Math.Abs(etäisyysY)) // Pyritään vähentämään X
                {
                    if (!liikeX(etäisyysX))
                        liikeY(etäisyysY);
                }
                else if(Math.Abs(etäisyysX) < Math.Abs(etäisyysY))
                {
                    if (!liikeY(etäisyysY))
                        liikeX(etäisyysX);
                }
                else
                {
                    if(Moottori.Pelaaja.ViimeisinSuunta == Liikesuunnat.VASEN || Moottori.Pelaaja.ViimeisinSuunta == Liikesuunnat.OIKEA)
                    {
                        liikeY(etäisyysY);
                    }
                    else
                    {
                        liikeX(etäisyysX);
                    }
                }
                
            }

            Trace.WriteLine("-------------");
            Trace.WriteLine("Pelaaja:   " + Moottori.Pelaaja.Sarake + " / " + Moottori.Pelaaja.Rivi);
            Trace.WriteLine("Vihu:      " + this.Sarake + " / " + this.Rivi);
            Trace.WriteLine("ETäisyydet " + (Moottori.Pelaaja.Sarake - this.Sarake) + " / " + (Moottori.Pelaaja.Rivi - this.Rivi));

            //bool vuoroKäytetty = false;
            //int etäisyysX = this.Sarake - Moottori.Pelaaja.Sarake;
            //int etäisyysY = this.Rivi - Moottori.Pelaaja.Rivi;


            //if (Math.Abs(etäisyysX) >= Math.Abs(etäisyysY))
            //{ // x suunta isompi kuin y
            //    if (etäisyysX < 0 && this.LiikuOikealle())
            //        vuoroKäytetty = true;
            //    else if (this.LiikuVasemmalle())
            //        vuoroKäytetty = true;
            //}
            //if (!vuoroKäytetty)
            //{
            //    if (etäisyysY < 0 && this.LiikuAlas())
            //        vuoroKäytetty = true;
            //    else if (this.LiikuYlös())
            //        vuoroKäytetty = true;
            //}

            //if (!vuoroKäytetty)
            //{
            //    if (this.LiikuVasemmalle()){ }
            //    else if (this.LiikuYlös()) { }
            //    else if (this.LiikuOikealle()) { }
            //    else if (this.LiikuAlas()) { }
            //    vuoroKäytetty = true;
            //}
        }
    }
}