using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Periferia
{
    public class Hahmoruutu
    {
        public void Piirrä(int kursoriVasen, int kursoriYlä)
        {
            Console.SetCursorPosition(kursoriVasen, kursoriYlä); // Asetetaan kursorin aloituspaikka
            
            Console.Write("Pelaaja:   " + Moottori.Pelaaja.Nimi);

            Konsoli.UusiRivi(kursoriVasen);
            Console.Write("HP:        " + PiirräPalkki(Moottori.Pelaaja.HP));
            Konsoli.UusiRivi(kursoriVasen);
            Console.Write("Nesteytys: " + PiirräPalkki(Moottori.Pelaaja.Nesteytys));
            Console.ResetColor();
        }

        public string PiirräPalkki(int PropertynKoko)
        {
            string palkki = "[";
            Console.ForegroundColor = ConsoleColor.Magenta;

            for (int i = 0; i < 10; i++)
            {
                if (i < PropertynKoko / 10)
                {
                    palkki += "|";
                }
                else
                    palkki += " ";
            }
            Console.ResetColor();

            return palkki += "]";
        }

    }
}
