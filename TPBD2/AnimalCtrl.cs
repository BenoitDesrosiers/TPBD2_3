﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TPBD2
{
    class AnimalCtrl
    {
        private TPBD2e7654321Entities _context;

        public AnimalCtrl(TPBD2e7654321Entities context)
        {
            _context = context;
        }
        public void Index()
        {
            AnimalView menu = new AnimalView(_context, this);
            menu.Index();            
        }

        /// <summary>
        /// Ajout d'un animal
        /// [répond à la question 2a avec FK non null sur Espèce
        ///  et à la question 1a pour la relation un à plusieurs sur Proprietaire ].  
        /// </summary>
        /// <param name="context"></param>
        public void Ajout()
        {
            AnimalView view = new AnimalView(_context, this);
            Animal nouvelAnimal = view.Creation();
            if ( nouvelAnimal != null)
            {
                _context.Animals.Add(nouvelAnimal);
                _context.SaveChanges();
            }

        }            

        /// <summary>
        /// Effacement d'un Animal
        /// [répond à la question 1d pour les proprietaire
        /// </summary>
        /// <param name="context"></param>
        public void Effacer()
        {
            AnimalView view = new AnimalView(_context, this);
            Animal animal = view.Effacement();

            if (animal != null)
                {
                    _context.Animals.Remove(animal);
                    _context.SaveChanges();
                }

        }
        
        /// <summary>
        /// affiche le contenu de la table Animal et la quantié de soins qu'ils ont recu
        /// [répond à la question 4 a et b]
        /// </summary>
        public void RapportNombreSoin()
        {
            AnimalView view = new AnimalView(_context, this);
            view.RapportNombreSoin();      
        }

        /// <summary>
        /// Liste un animal et ses propriétaires
        /// [répond à la question 1b puisque la table Animal à une relation
        /// plusieurs à plusieurs avec Propriétaire.] 
        /// </summary>
        public void ListeProprietaires()
        {
            AnimalView view = new AnimalView(_context, this);
            view.ListeProprietaires();
        }
       
    }
}
