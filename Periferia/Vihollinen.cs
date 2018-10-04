using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Periferia
{
    public class Vihollinen : Hahmo, IPiirrettävä
    {

        static public Random Rnd = new Random();

        public int Näkökenttä { get; set; }

        public int EtäisyysPelaajasta { get
            {
                if (!this.Elossa)
                    return 9001;
                return (Math.Abs(Moottori.Pelaaja.Rivi - this.Rivi) + Math.Abs(Moottori.Pelaaja.Sarake - this.Sarake));
            }
        }

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
            if (etäisyys > 0)
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
            int etäisyysX = Moottori.Pelaaja.Sarake - this.Sarake;
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
                else if (Math.Abs(etäisyysX) > Math.Abs(etäisyysY) && etäisyysY != 0)
                {
                    if (!liikeY(etäisyysY))
                        liikeX(etäisyysX);
                }
                else if (Math.Abs(etäisyysX) == Math.Abs(etäisyysY))
                {
                    if (Moottori.Pelaaja.ViimeisinSuunta == Liikesuunnat.VASEN || Moottori.Pelaaja.ViimeisinSuunta == Liikesuunnat.OIKEA)
                    {
                        liikeY(etäisyysY);
                    }
                    else
                    {
                        liikeX(etäisyysX);
                    }
                }
                else if (etäisyysX == 0)
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

        static public Vihollinen Generoi(VihollisMalli malli)
        {
            Vihollinen vihu = new Vihollinen();
            int lvl = 1;
            if(Moottori.Pelaaja.Taso > 1) {
                // Pelaaja yli level 1, vihu voi olla sama tai yhden pienempi
                lvl = (int) Math.Ceiling((decimal) Moottori.VaikeusKerroin * Rnd.Next(Moottori.Pelaaja.Taso - 1, Moottori.Pelaaja.Taso));
            }
            vihu.Merkki = malli.Merkki;
            vihu.Väri = malli.Väri;
            vihu.OnkoTekoäly = true;
            vihu.HP = malli.HP;
            
            vihu.Voima = malli.Voima;
            vihu.Onnekkuus = malli.Onnekkuus;
            vihu.Nopeus = malli.Nopeus;
            vihu.Nimi = malli.Nimi;
            vihu.Hyökkäys = malli.Hyökkäys;
            vihu.Taso = lvl;
            vihu.Näkökenttä = malli.Näkökenttä;

            if(lvl > 1) {
                // Vihun leveli yli 1, generoidaan randomilla statseja 1 / leveli
                for (int i = 1; i < lvl; i++)
                {
                    int randomi = Rnd.Next(1, 5);
                    switch (randomi)
                    {
                        case 1:
                            vihu.Voima++;
                            break;
                        case 2:
                            vihu.Nopeus++;
                            break;
                        case 3:
                            vihu.Onnekkuus++;
                            break;
                        case 4:
                            vihu.HP += 10;
                            break;
                        default:
                            vihu.HP += 10;
                            break;
                    }
                }
            }
            vihu.MaksimiHP = vihu.HP;

            return vihu;
        }
    }

    public class VihollisMalli{
        public int HP = 50;
        public int Voima = 1;
        public int Nopeus = 1;
        public int Onnekkuus = 1;
        public int Näkökenttä = 5;
        public string Nimi;
        public char Merkki;
        public ConsoleColor Väri;
        public string Hyökkäys = "vahingoittaa";

        public VihollisMalli(string nimi, char merkki, ConsoleColor väri = ConsoleColor.DarkRed)
        {
            Nimi = nimi;
            Merkki = merkki;
            Väri = väri;
        }
    }
}