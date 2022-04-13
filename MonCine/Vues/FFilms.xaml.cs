using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MonCine.Data;

namespace MonCine.Vues
{
    /// <summary>
    /// Logique d'interaction pour Films.xaml
    /// </summary>
    public partial class FFilms : Page
    {
        private List<Film> Films { get; set; }
        private DAL Dal { get; set; }
        

        public FFilms(DAL pDal)
        {
            InitializeComponent();
            Dal = pDal;
            Films = Dal.ReadFilm();
            LstFilms.ItemsSource = Films;

            InitialConfiguration();
        }

        /// <summary>
        /// Définit l'état inital du form
        /// </summary>
        private void InitialConfiguration()
        {
            BtnDelete.IsEnabled = false;
            BtnUpdate.IsEnabled = false;
            NameField.Text = "";
        }

        private void BtnReturn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new Accueil());
        }
        

        private void LstFilms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Film film = (Film)LstFilms.SelectedItem;
            NameField.Text = film?.Name;

            BtnDelete.IsEnabled = film != null;
        }


        private async void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (LstFilms.SelectedIndex == -1)
            {
                MessageBox.Show("Veuillez choisir un film pour le modifier", "Erreur", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {

                Film film = (Film)LstFilms.SelectedItem;
                UpdateFilm(film);
                var result = await Dal.UpdateFilm(film);

                if (result)
                {
                    NameField.Text = "";
                    UpdateItems();
                    MessageBox.Show($"Le film {film.Name} a été mis à jour avec succès !", "Modification", MessageBoxButton.OK, MessageBoxImage.None);
                }

            }
        }

        private void UpdateFilm(Film pFilm)
        {
            pFilm.Name = NameField.Text;
        }

        /// <summary>
        /// Permet de mettre à jour les données des éléments affichés
        /// </summary>
        private void UpdateItems()
        {
            LstFilms.ItemsSource = Dal.ReadFilm();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
