﻿using GalaSoft.MvvmLight.Messaging;
using MVVMFirma.Helper;
using MVVMFirma.Models.BusinessLogic;
using MVVMFirma.Models.Entities;
using MVVMFirma.Models.EntitiesForView;
using MVVMFirma.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;

namespace MVVMFirma.ViewModels
{
    public class NowyPlatnoscViewModel : JedenViewModel<Platnosc>
    {
        #region DB
        HotelEntities db;
        #endregion

        #region Fields
        private decimal _doZaplaty;
        private BaseCommand _openRezerwacjeModalne;
        #endregion

        #region Properties
        public string NrPlatnosci
        {
            get
            {
                return item.NrPlatnosci;
            }
            set
            {
                item.NrPlatnosci = value;
                OnPropertyChanged(() => NrPlatnosci);
            }
        }

        private string _nrRezerwacji;
        public string NrRezerwacji
        {
            get
            {
                return _nrRezerwacji;
            }
            set
            {
                _nrRezerwacji = value;
                OnPropertyChanged(() => NrRezerwacji);
            }
        }

        public int IdRezerwacji
        {
            get
            {
                return item.IdRezerwacji;
            }
            set
            {
                if (item.IdRezerwacji != value)
                {
                    item.IdRezerwacji = value;
                    OnPropertyChanged(() => IdRezerwacji);

                    var rezerwacja = db.Rezerwacja.FirstOrDefault(r => r.IdRezerwacji == value);
                    if (rezerwacja != null)
                    {
                        NrRezerwacji = rezerwacja.NrRezerwacji;
                        DoZaplaty = rezerwacja.Kwota - sumaPlatnosci(rezerwacja.IdRezerwacji);
                        Kwota = DoZaplaty;

                        OnPropertyChanged(() => NrRezerwacji);
                        OnPropertyChanged(() => Kwota);
                        OnPropertyChanged(() => DoZaplaty);
                    }
                }
            }
        }

        public int IdSposobuPlatnosci
        {
            get
            {
                return item.IdSposobuPlatnosci;
            }
            set
            {
                item.IdSposobuPlatnosci = value;
                OnPropertyChanged(() => IdSposobuPlatnosci);
            }
        }

        public int IdStatusuPlatnosci
        {
            get
            {
                return item.IdStatusuPlatnosci;
            }
            set
            {
                item.IdStatusuPlatnosci = value;
                OnPropertyChanged(() => IdStatusuPlatnosci);
            }
        }

        public DateTime DataPlatnosci
        {
            get
            {
                return item.DataPlatnosci;
            }
            set
            {
                item.DataPlatnosci = value;
                OnPropertyChanged(() => DataPlatnosci);
            }
        }

        public decimal Kwota
        {
            get
            {
                return item.Kwota;
            }
            set
            {
                item.Kwota = value;
                OnPropertyChanged(() => Kwota);
            }
        }

        public decimal DoZaplaty
        {
            get { return _doZaplaty; }
            set
            {
                if (_doZaplaty != value)
                {
                    _doZaplaty = value;
                    OnPropertyChanged(() => DoZaplaty);
                }
            }
        }

        // metoda obliczająca kwote pozostałą do zapłacenia dla danej rezerwacji
        private decimal sumaPlatnosci(int idRezerwacji)
        {
            return db.Platnosc
                     .Where(p => p.IdRezerwacji == idRezerwacji)
                     .Sum(p => (decimal?)p.Kwota) ?? 0;
        }
        #endregion

        #region Items
        public IEnumerable<KeyAndValue> RezerwacjaItems
        {
            get
            {
                return new RezerwacjaB(db).GetRezerwacjaKeyAndValueItems();
            }
        }

        public IEnumerable<KeyAndValue> SposobPlatnosciItems
        {
            get
            {
                return new SposobPlatnosciB(db).GetSposobPlatnosciKeyAndValueItems();
            }
        }

        public IEnumerable<KeyAndValue> StatusPlatnosciItems
        {
            get
            {
                return new StatusPlatnosciB(db).GetStatusPlatnosciKeyAndValueItems();
            }
        }
        #endregion

        #region Methods
        public BaseCommand OpenRezerwacjeModalneCommand
        {
            get
            {
                if (_openRezerwacjeModalne == null)
                {
                    _openRezerwacjeModalne = new BaseCommand(OpenRezerwacjeModalne);
                }
                return _openRezerwacjeModalne;
            }
        }
    
        private void OpenRezerwacjeModalne()
        {
            var rezerwacjeModalne = new RezerwacjeModalneView();
            rezerwacjeModalne.ShowDialog();
        }

