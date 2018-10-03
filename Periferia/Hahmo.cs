using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Periferia
{
    abstract public class Hahmo
    {
        public event EventHandler HpMuuttunut;

        public int Taso { get; set; }
        public int Voima { get; set; }
        public int Nopeus { get; set; }
        public int Onnekkuus { get; set; }

        private int hp;

        public int HP
        {
            get { return hp; }
            set {
                HpMuuttunut?.Invoke(this, EventArgs.Empty);
                if (value <= 0)
                {
                    hp = 0;
                    Kuole();
                } else
                {
                    hp = value;
                }
            }
        }
        public int MaksimiHP { get; set; }

        public virtual void Kuole()
        {
            // Poistetaan karttaruudulla oleva hahmo
            Moottori.NykyinenKartta.Ruudut[this.Sarake, this.Rivi].Entiteetti = null;
        }
        public int Sarake { get; set; }
        public int Rivi { get; set; }
        public char Merkki { get; set; }
        public ConsoleColor Väri { get; set; }
        public string Nimi { get; set; }
        public bool OnkoYstävä { get; set; }
        public bool OnkoTekoäly { get; set; }
        public Liikesuunnat ViimeisinSuunta { get; private set; }

        public int ViimeSarake { get; set; }
        public int ViimeRivi { get; set; }

        public void Hyökkää(Hahmo kohde)
        {
            // (1) Tarkistetaan, osuuko hyökkäys
            if (osuukoHyökkäysNopeus(kohde) || osuukoHyökkäysOnni(kohde))
            {
                // (2) Kuinka paljon vahinkoa tehdään?
                int vahinko = laskeVahinko();
                // (3) Onko kriittinen vahinko?
                if (onkoKriittinenOsuma(kohde))
                {
                    vahinko *= 2;
                }
                // Vähennetään kohteen hp vahingon mukaan
                kohde.HP -= vahinko;
            }


        }

        private bool onkoKriittinenOsuma(Hahmo kohde)
        {
            throw new NotImplementedException();
        }

        private int laskeVahinko()
        {
            throw new NotImplementedException();
        }

        private bool osuukoHyökkäysOnni(Hahmo kohde)
        {
            float suhdeLuku = ((float)this.Onnekkuus) / ((float)kohde.Onnekkuus);
            // määritetään tod.näk. sille, että hyökkäys osuu
            double tn = 0.0f;
            if (suhdeLuku > 2.0f)
            {
                tn = 1.0f;
            }
            else if (suhdeLuku < 0.1f)
            {
                tn = 0.0f;
            }
            else
            {
                tn = -0.1140351f + (1.171053f * suhdeLuku) - (0.3070175f * Math.Pow(suhdeLuku, 2));
            }
            // varmistetaan, ettei tn pääse alle 0 tai yli 1
            tn = Math.Min(tn, 1.0f);
            tn = Math.Max(tn, 0.0f);
            Random r = new Random();
            int randomLuku = r.Next(1, 1000);
            if (randomLuku > tn * 1000.0f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool osuukoHyökkäysNopeus(Hahmo kohde)
        {
            float suhdeLuku = ((float) this.Nopeus) / ((float) kohde.Nopeus);
            // määritetään tod.näk. sille, että hyökkäys osuu
            double tn = 0.0f;
            if (suhdeLuku > 2.0f)
            {
                tn = 1.0f;
            }
            else if (suhdeLuku < 0.1f)
            {
                tn = 0.0f;
            }
            else
            {
                tn = -0.1140351f + (1.171053f * suhdeLuku) - (0.3070175f * Math.Pow(suhdeLuku, 2));
            }
            // varmistetaan, ettei tn pääse alle 0 tai yli 1
            tn = Math.Min(tn, 1.0f);
            tn = Math.Max(tn, 0.0f);
            Random r = new Random();
            int randomLuku = r.Next(1, 1000);
            if (randomLuku > tn*1000.0f)
            {
                return true;
            } else
            {
                return false;
            }
        }

        public bool LiikuOikealle()
        {
            Karttaruutu vr = Moottori.NykyinenKartta.Ruudut[this.Rivi, this.Sarake];
            Karttaruutu ur = Moottori.NykyinenKartta.Ruudut[this.Rivi, this.Sarake+1];
            if (!liiku(vr,ur))
                return false;
            ViimeSarake = Sarake;
            Sarake++;
            ViimeisinSuunta = Liikesuunnat.OIKEA;
            vr.Päivitä();
            ur.Päivitä();
            return true;
        }

        public bool LiikuVasemmalle()
        {
            Karttaruutu vr = Moottori.NykyinenKartta.Ruudut[this.Rivi, this.Sarake];
            Karttaruutu ur = Moottori.NykyinenKartta.Ruudut[this.Rivi, this.Sarake-1];
            if (!liiku(vr,ur))
                return false;
            ViimeSarake = Sarake;
            Sarake--;
            ViimeisinSuunta = Liikesuunnat.VASEN;
            vr.Päivitä();
            ur.Päivitä();
            return true;
        }

        public bool LiikuAlas()
        {
            Karttaruutu vr = Moottori.NykyinenKartta.Ruudut[this.Rivi, this.Sarake];
            Karttaruutu ur = Moottori.NykyinenKartta.Ruudut[this.Rivi+1, this.Sarake];
            if (!liiku(vr,ur))
                return false;
            ViimeRivi = Rivi;
            Rivi++;
            ViimeisinSuunta = Liikesuunnat.ALAS;
            vr.Päivitä();
            ur.Päivitä();
            return true;
        }

        public bool LiikuYlös()
        {
            Karttaruutu vr = Moottori.NykyinenKartta.Ruudut[this.Rivi, this.Sarake];
            Karttaruutu ur = Moottori.NykyinenKartta.Ruudut[this.Rivi-1, this.Sarake];
            if (!liiku(vr,ur))
                return false;
            ViimeRivi = Rivi;
            Rivi--;
            ViimeisinSuunta = Liikesuunnat.YLÖS;
            vr.Päivitä();
            ur.Päivitä();
            return true;
        }



        private bool liiku(Karttaruutu vr, Karttaruutu ur)
        {
            
            if (!ur.Käveltävä)
                return false;
            ur.Entiteetti = vr.Entiteetti;
            vr.Entiteetti = null;


            return true;
        }

        public void Piirrä()
        {
            Console.ForegroundColor = this.Väri;
            Console.Write(this.Merkki);
        }

        public enum Liikesuunnat
        {
            VASEN,
            OIKEA,
            YLÖS,
            ALAS
        }
    }
}
