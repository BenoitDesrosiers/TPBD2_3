using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TPBD2.Vues;

namespace TPBD2.Controlleurs
{
    class AnimalCRUDCtrl : AbstractCRUDCtrl
    {
        
        public AnimalCRUDCtrl(TPBD2e7654321Entities context, IIO io):base(context, io)
        { }
       

        //
        // CRUD
        //

        /// <summary>
        /// Ajout d'un animal
        /// [répond à la question 2a avec FK non null sur Espèce
        ///  et à la question 1a pour la relation un à plusieurs sur Proprietaire ].  
        /// </summary>
        /// <param name="context"></param>
        override public void Ajout()
        {
            AnimalCRUDVue view = new AnimalCRUDVue(_context, _io);
            Animal nouvelAnimal = view.Creer();
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
        override public void Effacer()
        {
            AnimalCRUDVue view = new AnimalCRUDVue(_context, _io);
            Animal animal = view.Effacer();

            if (animal != null)
                {
                    _context.Animals.Remove(animal);
                    _context.SaveChanges();
                }

        }

        override public void Modifier()
        {
            AnimalCRUDVue view = new AnimalCRUDVue(_context, _io);
            Animal animalModifie = view.Modifier();
            if (animalModifie != null)
            {
                //_context.Animals.Add(animalModifie);
                _context.SaveChanges();
            }
        }

        public override void Afficher()
        {
            throw new NotImplementedException();
        }

       
       
    }
}
