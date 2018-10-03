using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Periferia
{
    abstract public class Hahmo
    {
        public int Taso { get; set; }
        public int Voima { get; set; }
        public int Nopeus { get; set; }
        public int Onnekkuus { get; set; }

        public int HP { get; set; }
        public int MaksimiHP { get; set; }
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