        private string GenerujNumerPlatnosci()
        {
            // pobranie ostatniej platnosci z BD do ustalenia następnego numeru
            var ostatniaPlatnosc = db.Platnosc
                                     .OrderByDescending(f => f.IdPlatnosci)
                                     .Select(f => f.NrPlatnosci)
                                     .FirstOrDefault();

            // deklaracja zmiennej która otrzyma i na końcu ustawi odpowiedni numer aktualnie tworzonej płatności
            string numerPlatnosci;

            // jeśli istnieje jakakolwiek płatność w bazie
            if (ostatniaPlatnosc != null)
            {
                string platnoscMiesiac = ostatniaPlatnosc.Substring(5, 2); // yyyy-MM
                string obecnyMiesiac = DateTime.Now.ToString("MM");

                if (platnoscMiesiac == obecnyMiesiac)
                {
                    // ustalenie pozycji "P" w stringu aby wyodrębnić numer do inkrementacji
                    int pozycjaP = ostatniaPlatnosc.IndexOf('P');
                    if (pozycjaP != -1 && pozycjaP + 1 < ostatniaPlatnosc.Length)
                    {
                        string numer = ostatniaPlatnosc.Substring(pozycjaP + 1);

                        // próba konwersji stringu z numerem na int aby zwiększyć o 1
                        if (int.TryParse(numer, out int numerInt))
                        {
                            numerPlatnosci = (numerInt + 1).ToString();
                        }
                        else // jeśli nie udało się zamienić na int
                        {
                            numerPlatnosci = "1";
                        }
                    }
                    else // jeśli P nie znaleziona lub nie ma za nią żadnych cyfr
                    {
                        numerPlatnosci = "1";
                    }
                }
                else // jeśli jest nowy miesiąc
                {
                    numerPlatnosci = "1";
                }
            }
            else // jeśli w bazie nie ma jeszcze ani jednej płatności
            {
                numerPlatnosci = "1";
            }
            return $"{DateTime.Now:yyyy-MM}-P{numerPlatnosci}";
        }
        #endregion

        #region Helpers
        public override void Save()
        {
            if (item.IdPlatnosci == 0) // Dodawanie rekordu = brak ID = insert
            {
                db.Platnosc.Add(item);
            }
            else // Edycja rekordu = istnieje ID = update
            {
                var doEdycji = db.Platnosc.FirstOrDefault(f => f.IdPlatnosci == item.IdPlatnosci);
                if (doEdycji != null)
                {
                    db.Entry(doEdycji).CurrentValues.SetValues(item);
                }
            }

            db.SaveChanges();
            // wysłanie prośby o odświeżenie listy po zapisie
            Messenger.Default.Send("PlatnoscRefresh");
        }
        #endregion

        #region Constructor
        public NowyPlatnoscViewModel()
            : base("Płatność")
        {
            db = new HotelEntities();
            item = new Platnosc();

            DataPlatnosci = DateTime.Now;
            NrPlatnosci = GenerujNumerPlatnosci();

            Messenger.Default.Register<int>(this, idRezerwacji => IdRezerwacji = idRezerwacji);
        }

        public NowyPlatnoscViewModel(int itemId)
            : base("Edycja płatności")
        {
            db = new HotelEntities();
            // inicjalizacja pól danymi z rekordu o ID przekazanym w argumencie (itemId)
            item = db.Platnosc.FirstOrDefault(p => p.IdPlatnosci == itemId);

            if (item != null)
            {
                NrPlatnosci = item.NrPlatnosci;
                IdRezerwacji = item.IdRezerwacji;
                IdSposobuPlatnosci = item.IdSposobuPlatnosci;
                IdStatusuPlatnosci = item.IdStatusuPlatnosci;
                DataPlatnosci = item.DataPlatnosci;
                Kwota = item.Kwota;
            }
            Messenger.Default.Register<int>(this, idRezerwacji => IdRezerwacji = idRezerwacji);
        }
        #endregion

        #region Validation
        protected override string ValidateProperty(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(IdRezerwacji):
                    return IdRezerwacji <= 0 ? "wybierz rezerwację" : string.Empty;

                case nameof(IdSposobuPlatnosci):
                    return IdSposobuPlatnosci <= 0 ? "wybierz sposób płatności" : string.Empty;

                case nameof(IdStatusuPlatnosci):
                    return IdStatusuPlatnosci <= 0 ? "wybierz status płatności" : string.Empty;

                case nameof(DataPlatnosci):
                    return DataPlatnosci == null || DataPlatnosci > DateTime.Now ? "wybierz poprawną datę płatności" : string.Empty;

                case nameof(Kwota):
                    return Kwota <= 0 ? "wprowadź poprawną kwotę" : string.Empty;

                default:
                    return string.Empty;
            }
        }
        #endregion
    }
}
