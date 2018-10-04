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

        public virtual int Taso { get; set; }
        public virtual int Voima { get; set; }
        public virtual int Nopeus { get; set; }
        public virtual int Onnekkuus { get; set; }

        public int EtäisyysPelaajasta
        {
            get
            {
                if (!this.Elossa)
                    return 9001;
                return (Math.Abs(Moottori.Pelaaja.Rivi - this.Rivi) + Math.Abs(Moottori.Pelaaja.Sarake - this.Sarake));
            }
        }

        private int _hp;

        public int HP
        {
            get { return _hp; }
            set
            {
                if (value <= 0)
                {
                    _hp = 0;
                    Kuole();
                }
                else
                {
                    _hp = value;
                }
                HpMuuttunut?.Invoke(this, EventArgs.Empty);
            }
        }
        public int MaksimiHP { get; set; }


        public bool Elossa { get => (HP > 0); }
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
        public string Hyökkäys { get; set; } = "vahingoittaa";

        public virtual void Kuole()
        {
            // Poistetaan karttaruudulla oleva hahmo
            Moottori.NykyinenKartta.Ruudut[this.Rivi, this.Sarake].Entiteetti = null;
            Moottori.NykyinenKartta.Ruudut[this.Rivi, this.Sarake].Päivitä();
            this.Sarake = 0;
            this.Rivi = 0;
            if (this is Vihollinen)
            {
                int saatuKokemus = (this as Vihollinen).KokemusPalkinto;
                Konsoli.Viestiloki.Lisää($"{Moottori.Pelaaja.Nimi} sai {saatuKokemus} XP. \t ( Seuraava LVL {Moottori.Pelaaja.Kokemus} / {Moottori.Pelaaja.levutusRaja} )");
                Moottori.Pelaaja.Kokemus += saatuKokemus;
                Moottori.Pelaaja.MontakoTapettu++;
                Moottori.Pelaaja.TappoPisteet += saatuKokemus * this.Taso;
            }
        }

        public void Hyökkää(Hahmo kohde)
        {
            // (1) Tarkistetaan, osuuko hyökkäys
            if (osuukoHyökkäysNopeus(kohde) || osuukoHyökkäysOnni(kohde))
            {                // (2) Kuinka paljon vahinkoa tehdään?
                int vahinko = laskeVahinko();
                // (3) Onko kriittinen vahinko?
                if (onkoKriittinenOsuma(kohde))
                {
                    vahinko *= 2;
                    Konsoli.Viestiloki.Lisää("Kriittinen osuma!", ConsoleColor.DarkYellow);
                }
                // Vähennetään kohteen hp vahingon mukaan
                kohde.HP -= vahinko;
                ConsoleColor tekstinVari = ConsoleColor.Gray;
                if (this is Vihollinen)
                {
                    tekstinVari = ConsoleColor.Red;
                }
                Konsoli.Viestiloki.Lisää($"{this.Nimi} {this.Hyökkäys} olentoa {kohde.Nimi}! {kohde.Nimi} HP -{vahinko} pistettä.", tekstinVari);
            }
            else
            {
                Konsoli.Viestiloki.Lisää($"{this.Nimi} olennon isku meni ohi {kohde.Nimi}-olennon!", ConsoleColor.DarkCyan);
            }
        }

        private bool onkoKriittinenOsuma(Hahmo kohde)
        {
            float suhdeLuku = ((float)this.Onnekkuus) / ((float)kohde.Onnekkuus);
            // määritetään tod.näk. sille, että hyökkäys osuu
            double tn = 0.0f;
            if (suhdeLuku < 2.0f)
            {
                tn = 0.0f;
            }
            else if (suhdeLuku > 4.0f)
            {
                tn = 0.2f;
            }
            else
            {
                tn = 0.1f * suhdeLuku - 0.2f;
            }
            Random r = new Random();
            int randomLuku = r.Next(1, 1000);
            if (randomLuku < tn * 1000.0f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private int laskeVahinko()
        {
            int painokerroin = 8;
            return this.Voima * painokerroin;
        }

        private bool osuukoHyökkäysOnni(Hahmo kohde)
        {
            float suhdeLuku = ((float)this.Onnekkuus) / ((float)kohde.Onnekkuus);
            // määritetään tod.näk. sille, että hyökkäys osuu
            double tn = 0.0f;
            if (suhdeLuku < 1.5f)
            {
                tn = 0.0f;
            }
            else
            {
                tn = 0.2f * suhdeLuku - 0.3f;
            }
            // varmistetaan, ettei tn pääse alle 0 tai yli 1
            tn = Math.Min(tn, 1.0f);
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
            float suhdeLuku = ((float)this.Nopeus) / ((float)kohde.Nopeus);
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
            if (randomLuku < tn * 1000.0f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool LiikuOikealle()
        {
            Karttaruutu vr = Moottori.NykyinenKartta.Ruudut[this.Rivi, this.Sarake];
            Karttaruutu ur;
            try
            {
                ur = Moottori.NykyinenKartta.Ruudut[this.Rivi, this.Sarake + 1];
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
            if (!liiku(vr, ur))
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
            Karttaruutu ur;
            try
            {
                ur = Moottori.NykyinenKartta.Ruudut[this.Rivi, this.Sarake -1];
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
            if (!liiku(vr, ur))
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
            Karttaruutu ur;
            try
            {
                ur = Moottori.NykyinenKartta.Ruudut[this.Rivi + 1, this.Sarake];
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
            if (!liiku(vr, ur))
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
            Karttaruutu ur;
            try
            {
                ur = Moottori.NykyinenKartta.Ruudut[this.Rivi - 1, this.Sarake];
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
            if (!liiku(vr, ur))
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

            if (this is Vihollinen ? !ur.TekoälyKäveltävä : !ur.Käveltävä)
            {
                // Pelaajan hyökkäys
                if (ur.Entiteetti is Vihollinen && (this is Pelaaja))
                {
                    this.Hyökkää((Hahmo)ur.Entiteetti);
                    return false;
                }
                // Pelaaja vaihtaa karttaa
                else if (ur.Tyyppi == Karttaruutu.Ruututyypit.ULOS && (this is Pelaaja))
                {
                    vr.Entiteetti = null;
                    Moottori.SeuraavaKartta();
                }
                else if (ur.Tyyppi == Karttaruutu.Ruututyypit.SISÄÄN && (this is Pelaaja))
                {
                    vr.Entiteetti = null;
                    Moottori.EdellinenKartta();
                }
                // Puun kaato
                if (this is Pelaaja && ur.Tyyppi is Karttaruutu.Ruututyypit.PUU)
                {
                    ur.Väri =ConsoleColor.DarkYellow;
                    ur.Tyyppi = Karttaruutu.Ruututyypit.PUU_HAJOAMASSA;
                    Konsoli.Viestiloki.Lisää("Kaadat puuta, sahaa vielä hetki");
                    Moottori.NykyinenKartta.Ruudut[ur.Rivi, ur.Sarake].Päivitä();
                    return false;
                }
                // Puu hajoamassa
                if(this is Pelaaja && ur.Tyyppi is Karttaruutu.Ruututyypit.PUU_HAJOAMASSA)
                {
                    ur.Tyyppi= Karttaruutu.Ruututyypit.TYHJÄ;
                    ur.Merkki = '░';
                    ur.Väri = ConsoleColor.DarkGray;
                    Moottori.NykyinenKartta.Ruudut[ur.Rivi, ur.Sarake].Päivitä();
                    
                    return false;
                }

                if (this is Pelaaja && ur.Tyyppi is Karttaruutu.Ruututyypit.PELASTUS)
                {
                    Konsoli.Viestiloki.Lisää("Voiti");
                    Moottori.Pelijatkuu = false;

                    return false;
                }

                return false;


            } // liikkumattomuus päättyy
            // Jos liikkuminen onnistuu, suoritetaan alla olevat rivit
            if (ur.Entiteetti is Tavara && this is Pelaaja)
            {
                switch (ur.Entiteetti.Nimi)
                {
                    case ("vesi"):
                        Moottori.Pelaaja.Nesteytys += Moottori.VedenPisteet;
                        Konsoli.Viestiloki.Lisää($"Löysit vettä +{Moottori.VedenPisteet}!", ConsoleColor.Blue);
                        break;
                    case ("taikajuoma"):
                        Moottori.Pelaaja.HP += Moottori.TaikajuomanPisteet;
                        Konsoli.Viestiloki.Lisää($"Löysit taikajuomaa +{Moottori.TaikajuomanPisteet}!", ConsoleColor.DarkRed);
                        break;
                }
                
                
            }
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
