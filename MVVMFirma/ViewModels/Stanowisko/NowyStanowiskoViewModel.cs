﻿using GalaSoft.MvvmLight.Messaging;
using MVVMFirma.Helper;
using MVVMFirma.Models.Entities;
using System.Linq;

namespace MVVMFirma.ViewModels
{
    public class NowyStanowiskoViewModel : JedenViewModel<Stanowisko>
    {
        #region DB
        HotelEntities db;
        #endregion

        #region Properties
        public string Nazwa
        {
            get
            {
                return item.Nazwa;
            }
            set
            {
                item.Nazwa = value;
                OnPropertyChanged(() => Nazwa);
            }
        }

        public decimal StawkaGodzinowa
        {
            get
            {
                return item.StawkaGodzinowa;
            }
            set
            {
                item.StawkaGodzinowa = value;
                OnPropertyChanged(() => StawkaGodzinowa);
            }
        }
        #endregion

        #region Helpers
        public override void Save()
        {
            if (item.IdStanowiska == 0) // dodawanie rekordu = brak ID = insert
            {
                db.Stanowisko.Add(item);
            }
            else // edycja rekordu = istnieje ID = update
            {
                var doEdycji = db.Stanowisko.FirstOrDefault(f => f.IdStanowiska == item.IdStanowiska);
                if (doEdycji != null)
                {
                    db.Entry(doEdycji).CurrentValues.SetValues(item);
                }
            }

            db.SaveChanges();
            // wysłanie prośby o odświeżenie listy po zapisie
            Messenger.Default.Send("StanowiskoRefresh");
        }
        #endregion

        #region Constructor
        public NowyStanowiskoViewModel()
            : base("Stanowisko")
        {
            db = new HotelEntities();
            item = new Stanowisko();
        }

        public NowyStanowiskoViewModel(int itemId)
            : base("Edycja stanowiska")
        {
            db = new HotelEntities();
            // inicjalizacja pól danymi z rekordu o ID przekazanym w argumencie (itemId)
            item = db.Stanowisko.FirstOrDefault(r => r.IdStanowiska == itemId);

            if (item != null)
            {
                Nazwa = item.Nazwa;
                StawkaGodzinowa = item.StawkaGodzinowa;
            }
        }
        #endregion

        #region Validation
        protected override string ValidateProperty(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(Nazwa):
                    return StringValidator.ContainsOnlyLettersWithSpaces(Nazwa) ? "wprowadź poprawną nazwę (nie używaj cyfr ani znaków specjalnych)" : string.Empty;

                case nameof(StawkaGodzinowa):
                    return StawkaGodzinowa <= 0 ? "wprowadź poprawną kwotę" : string.Empty;

                default:
                    return string.Empty;
            }
        }

        public string ValidateDuplicate()
        {
            var istniejacyRekord = db.Stanowisko.FirstOrDefault(s =>
                s.Nazwa == Nazwa &&
                s.StawkaGodzinowa == StawkaGodzinowa &&
                s.IdStanowiska != item.IdStanowiska);

            if (istniejacyRekord != null)
                return $"istnieje już identyczne stanowisko. Ma ID: {istniejacyRekord.IdStanowiska}. Możesz je edytować lub usunąć i dodać na nowo.";

            return string.Empty;
        }
        #endregion
    }
}
