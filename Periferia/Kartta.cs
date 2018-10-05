﻿using System;
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
        public static int karttageneraattori_vesilähteet = 2;
        public static int karttageneraattori_taikajuomat = 1;
        public static int minPelastusKartanNumero = 5;
        public static float pelastusTodennäköisyysProsentti = 10f;
        private const int KARTTAGENERAATTORI_PUUTIHEYS = 6;

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
            foreach (Karttaruutu r in Ruudut)
            {
                int KursoriYlä = r.Rivi + Konsoli.KarttaOffset_Ylä;
                int KursoriVasen = r.Sarake + Konsoli.KarttaOffset_Vasen;
                Console.SetCursorPosition(KursoriVasen, KursoriYlä);
                r.Piirrä();
            }

            PiirräEntiteetit();
            Konsoli.Hahmoruutu.PiirräEntiteettienTiedot(Konsoli.HahmoRuutuOffset_Vasen, Konsoli.HahmoRuutuOffset_Ylä);
            
            foreach(Tavara t in this.Entiteetit.Where(t => t is Tavara && (t as Tavara).Poimittava))
            {
                Konsoli.Viestiloki.Lisää($"Näet kartalla tavaran {t.Nimi} ({t.Merkki})");
            }
        }

        public void PiirräEntiteetit()
        {
            PiirräEntiteetti(Moottori.Pelaaja);
            Ruudut[Moottori.Pelaaja.Rivi, Moottori.Pelaaja.Sarake].Entiteetti = Moottori.Pelaaja;

            if (Entiteetit != null)
                foreach (IPiirrettävä p in Entiteetit.Where(e => (e is Vihollinen && (e as Vihollinen).Elossa) || e is Tavara))
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
            } while (!k.Ruudut[y, x].TekoälyKäveltävä);

            return new Tuple<int, int>(y, x);
        }

        static public Kartta LuoKartta()
        {
            Kartta k = new Kartta();
            Random rnd = new Random();


            SUUNTA? sisään = null, ulos = null;
            List<SUUNTA> sallitutUlosmenot = new List<SUUNTA>() { SUUNTA.YLÄ, SUUNTA.ALA, SUUNTA.VASEN, SUUNTA.OIKEA };
            if (k.Id != 0)
            {

                switch (Moottori.Kartat[k.Id - 1].Ulosmenosuunta)
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
            int ulosInt = rnd.Next(0, sallitutUlosmenot.Count - 1);
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


                    if ((y == 0 || y == KARTTAKORKEUS - 1) || (x == 0 || x == KARTTALEVEYS - 1))
                    {
                        r.Tyyppi = Karttaruutu.Ruututyypit.SEINÄ;
                        r.Väri = ConsoleColor.DarkGray;
                        r.Merkki = '#';
                    }
                    else
                    {
                        if (rnd.Next(0, 100) <= KARTTAGENERAATTORI_PUUTIHEYS)
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
            int vihujenMaara = Math.Min(6, (int)Math.Ceiling((decimal)Moottori.VaikeusKerroin * Vihollinen.Rnd.Next(2, 5)));
            for (int i = 0; i < vihujenMaara; i++)
            {
                VihollisMalli malli = Moottori.VihollisMallit[Vihollinen.Rnd.Next(0, Moottori.VihollisMallit.Count)];
                Vihollinen vihu = Vihollinen.Generoi(malli);
                vihu.HpMuuttunut += Konsoli.Hahmoruutu.VihollisenHPMuuttunut;
                Tuple<int, int> YX = RandomiVapaaRuutu(k);
                vihu.Rivi = YX.Item1;
                vihu.Sarake = YX.Item2;
                k.Entiteetit.Add(vihu);
            }

            k.Ulosmenosuunta = ulos;
            if(Moottori.Pelaaja.Nimi.ToLower() == "maija")
            {
                // Spawnataan mahtikirves
                if (k.Id == 0)
                {
                    Tuple<int, int> YX = RandomiVapaaRuutu(k);
                    Tavara kirves = new Tavara("Mahtikirves") { Merkki = '$', Väri = ConsoleColor.DarkMagenta, Rivi = YX.Item1, Sarake = YX.Item2, Poimittava = true, PlusVoima = 3, PlusNopeus = 2 };
                    k.Entiteetit.Add(kirves);
                    k.Ruudut[YX.Item1, YX.Item2].Entiteetti = kirves;
                }
                // Spawnataan jäniksenkäpälä
                if (k.Id == 1)
                {
                    Tuple<int, int> YX = RandomiVapaaRuutu(k);
                    Tavara jk = new Tavara("Jäniksenkäpälä") { Merkki = '*', Väri = ConsoleColor.DarkYellow, Rivi = YX.Item1, Sarake = YX.Item2, Poimittava = true,  PlusOnnekkuus = 5 };
                    k.Entiteetit.Add(jk);
                    k.Ruudut[YX.Item1, YX.Item2].Entiteetti = jk;
                }
                // Spawnataan jäniksenkäpälä
                if (k.Id == 2)
                {
                    Tuple<int, int> YX = RandomiVapaaRuutu(k);
                    Tavara dt = new Tavara("Demotavara") { Merkki = 'D', Väri = ConsoleColor.DarkMagenta, Rivi = YX.Item1, Sarake = YX.Item2, Poimittava = true, PlusOnnekkuus = 5 };
                    k.Entiteetit.Add(dt);
                    k.Ruudut[YX.Item1, YX.Item2].Entiteetti = dt;
                }

            }

            //Varataan karttaruudut vesilähteille
            for (int i = 0; i < karttageneraattori_vesilähteet; i++)
            {
                Tuple<int, int> YX = RandomiVapaaRuutu(k);
                Tavara vesi = new Tavara("vesi") { Merkki = 'V', Väri = ConsoleColor.Blue, Rivi = YX.Item1, Sarake = YX.Item2 };
                k.Entiteetit.Add(vesi);
                k.Ruudut[YX.Item1, YX.Item2].Entiteetti = vesi;
            }

            //Varataan karttaruudut taikajuomalle
            if (k.Id % 5 == 0 && k.Id != 0)
            {
                for (int i = 0; i < karttageneraattori_taikajuomat; i++)
                {
                    Tuple<int, int> YX = RandomiVapaaRuutu(k);
                    Tavara taikajuoma = new Tavara("taikajuoma") { Merkki = '\u2665', Väri = ConsoleColor.DarkRed, Rivi = YX.Item1, Sarake = YX.Item2 };
                    k.Entiteetit.Add(taikajuoma);
                    k.Ruudut[YX.Item1, YX.Item2].Entiteetti = taikajuoma;
                    Konsoli.Viestiloki.Lisää("\u2665 \u2665 \u2665 HP-boosti näköpiirissä! \u2665 \u2665 \u2665", ConsoleColor.DarkRed);
                }
            }

            // Määritetään pelastuksen sijainti
            if (k.Id >= minPelastusKartanNumero)
            {
                Random r = new Random();
                int randomLuku = r.Next(1, 100);
                if (randomLuku < pelastusTodennäköisyysProsentti || Moottori.Pelaaja.Nimi.ToLower() == "meo" && k.Id == 4)
                {
                    Tuple<int, int> YX = RandomiVapaaRuutu(k);
                    k.Ruudut[YX.Item1, YX.Item2].Tyyppi = Karttaruutu.Ruututyypit.PELASTUS;
                    k.Ruudut[YX.Item1, YX.Item2].Väri = ConsoleColor.DarkMagenta;
                    k.Ruudut[YX.Item1, YX.Item2].Merkki = '\u25B2';
                    Konsoli.Viestiloki.Lisää("\u25B2 \u25B2 \u25B2 PELASTUS NÄKÖPIIRISSÄ! PELASTUS NÄKÖPIIRISSÄ! \u25B2 \u25B2 \u25B2", ConsoleColor.DarkMagenta);
                    Konsoli.Viestiloki.Lisää("\u25B2 \u25B2 \u25B2 PELASTUS NÄKÖPIIRISSÄ! PELASTUS NÄKÖPIIRISSÄ! \u25B2 \u25B2 \u25B2", ConsoleColor.DarkMagenta);
                }
            }

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