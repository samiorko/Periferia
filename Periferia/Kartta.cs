using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Periferia
{
    public class Kartta
    {
        public const int KARTTALEVEYS = 60;
        public const int KARTTAKORKEUS = 14;

        private const int KARTTAGENERAATTORI_PUUTIHEYS = 1;

        static int seuraavaVapaaId = 0;
        
        public int Id { get; set; }
        public List<IPiirrettävä> Entiteetit = new List<IPiirrettävä>(); // Kartalle piirrettävät objektit (tavarat, olennot ym.)
        public Karttaruutu[,] Ruudut = new Karttaruutu[KARTTAKORKEUS, KARTTALEVEYS];

        public SUUNTA? Ulosmenosuunta { get; private set; }
        public Karttaruutu Sisääntuloruutu { get; set; }
        public Karttaruutu Ulosmenoruutu { get; set; }
        public Kartta()
        {
            Id = seuraavaVapaaId;
            seuraavaVapaaId++;
        }

        public void Piirrä()
        {
            foreach(Karttaruutu r in Ruudut)
            {
                int KursoriYlä = r.Rivi + Konsoli.KarttaOffset_Ylä;
                int KursoriVasen = r.Sarake + Konsoli.KarttaOffset_Vasen;
                Console.SetCursorPosition(KursoriVasen, KursoriYlä);
                r.Piirrä();
            }

            PiirräEntiteetit();
        }

        public void PiirräEntiteetit()
        {
            PiirräEntiteetti(Moottori.Pelaaja);
            Ruudut[Moottori.Pelaaja.Rivi, Moottori.Pelaaja.Sarake].Entiteetti = Moottori.Pelaaja;

            if (Entiteetit != null)
                foreach(IPiirrettävä p in Entiteetit.Where(e => e is Vihollinen && (e as Vihollinen).Elossa))
                {
                    Ruudut[p.Rivi, p.Sarake].Entiteetti = p;
                    PiirräEntiteetti(p);
                }
        }

 

        private void PiirräEntiteetti(IPiirrettävä p)
        {
            Console.SetCursorPosition(Konsoli.KarttaOffset_Vasen + p.Sarake, Konsoli.KarttaOffset_Ylä + p.Rivi);
            p.Piirrä();
        }

        static public Tuple<int, int> RandomiVapaaRuutu(Kartta k)
        {
            int x, y;
            do
            {
                x = Vihollinen.Rnd.Next(0, KARTTALEVEYS - 1);
                y = Vihollinen.Rnd.Next(0, KARTTAKORKEUS - 1);
            } while (!k.Ruudut[y, x].Käveltävä);

            return new Tuple<int, int>(y,x);
        }
            
        static public Kartta LuoKartta()
        {
            Kartta k = new Kartta();
            Random rnd = new Random();

            SUUNTA? sisään = null, ulos = null;
            List<SUUNTA> sallitutUlosmenot = new List<SUUNTA>() { SUUNTA.YLÄ, SUUNTA.ALA, SUUNTA.VASEN, SUUNTA.OIKEA };
            if (k.Id != 0)
            {

                switch (Moottori.Kartat[k.Id-1].Ulosmenosuunta)
                {
                    case SUUNTA.YLÄ:
                        sisään = SUUNTA.ALA;
                        sallitutUlosmenot.Remove(SUUNTA.ALA);
                        break;
                    case SUUNTA.ALA:
                        sisään = SUUNTA.YLÄ;
                        sallitutUlosmenot.Remove(SUUNTA.YLÄ);
                        break;
                    case SUUNTA.VASEN:
                        sisään = SUUNTA.OIKEA;
                        sallitutUlosmenot.Remove(SUUNTA.OIKEA);
                        break;
                    case SUUNTA.OIKEA:
                        sisään = SUUNTA.VASEN;
                        sallitutUlosmenot.Remove(SUUNTA.VASEN);
                        break;
                }
            }
            int ulosInt = rnd.Next(0, sallitutUlosmenot.Count-1);
            ulos = sallitutUlosmenot[ulosInt];

            for (int y = 0; y < KARTTAKORKEUS; y++)
            {
                // Käydään rivit
                for (int x = 0; x < KARTTALEVEYS; x++)
                {
                    // Käydään sarakkeet

                    Karttaruutu r = new Karttaruutu();

                    r.Rivi = y;
                    r.Sarake = x;
                    
                    
                    if( (y == 0 || y == KARTTAKORKEUS-1) || (x == 0 || x == KARTTALEVEYS - 1))
                    {
                        r.Tyyppi = Karttaruutu.Ruututyypit.SEINÄ;
                        r.Väri = ConsoleColor.DarkGray;
                        r.Merkki = '#';
                    }
                    else
                    {
                        if(rnd.Next(0,100) <= KARTTAGENERAATTORI_PUUTIHEYS)
                        {
                            r.Tyyppi = Karttaruutu.Ruututyypit.PUU;
                            r.Merkki = '&';
                            r.Väri = ConsoleColor.DarkGreen;
                        }
                        else
                        {
                            r.Tyyppi = Karttaruutu.Ruututyypit.TYHJÄ;
                            r.Merkki = '░';
                            r.Väri = ConsoleColor.DarkGray;
                        }
                    }
                    k.Ruudut[y, x] = r;
                }
            } // Tähän loppuu ruutujen generointi

            // Päätellään, mille seinälle ulos- ja sisääntulot pitää piirtää
            Karttaruutu sisäänmenoruutu = new Karttaruutu() { Tyyppi = Karttaruutu.Ruututyypit.SISÄÄN, Merkki = '<', Väri = ConsoleColor.DarkMagenta };
            Karttaruutu ulosmenoruutu = new Karttaruutu() { Tyyppi = Karttaruutu.Ruututyypit.ULOS, Merkki = '>', Väri = ConsoleColor.DarkMagenta };
            int X = (ulos == SUUNTA.VASEN || ulos == SUUNTA.OIKEA)
                    ? (ulos == SUUNTA.VASEN) ? 0 : Kartta.KARTTALEVEYS - 1
                    : rnd.Next(1, KARTTALEVEYS - 1);
            int Y = (ulos == SUUNTA.YLÄ || ulos == SUUNTA.ALA)
                    ? (ulos == SUUNTA.YLÄ) ? 0 : Kartta.KARTTAKORKEUS - 1
                    : rnd.Next(1, KARTTAKORKEUS - 1);
            // Piirretään ulosmenoruutu kartan päälle
            ulosmenoruutu.Rivi = Y;
            ulosmenoruutu.Sarake = X;
            k.Ruudut[Y, X] = ulosmenoruutu;
            k.Ulosmenoruutu = ulosmenoruutu;
            // Piirretään sisäänmenoruutu kartan päälle
            if (sisään != null)
            {
                X = (sisään == SUUNTA.VASEN || sisään == SUUNTA.OIKEA)
                            ? (sisään == SUUNTA.VASEN) ? 0 : Kartta.KARTTALEVEYS - 1
                            : rnd.Next(1, KARTTALEVEYS - 1);
                Y = (sisään == SUUNTA.YLÄ || sisään == SUUNTA.ALA)
                        ? (sisään == SUUNTA.YLÄ) ? 0 : Kartta.KARTTAKORKEUS - 1
                        : rnd.Next(1, KARTTAKORKEUS - 1);
                sisäänmenoruutu.Rivi = Y;
                sisäänmenoruutu.Sarake = X;
                k.Ruudut[Y, X] = sisäänmenoruutu;
                k.Sisääntuloruutu = sisäänmenoruutu;
            }

            for (int i = 0; i < Vihollinen.Rnd.Next(2, 5); i++)
            {
                VihollisMalli malli = Moottori.VihollisMallit[Vihollinen.Rnd.Next(0, Moottori.VihollisMallit.Count - 1)];
                Vihollinen vihu = Vihollinen.Generoi(malli);
                vihu.HpMuuttunut += Konsoli.Hahmoruutu.VihollisenHPMuuttunut;
                Tuple<int, int> YX = RandomiVapaaRuutu(k);
                vihu.Rivi = YX.Item1;
                vihu.Sarake = YX.Item2;
                k.Entiteetit.Add(vihu);
            }

            k.Ulosmenosuunta = ulos;

            return k;
            
        }
        public enum SUUNTA
        {
            VASEN,
            OIKEA,
            YLÄ,
            ALA
        };

    }
}