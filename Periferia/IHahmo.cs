using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Periferia
{
    public interface IHahmo
    {
        int Voima { get; set; }
        int HP { get; set; }
        bool OnkoYstävä { get; set; }
        bool OnkoTekoäly { get; set; }
    }
}