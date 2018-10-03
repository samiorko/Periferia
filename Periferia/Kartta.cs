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

        private const int KARTTAGENERAATTORI_PUUTIHEYS = 6;

        static int seuraavaVapaaId = 0;
        
        public int Id { get; set; }
        public List<IPiirrettävä> Entiteetit = new List<IPiirrettävä>(); // Kartalle piirrettävät objektit (tavarat, olennot ym.)
        public Karttaruutu[,] Ruudut = new Karttaruutu[KARTTAKORKEUS, KARTTALEVEYS];

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
                foreach(IPiirrettävä p in Entiteetit)
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
            
        static public Kartta LuoKartta()
        {
            Kartta k = new Kartta();
            Random rnd = new Random();

            k.Entiteetit.Add(new Vihollinen()
            {
                Nimi = "Karhu",
                HP = 200,
                Voima = 100,
                Merkki = 'K',
                Väri = ConsoleColor.DarkRed,
                Rivi = 1,
                Sarake = 1
            });

            k.Entiteetit.Add(new Vihollinen()
            {
                Nimi = "Karhu",
                HP = 2000,
                Voima = 100,
                Merkki = 'K',
                Väri = ConsoleColor.DarkRed,
                Rivi = 10,
                Sarake = 10
            });

            k.Entiteetit.Add(new Vihollinen()
            {
                Nimi = "Karhu",
                HP = 2000,
                Voima = 100,
                Merkki = 'K',
                Väri = ConsoleColor.DarkRed,
                Rivi = 10,
                Sarake = 20
            });
            


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

            }

            return k;


            //using (StreamReader sr = new StreamReader(@"DummyKartta.txt"))
            //{
            //    string row;
            //    int rivi = 0;
            //    while ((row = sr.ReadLine()) != null)
            //    {
            //        int sarake = 0;
            //        foreach (char c in row)
            //        {
            //            Karttaruutu r = new Karttaruutu
            //            {
            //                Sarake = sarake,
            //                Rivi = rivi
            //            };

            //            switch (c)
            //            {
            //                case '#':
            //                    r.Tyyppi = Karttaruutu.Ruututyypit.SEINÄ;
            //                    r.Väri = ConsoleColor.DarkGray;
            //                    r.Merkki = '#';
            //                    break;
            //                case '>':
            //                    r.Tyyppi = Karttaruutu.Ruututyypit.ULOS;
            //                    r.Merkki = '>';
            //                    break;
            //                case '&':
            //                    r.Tyyppi = Karttaruutu.Ruututyypit.PUU;
            //                    r.Väri = ConsoleColor.DarkGreen;
            //                    r.Merkki = '&';
            //                    break;
            //                case ' ':
            //                    r.Tyyppi = Karttaruutu.Ruututyypit.TYHJÄ;
            //                    r.Merkki = '░';
            //                    r.Väri = ConsoleColor.DarkGray;
            //                    break;
            //            }

            //            k.Ruudut[rivi, sarake] = r;
            //            sarake++;
            //        }

            //        rivi++;
            //    }

            //}

        }

        static public void PiirräKartalle(Kartta k, IPiirrettävä p)
        {

        }

    }
}