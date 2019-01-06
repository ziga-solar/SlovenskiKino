using System;
using System.Collections.Generic;

namespace SlovenskiKino.Models
{
    public partial class CenikVstopnic
    {
        public int IdCenik { get; set; }
        public int IdPodjetja { get; set; }
        public double? RednaCena { get; set; }
        public double? CenaSpopustom { get; set; }
        public double? StudentskaCena { get; set; }
        public double? Doplacilo3D { get; set; }
        public double? DoplaciloZa120 { get; set; }

        public Podjetja IdPodjetjaNavigation { get; set; }
    }
}
