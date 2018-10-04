using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Periferia
{
    static class Moottori
    {
        static public int VedenPisteet = 50;
        static public int TaikajuomanPisteet = 50;
        static public List<Kartta> Kartat = new List<Kartta>();
        static public float VaikeusKerroin { get => ((float)Vaikeustaso)/10.0f; }
        static public Vaikeustasot Vaikeustaso { get; set; } = Vaikeustasot.HELPPO;

        static public Kartta NykyinenKartta;
        static public List<VihollisMalli> VihollisMallit = new List<VihollisMalli>();
        static public Pelaaja Pelaaja = new Pelaaja()
        {
            Väri = ConsoleColor.Cyan,
            Merkki = '@',
            HP = 100,
            MaksimiHP = 100,
            Nesteytys = 100,
            Nimi = "Pekka",
            Taso = 1,
            Voima = 5,
            Nopeus = 2,
            Onnekkuus = 3,
            Reppu = new ObservableCollection<Tavara>(),
            Sarake = 2,
            Rivi = 2
        };
        static public bool Pelijatkuu { get; set; } = true;
        
        static public void Peli()
        {
            VihollisMallit.Add(new VihollisMalli("Karhu", 'K') {Voima=4, Nopeus=1, Onnekkuus=1, Näkökenttä = 10, HP=40, Hyökkäys="raapaisee"});
            VihollisMallit.Add(new VihollisMalli("Susi", 'S') {Voima=2, Nopeus=2, Onnekkuus=1, Näkökenttä = 25, HP = 30, Hyökkäys="puraisee"});
            VihollisMallit.Add(new VihollisMalli("Goblin", 'G', ConsoleColor.DarkGreen) {Voima=1, Nopeus=1, Näkökenttä = 50, HP = 15, Hyökkäys="lyö"});
            Konsoli.AlustaKonsoli();
            
            //Konsoli.piirräAloitusnäyttö();
            //Konsoli.HahmonLuonti();

            Moottori.Pelaaja.HpMuuttunut += Konsoli.Hahmoruutu.PelaajanHPMuuttunut;
            Moottori.Pelaaja.NesteMuuttunut += Konsoli.Hahmoruutu.PelaajanNesteytysMuuttunut;
            Moottori.Pelaaja.StatsitMuuttunut += Konsoli.Hahmoruutu.PelaajanStatsitMuuttuneet;

            Moottori.NykyinenKartta = Kartta.LuoKartta();

            Pelaaja.Reppu.Add(new Tavara("Retkisaha"));
            //Pelaaja.Reppu.Add(new Tavara("leka"));

            Moottori.Kartat.Add(Moottori.NykyinenKartta);

            Konsoli.PiirräHahmoRuutu();
            Konsoli.PiirräKartta(Moottori.NykyinenKartta);

            Konsoli.Viestiloki.Lisää("Peli alkaa!");

            while (Pelijatkuu)
            {
                //Konsoli.TyhjennäKonsoli();
                Konsoli.PiirräLoki();

                //Konsoli.PiirräReunat();


                pelaajanVuoro();

                if(Moottori.Pelaaja.Elossa) vihollistenVuoro();

            }
            if (Moottori.Pelaaja.Elossa)
            {
                Konsoli.PiirräPelastus();
            } else
            {
                Konsoli.PiirräGameOver();
            }
            
        }

        static void pelaajanVuoro()
        {
            ConsoleKeyInfo näppäin = Console.ReadKey(true);
            switch (näppäin.Key)
            {
                case ConsoleKey.RightArrow:
                    Moottori.Pelaaja.LiikuOikealle();
                    Pelaaja.Nesteytys--;
                    break;
                case ConsoleKey.LeftArrow:
                    Moottori.Pelaaja.LiikuVasemmalle();
                    Pelaaja.Nesteytys--;
                    break;
                case ConsoleKey.UpArrow:
                    Moottori.Pelaaja.LiikuYlös();
                    Pelaaja.Nesteytys--;
                    break;
                case ConsoleKey.DownArrow:
                    Moottori.Pelaaja.LiikuAlas();
                    Pelaaja.Nesteytys--;
                    break;

            }
        }

        static void vihollistenVuoro()
        {
            foreach (Vihollinen v in NykyinenKartta.Entiteetit.Where(v => v is Vihollinen))
            {
                if (!v.OnkoTekoäly || !v.Elossa)
                    continue; // älytön tai kuollut tyyppi, mennään seuraavaan
                if (v.EtäisyysPelaajasta > Math.Ceiling(v.Näkökenttä * Moottori.VaikeusKerroin))
                    continue; // Liian kaukana, ei nähdä pelaajaa

                v.Tekoäly();
            }

        }

        internal static void EdellinenKartta()
        {
            Kartta sk = Kartat[NykyinenKartta.Id-1];
            NykyinenKartta = sk;
            Pelaaja.Rivi = sk.Ulosmenoruutu.Rivi;
            Pelaaja.Sarake = sk.Ulosmenoruutu.Sarake;
            Konsoli.PiirräKartta(sk);
        }

        internal static void SeuraavaKartta()
        {
            Kartta sk = (NykyinenKartta.Id+1 == Kartat.Count) ? Kartta.LuoKartta() : Kartat[NykyinenKartta.Id+1] ;
            NykyinenKartta = sk;
            Pelaaja.Rivi = sk.Sisääntuloruutu.Rivi;
            Pelaaja.Sarake = sk.Sisääntuloruutu.Sarake;
            Konsoli.PiirräKartta(sk);
            if (!Kartat.Contains(sk))
                Kartat.Add(sk);
        }

        public enum Vaikeustasot
        {
            HELPPO = 10,
            VAIKEA = 15,
            VAIKEIN = 20
        }
    }
}
