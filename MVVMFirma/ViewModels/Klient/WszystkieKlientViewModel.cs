﻿using GalaSoft.MvvmLight.Messaging;
using MVVMFirma.Models.EntitiesForView;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace MVVMFirma.ViewModels
{
    public class WszystkieKlientViewModel : WszystkieViewModel<KlientForAllView>
    {
        #region Constructor
        public WszystkieKlientViewModel()
            : base("Klienci")
        {
            // odbieranie wiadomości odświeżenia listy
            Messenger.Default.Register<string>(this, OnMessageReceived);
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<KlientForAllView>
                (
                    from klient in hotelEntities.Klient
                    select new KlientForAllView
                    {
                        IdKlienta = klient.IdKlienta,
                        Imie = klient.Imie,
                        Nazwisko = klient.Nazwisko,
                        Ulica = klient.Ulica,
                        NrDomu = klient.NrDomu,
                        NrLokalu = klient.NrLokalu,
                        KodPocztowy = klient.KodPocztowy,
                        Miasto = klient.Miasto,
                        Kraj = klient.Kraj.Nazwa,
                        Email = klient.Email,
                        Telefon = klient.Telefon,
                        NIP = klient.NIP
                    }
                );
        }

        public override void Delete()
        {
            MessageBoxResult delete = MessageBox.Show("Czy na pewno chcesz usunąć wybranego klienta:\n" + SelectedItem.Imie + " " + SelectedItem.Nazwisko, "Usuwanie", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (SelectedItem != null && delete == MessageBoxResult.Yes)
            {
                hotelEntities.Klient.Remove(hotelEntities.Klient.FirstOrDefault(f => f.IdKlienta == SelectedItem.IdKlienta));
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
                Messenger.Default.Send(DisplayName + "Edit-" + SelectedItem.IdKlienta);
            }
        }

        // OnMessageReceived obsługuje wiadomość dotyczącą odświeżenia listy w widoku Wszystkie..View, wysłaną przy zapisie edytowanego rekordu 
        private void OnMessageReceived(string message)
        {
            if (message == "KlientRefresh")
            {
                Load();
            }
        }
        #endregion
    }
}
