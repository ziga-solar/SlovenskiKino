using System;
using System.Collections.Generic;

namespace SlovenskiKino.Models
{
    public partial class ArhivFilmov
    {
        public int IdFilma { get; set; }
        public string SlovenskiNaslov { get; set; }
        public string AngleskiNaslov { get; set; }
        public string Zanr { get; set; }
        public string Dolzina { get; set; }
        public string Datum { get; set; }
    }
}
