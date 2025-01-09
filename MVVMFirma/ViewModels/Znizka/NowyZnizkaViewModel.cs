﻿using GalaSoft.MvvmLight.Messaging;
using MVVMFirma.Models.Entities;
using System.Linq;

namespace MVVMFirma.ViewModels
{
    public class NowyZnizkaViewModel : JedenViewModel<Znizka>
    {
        #region DB
        HotelEntities db;
        #endregion

        #region Constructor
        public NowyZnizkaViewModel()
            : base("Zniżka")
        {
            db = new HotelEntities();
            item = new Znizka();
        }

        public NowyZnizkaViewModel(int itemId)
            : base("Edycja zniżki")
        {
            db = new HotelEntities();
            item = db.Znizka.FirstOrDefault(v => v.IdZnizki == itemId);

            if (item != null)
            {
                Nazwa = item.Nazwa;
                Wartosc = item.Wartosc;
            }
        }

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

        public string Wartosc
        {
            get
            {
                return item.Wartosc;
            }
            set
            {
                item.Wartosc = value;
                OnPropertyChanged(() => Wartosc);
            }
        }
        #endregion

        #region Helpers
        public override void Save()
        {
            if (item.IdZnizki == 0) // brak ID = insert
            {
                db.Znizka.Add(item);
            }
            else // istnieje ID = update
            {
                var doEdycji = db.Znizka.FirstOrDefault(f => f.IdZnizki == item.IdZnizki);
                if (doEdycji != null)
                {
                    db.Entry(doEdycji).CurrentValues.SetValues(item);
                }
            }

            db.SaveChanges();
            // automatyczne odświeżenie listy po edycji rekordu
            Messenger.Default.Send("ZnizkaRefresh");
        }
        #endregion
    }
}