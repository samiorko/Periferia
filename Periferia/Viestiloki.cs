using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Periferia
{
    public class Viestiloki
    {
        static int MaksimiRivit = 15;
        static int MaksimiLeveys = 62;

        Queue<Tuple<string, ConsoleColor>> Viestit = new Queue<Tuple<string, ConsoleColor>>();

        public void Lisää(string viesti, ConsoleColor väri = ConsoleColor.White)
        {
            Viestit.Enqueue(new Tuple<string, ConsoleColor>(viesti, väri));
            if (Viestit.Count > MaksimiRivit)
                Viestit.Dequeue();
        }

        public void Piirrä()
        {           
            Konsoli.PiirräReunatStringBuilder(Konsoli.ViestiLokiOffset_Vasen, Konsoli.ViestiLokiOffset_Ylä, MaksimiRivit, MaksimiLeveys);
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
