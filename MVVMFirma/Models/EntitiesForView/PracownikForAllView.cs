﻿using System;

namespace MVVMFirma.Models.EntitiesForView
{
    public class PracownikForAllView
    {
        public int IdPracownika { get; set; }
        public string StanowiskoNazwa { get; set; } // z klucza obcego
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Ulica { get; set; }
        public string NrDomu { get; set; }
        public string NrLokalu { get; set; }
        public string KodPocztowy { get; set; }
        public string Miasto { get; set; }
        public string Kraj { get; set; } //  z klucza obcego
        public DateTime DataUrodzenia { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
    }
}
