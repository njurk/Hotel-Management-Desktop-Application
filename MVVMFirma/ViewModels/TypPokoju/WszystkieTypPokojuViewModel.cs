﻿using GalaSoft.MvvmLight.Messaging;
using MVVMFirma.Models.Entities;
using MVVMFirma.Models.EntitiesForView;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace MVVMFirma.ViewModels
{
    public class WszystkieTypPokojuViewModel : WszystkieViewModel<TypPokojuForAllView>
    {
        #region Constructor
        public WszystkieTypPokojuViewModel()
            : base("Typy pokojów")
        {
            // odbieranie wiadomości odświeżenia listy
            Messenger.Default.Register<string>(this, OnMessageReceived);
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<TypPokojuForAllView>
            (
                from typpokoju in hotelEntities.TypPokoju
                select new TypPokojuForAllView
                {
                    IdTypuPokoju = typpokoju.IdTypuPokoju,
                    Nazwa = typpokoju.Nazwa,
                    MaxLiczbaOsob = typpokoju.MaxLiczbaOsob
                }
            );
        }
        public override void Delete()
        {
            MessageBoxResult delete = MessageBox.Show("Czy na pewno chcesz usunąć wybrany typ pokoju:\n" + SelectedItem.Nazwa, "Usuwanie", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (SelectedItem != null && delete == MessageBoxResult.Yes)
            {
                hotelEntities.TypPokoju.Remove(hotelEntities.TypPokoju.FirstOrDefault(f => f.IdTypuPokoju == SelectedItem.IdTypuPokoju));
                hotelEntities.SaveChanges();
                Load();
            }
        }

        // w celu edycji wybranego rekordu wysyłana jest wiadomość zawierająca jego ID
        // odbiera i obsługuje ją metoda open() w klasie MainWindowViewModel
        public override void Edit()
        {
            if (SelectedItem != null)
            {
                Messenger.Default.Send(DisplayName + "Edit-" + SelectedItem.IdTypuPokoju);
            }
        }

        // OnMessageReceived obsługuje wiadomość dotyczącą odświeżenia listy w widoku Wszystkie..View, wysłaną przy zapisie edytowanego rekordu 
        private void OnMessageReceived(string message)
        {
            if (message == "TypPokojuRefresh")
            {
                Load();
            }
        }
        #endregion
    }
}
