using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MonCine.Data
{
    internal class Film
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public List<Categorie> Categories { get; set; }
        public DateTime DateSortie { get; set; }
        public DateTime DateProjection { get; set; }
        public DateTime DerniereProjection { get; set; }
        public bool SurAffiche { get; set; }
        public List<Acteur> Acteurs { get; set; }
        public List<Realisateur> Realisateurs { get; set; }
        private List<int> Notes { get; set; }
        [Range(0,2, ErrorMessage = "Le nombre de projection d'un film ne peut pas dépasser 2 projections pas années")]
        private int NbProjection { get; set; }



        public Film(string pName)
        {
            Name = pName;
        }

        public double CalculerMoyennesNotes()
        {
            return Notes.Sum(x => x) / Notes.Count;
        }

        public void Noter(int note)
        {
            if (note < 0 || note > 10)
            {
                throw new ArgumentOutOfRangeException("note", "La notre attribué doit être comprise entre 1 et 10");
            }

            Notes.Add(note);
        }

        public Film SelectionnerFilm()
        {
            return this;
        }

        public bool EstAdmissibleAReprojecter()
        {
            return NbProjection <= 1;
        }







    }
}
