using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Periferia
{
    public class Pelaaja : Hahmo, IPiirrettävä
    {
        public event EventHandler NesteMuuttunut;
        public event EventHandler StatsitMuuttunut;

        private int _nesteytys;
        public int TappoPisteet { get; set; }
        public int Pisteet { get => (TappoPisteet + Moottori.Kartat.Count * 100 + Taso * 100 + HP * 10); }
        public int Nesteytys {
            get {
                return _nesteytys;
            }
            set
            {
                this._nesteytys = value;

                if (this.Nimi == "KariG")
                    _nesteytys = 100;

                if(Nesteytys <= 0)
                {
                    _nesteytys = 0;
                    HP = 0;
                }
                else if (Nesteytys > 100)
                {
                    _nesteytys = 100;
                }
                
                NesteMuuttunut?.Invoke(this, EventArgs.Empty);
            }
        }

        private int _kokemus;
        public int levutusRaja { get => (int)Math.Ceiling((decimal)Taso * 1.5m * 10.0m); }

        public int Kokemus {
            get
            {
                return _kokemus;
            }
            set {
                _kokemus = _kokemus + value;
                if(_kokemus > levutusRaja)
                {
                    Taso++;
                    MaksimiHP += 10;
                    HP = MaksimiHP;
                    Konsoli.Viestiloki.Lisää($"{this.Nimi} saavutti tason {this.Taso}!", ConsoleColor.DarkYellow);
                    _kokemus = _kokemus - levutusRaja;
                    Voima++;
                    Nopeus++;
                    Onnekkuus++;
                }
            }
        }

        public string TulostaVoima {
            get
            {
                int plusVoima = Reppu.Where(t => t.PlusVoima > 0).Sum(t => t.PlusVoima);
                return Voima + ((plusVoima > 0) ? $" (+{plusVoima})" : "");
            }
        }

        public string TulostaNopeus
        {
            get
            {
                int plusNopeus = Reppu.Where(t => t.PlusNopeus > 0).Sum(t => t.PlusNopeus);
                return Nopeus + ((plusNopeus > 0) ? $" (+{plusNopeus})" : "");
            }
        }

        public string TulostaOnnekkuus
        {
            get
            {
                int plusOnnekkuus = Reppu.Where(t => t.PlusOnnekkuus > 0).Sum(t => t.PlusOnnekkuus);
                return Nopeus + ((plusOnnekkuus > 0) ? $" (+{plusOnnekkuus})" : "");
            }
        }


        private int _taso;
        public override int Taso
        {
            get { return _taso; }
            set
            {
                _taso = value;
                StatsitMuuttunut?.Invoke(this, EventArgs.Empty);
            }
        }
        private int _voima;
        public override int Voima
        {
            get {
                return _voima + Reppu.Where(t => t.PlusVoima > 0).Sum(t => t.PlusVoima);
            }
            set
            {
                _voima = value;
                StatsitMuuttunut?.Invoke(this, EventArgs.Empty);
            }
        }
        private int _nopeus;
        public override int Nopeus
        {
            get {
                return _nopeus + Reppu.Where(t => t.PlusNopeus > 0).Sum(t => t.PlusNopeus);
            }
            set
            {
                _nopeus = value;
                StatsitMuuttunut?.Invoke(this, EventArgs.Empty);
            }
        }
        private int _onnekkuus;
        public override int Onnekkuus
        {
            get {
                return _onnekkuus + Reppu.Where(t => t.PlusOnnekkuus > 0).Sum(t => t.PlusOnnekkuus);
            }
            set
            {
                _onnekkuus = value;
                StatsitMuuttunut?.Invoke(this, EventArgs.Empty);
            }
        }

        public int MontakoTapettu { get; set; } = 0;

        public ObservableCollection<Tavara> Reppu { get; set ; }

        public override void Kuole()
        {
            // Poistetaan karttaruudulla oleva pelaaja
            Moottori.NykyinenKartta.Ruudut[this.Rivi, this.Sarake].Entiteetti = null;
            Moottori.NykyinenKartta.Ruudut[this.Rivi, this.Sarake].Päivitä();
            // Game over-ruutuun
            Moottori.Pelijatkuu = false;
            
        }
    }
}