using System;
using System.Collections.Generic;

namespace SlovenskiKino.Models
{
    public partial class AktualniFilmi
    {
        public int IdAktualenFilm { get; set; }
        public int IdPodjetja { get; set; }
        public string SlovenskiNaslov { get; set; }
        public string AngleskiNaslov { get; set; }
        public string Zanr { get; set; }
        public int? Dolzina { get; set; }
        public string NaSporeduOd { get; set; }

        public Podjetja IdPodjetjaNavigation { get; set; }
    }
}
