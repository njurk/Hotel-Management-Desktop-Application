﻿using MVVMFirma.Models.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.ViewModels
{
    public class WszystkieRodzajPracownikaViewModel : WszystkieViewModel<RodzajPracownika>
    {
        #region Constructor
        public WszystkieRodzajPracownikaViewModel()
            : base()
        {
            base.DisplayName = "Rodzaje pracowników";
        }

        public override void Delete()
        {
            if (SelectedItem != null)
            {
                hotelEntities.RodzajPracownika.Remove(hotelEntities.RodzajPracownika.FirstOrDefault(f => f.IdRodzajuPracownika == SelectedItem.IdRodzajuPracownika));
                hotelEntities.SaveChanges();
                List.Remove(SelectedItem);
            }
        }

        public override void Edit()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<RodzajPracownika>
            (
                hotelEntities.RodzajPracownika.ToList()
            );
        }
        #endregion
    }
}
