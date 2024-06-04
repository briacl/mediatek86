﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Mediatek86.Data;
using Mediatek86.Models;

namespace Mediatek86.Views
{
    /// <summary>
    /// Classe ListeAbsences pour la gestion des absences des personnels.
    /// </summary>
    public partial class ListeAbsences : Page
    {
        private Personnel personnel;

        /// <summary>
        /// Constructeur de la page ListeAbsences.
        /// </summary>
        /// <param name="personnel">le personnel en cours de traitement</param>
        public ListeAbsences(Personnel personnel)
        {
            InitializeComponent();

            this.personnel = personnel;
            populateListAbsence(personnel);
        }

        /// <summary>
        /// Handler pour le bouton d'ajout d'une absence.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAjouterAbsence_Click(object sender, RoutedEventArgs e)
        {
            // Ajoutez votre code pour ajouter une absence ici
        }

        /// <summary>
        /// Handler pour le bouton de modification d'une absence.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModifierAbsence_Click(object sender, RoutedEventArgs e)
        {
            // Ajoutez votre code pour modifier une absence ici
        }

        /// <summary>
        /// Handler pour le bouton de suppression d'une absence.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSupprimerAbsence_Click(object sender, RoutedEventArgs e)
        {
            // On demande confirmation à l'utilisateur
            MessageBoxResult result = MessageBox.Show("Voulez-vous vraiment supprimer cette absence ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes) {
                using (var db = new MyDbContext())
                    {
                    // On récupère l'absence sélectionnée
                    Absence? absence = myDataGrid.SelectedItem as Absence;
                    if (absence != null)
                    {
                        // On supprime l'absence de la base de données
                        db.Absence?.Remove(absence);
                        db.SaveChanges();
                        // On rafraîchit la liste des absences
                        populateListAbsence(personnel);
                    }
                }
            }
        }

        /// <summary>
        /// Handler pour le bouton de fermeture de la page de gestion des absences.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFermer_Click(object sender, RoutedEventArgs e)
        {
            // Changez la navigation pour mettre la page ListePersonnel
            NavigationService.Navigate(new ListePersonnels());
        }

        /// <summary>
        /// Alimente la liste des absences pour un personnel donné.
        /// </summary>
        /// <param name="personnel"></param>
        /// <returns></returns>
        private void  populateListAbsence(Personnel personnel)
        {
            using (var db = new MyDbContext())
            {
                var absences = db.Absence?.Include("Motif").Where(a => a.IdPersonnel == personnel.IdPersonnel).ToList(); 
                myDataGrid.ItemsSource = absences;
            }
        }
    }
}