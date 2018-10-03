using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Periferia
{
    public class Vihollinen : Hahmo, IPiirrettävä
    {

        public int KokemusPalkinto
        {
            get
            {
                return 2;
            }
        }

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
                this.Hyökkää(Moottori.Pelaaja);
            }
            else
            {
                // Karhu ei ole vieressä, pyritään lähemmäs7

                if (Math.Abs(etäisyysX) < Math.Abs(etäisyysY) && etäisyysX != 0) // Pyritään vähentämään X
                {
                    if (!liikeX(etäisyysX))
                        liikeY(etäisyysY);
                }
                else if(Math.Abs(etäisyysX) > Math.Abs(etäisyysY) && etäisyysY != 0)
                {
                    if (!liikeY(etäisyysY))
                        liikeX(etäisyysX);
                }
                else if(Math.Abs(etäisyysX) == Math.Abs(etäisyysY))
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
                else if(etäisyysX == 0)
                {
                    liikeY(etäisyysY);
                }
                else if (etäisyysY == 0)
                {
                    liikeX(etäisyysX);
                }

            }

            Trace.WriteLine("-------------");
            Trace.WriteLine("Pelaaja:   " + Moottori.Pelaaja.Sarake + " / " + Moottori.Pelaaja.Rivi);
            Trace.WriteLine("Vihu:      " + this.Sarake + " / " + this.Rivi);
            Trace.WriteLine("ETäisyydet " + (Moottori.Pelaaja.Sarake - this.Sarake) + " / " + (Moottori.Pelaaja.Rivi - this.Rivi));


        }
    }
}