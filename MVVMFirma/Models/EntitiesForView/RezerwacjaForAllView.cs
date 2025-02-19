﻿using System;

namespace MVVMFirma.Models.EntitiesForView
{
    public class RezerwacjaForAllView
    {
        public int IdRezerwacji { get; set; }
        public string NrRezerwacji { get; set; }
        public string KlientImie { get; set; } // z klucza obcego
        public string KlientNazwisko { get; set; } // z klucza obcego
        public string NrPokoju { get; set; } // z klucza obcego
        public string LiczbaDoroslych { get; set; }
        public string LiczbaDzieci { get; set; }
        public bool CzyZwierzeta { get; set; }
        public DateTime DataRezerwacji { get; set; }
        public DateTime DataZameldowania { get; set; }
        public DateTime DataWymeldowania { get; set; }
        public decimal Kwota { get; set; }
        public string Uwagi { get; set; }
        public string Znizka { get; set; } // z klucza obcego
        public bool CzyZaplacona { get; set; } // obliczane dynamicznie

    }
}
