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

        private int _nesteytys;

        public int Nesteytys {
            get {
                return _nesteytys;
            }
            set
            {
                NesteMuuttunut?.Invoke(this, EventArgs.Empty);
                this._nesteytys = value;
                if(Nesteytys < 0)
                {
                    //Kuole();
                    Konsoli.Viestiloki.Lisää("NEste loppui, KUOLIT!", ConsoleColor.Red);
                }
            }
        }

        private int _kokemus;

        public int Kokemus {
            get
            {
                return _kokemus;
            }
            set {
                _kokemus = _kokemus + value;
                if(_kokemus > 10)
                {
                    Taso++;
                    _kokemus = _kokemus - 10;
                    Voima++;
                    Nopeus++;
                    Onnekkuus++;
                }
            }
        }


        public ObservableCollection<Tavara> Reppu { get; set ; }


    }
}