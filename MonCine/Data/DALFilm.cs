﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MongoDB.Driver;

namespace MonCine.Data
{
    public class DALFilm : DAL, ICRUD<Film>
    {
        public string CollectionName { get; set; }

        public DALFilm()
        { 
            CollectionName = "Film";
            AddDefaultFilms();
        }


        /// <summary>
        /// Populer la collections films au départ
        /// </summary>
        private async void AddDefaultFilms()
        {
            List<Film> films = new List<Film>
            {
                new Film("Film 1"),
                new Film("Film 2"),
                new Film("Film 3")
            };

            try
            {
                var collection = database.GetCollection<Film>(CollectionName);
                if (collection.CountDocuments(Builders<Film>.Filter.Empty) <= 0)
                {
                    await collection.InsertManyAsync(films);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossible d'ajouter des films dans la collection " + ex.Message, "Erreur",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                throw;
            }
        }



        /// <summary>
        /// Récupère l'ensemble des films de la BD
        /// </summary>
        /// <returns>Liste de films</returns>
        public List<Film> ReadItems()
        {
            List<Film> films = new List<Film>();

            try
            {
                var collection = database.GetCollection<Film>("Films");
                films = collection.Aggregate().ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossible d'obtenir la collection " + ex.Message, "Erreur", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

            return films;
        }




        public async Task<bool> AddItem(Film pFilm)
        {
            if (pFilm is null)
            {
                throw new ArgumentNullException("pFilm", "Le film ne peut pas être null");
            }

            try
            {
                var collection = database.GetCollection<Film>(CollectionName);
                await collection.InsertOneAsync(pFilm);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Impossible d'ajouter le film {pFilm.Name} dans la collection {ex.Message}",
                    "Erreur d'ajout", MessageBoxButton.OK, MessageBoxImage.Error);

                throw;
            }

            return true;
        }

        public async Task<bool> UpdateItem(Film pFilm)
        {
            if (pFilm is null)
            {
                throw new ArgumentNullException("pFilm", "Le film ne peut pas être null");
            }

            try
            {
                var collection = database.GetCollection<Film>(CollectionName);
                await collection.ReplaceOneAsync(x => x.Id == pFilm.Id, pFilm);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Impossible de mettre à jour le film {pFilm.Name} dans la collection {ex.Message}",
                    "Erreur de mise à jour", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }

            return true;
        }

        public async Task<bool> DeleteItem(Film pFilm)
        {
            if (pFilm is null)
            {
                throw new ArgumentNullException("pFilm", "Le film ne peut pas être null");
            }

            try
            {
                var collection = database.GetCollection<Film>(CollectionName);
                await collection.DeleteOneAsync(Builders<Film>.Filter.Eq(x => x.Id, pFilm.Id));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Impossible de mettre à jour le film {pFilm.Name} dans la collection {ex.Message}", "Erreur de mise à jour", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }

            return true;
        }

       
    }
}