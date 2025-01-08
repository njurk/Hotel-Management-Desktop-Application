﻿using System;

namespace MVVMFirma.Models.EntitiesForView
{
    public class PlatnoscForAllView
    {
        public int IdPlatnosci { get; set; }
        public string NrPlatnosci { get; set; }
        public int IdRezerwacji { get; set; }
        public string SposobPlatnosciNazwa { get; set; } // z klucza obcego
        public string StatusPlatnosciNazwa { get; set; } // z klucza obcego
        public DateTime DataPlatnosci { get; set; }
        public decimal Kwota { get; set; }
    }
}
