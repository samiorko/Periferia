using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Periferia
{
    public class Kartta
    {
        public const int KARTTALEVEYS = 20;
        public const int KARTTAKORKEUS = 10;

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
                r.Piirrä(this);
            }

            PiirräEntiteetit();
        }

        public void PiirräEntiteetit()
        {
            PiirräEntiteetti(Moottori.Pelaaja);

            if(Entiteetit != null)
                foreach(IPiirrettävä p in Entiteetit)
                {
                    Ruudut[p.Rivi, p.Sarake].Entiteetti = p;
                    PiirräEntiteetti(p);
                }
        }

 

        private void PiirräEntiteetti(IPiirrettävä p)
        {
            Console.SetCursorPosition(Konsoli.KarttaOffset_Vasen + p.Sarake, Konsoli.KarttaOffset_Ylä + p.Rivi);
            Console.ForegroundColor = p.Väri;
            Console.Write(p.Merkki);
        }
            
        static public Kartta LuoKartta()
        {
            Kartta k = new Kartta();

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
            

            using (StreamReader sr = new StreamReader(@"DummyKartta.txt"))
            {
                string row;
                int rivi = 0;
                while ((row = sr.ReadLine()) != null)
                {
                    int sarake = 0;
                    foreach (char c in row)
                    {
                        Karttaruutu r = new Karttaruutu
                        {
                            Sarake = sarake,
                            Rivi = rivi
                        };

                        switch (c)
                        {
                            case '#':
                                r.Tyyppi = Karttaruutu.Ruututyypit.SEINÄ;
                                r.Väri = ConsoleColor.DarkGray;
                                r.Merkki = '#';
                                break;
                            case '>':
                                r.Tyyppi = Karttaruutu.Ruututyypit.ULOS;
                                r.Merkki = '>';
                                break;
                            case '&':
                                r.Tyyppi = Karttaruutu.Ruututyypit.PUU;
                                r.Väri = ConsoleColor.DarkGreen;
                                r.Merkki = '&';
                                break;
                            case ' ':
                                r.Tyyppi = Karttaruutu.Ruututyypit.TYHJÄ;
                                r.Merkki = '.';
                                r.Väri = ConsoleColor.DarkGray;
                                break;
                        }

                        k.Ruudut[rivi, sarake] = r;
                        sarake++;
                    }

                    rivi++;
                }
                
            }

            return k;
        }

        static public void PiirräKartalle(Kartta k, IPiirrettävä p)
        {

        }

    }
}