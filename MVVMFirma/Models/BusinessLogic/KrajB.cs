﻿using MVVMFirma.Models.Entities;
using MVVMFirma.Models.EntitiesForView;
using System.Linq;

namespace MVVMFirma.Models.BusinessLogic
{
    public class KrajB : DatabaseClass
    {
        #region Constructor
        public KrajB(HotelEntities db)
            : base(db) { }
        #endregion

        #region Funkcje biznesowe
        public IQueryable<KeyAndValue> GetKrajKeyAndValueItems()
        {
            return
                (
                    from kraj in db.Kraj
                    select new KeyAndValue
                    {
                        Key = kraj.IdKraju,
                        Value = kraj.Nazwa
                    }
                ).ToList().AsQueryable();
        }
        #endregion
    }
}
