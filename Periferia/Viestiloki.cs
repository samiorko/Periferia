using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Periferia
{
    public class Viestiloki
    {
        static int MaksimiRivit = 5;
        static int MaksimiLeveys = 61;
        Queue<Tuple<string, ConsoleColor>> Viestit = new Queue<Tuple<string, ConsoleColor>>();

        public void Lisää(string viesti, ConsoleColor väri = ConsoleColor.White)
        {
            Viestit.Enqueue(new Tuple<string, ConsoleColor>(viesti, väri));
            if (Viestit.Count > MaksimiRivit)
                Viestit.Dequeue();
        }

        public void Piirrä()
        {
            //Essin
            Konsoli.PiirräReunatStringWriter(Konsoli.ViestiLokiOffset_Vasen, Konsoli.ViestiLokiOffset_Ylä, 5, MaksimiLeveys);
            //
            Console.SetCursorPosition(Konsoli.ViestiLokiOffset_Vasen, Konsoli.ViestiLokiOffset_Ylä);
            foreach(Tuple<string, ConsoleColor> v in Viestit)
            {

                Console.ForegroundColor = v.Item2;
                Console.Write(v.Item1);
                Konsoli.UusiRivi(Konsoli.ViestiLokiOffset_Vasen);

            }
        }
        

    }
}
