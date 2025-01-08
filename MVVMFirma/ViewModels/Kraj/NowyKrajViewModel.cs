﻿using GalaSoft.MvvmLight.Messaging;
using MVVMFirma.Models.Entities;
using System.Linq;

namespace MVVMFirma.ViewModels
{
    public class NowyKrajViewModel : JedenViewModel<Kraj>
    {
        #region DB
        HotelEntities db;
        #endregion

        #region Constructor
        public NowyKrajViewModel()
            : base("Kraj")
        {
            db = new HotelEntities();
            item = new Kraj();
        }

        public NowyKrajViewModel(int itemId)
            : base("Edycja kraju")
        {
            db = new HotelEntities();
            item = db.Kraj.FirstOrDefault(k => k.IdKraju == itemId);

            if (item != null)
            {
                Nazwa = item.Nazwa;
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
        #endregion

        #region Helpers
        public override void Save()
        {
            if (item.IdKraju == 0) // brak ID = insert
            {
                db.Kraj.Add(item);
            }
            else // istnieje ID = update
            {
                var doEdycji = db.Kraj.FirstOrDefault(f => f.IdKraju == item.IdKraju);
                if (doEdycji != null)
                {
                    db.Entry(doEdycji).CurrentValues.SetValues(item);
                }
            }

            db.SaveChanges();
            // automatyczne odświeżenie listy po edycji rekordu
            Messenger.Default.Send("KrajRefresh");
        }
        #endregion
    }
}
