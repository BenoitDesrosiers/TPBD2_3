using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TPBD2.IO;
using TPBD2.Vues;
using TPBD2.Facade;

namespace TPBD2.Controlleurs
{
    class AnimalCRUDCtrl : AbstractCRUDCtrl
    {
        
        public AnimalCRUDCtrl(BDFacade facade, IIO io):base(facade, io)
        { }


        //
        // CRUD
        //

        /// <summary>
        /// Ajout d'un animal
        /// [répond à la question 2a avec FK non null sur Espèce
        ///  et à la question 1a pour la relation un à plusieurs sur Proprietaire ].  
        /// </summary>
        override public void Ajout()
        {
            AnimalCRUDVue view = new AnimalCRUDVue(_facade, _io);
            Animal nouvelAnimal = view.Creer();
            if ( nouvelAnimal != null)
            {
                _facade.SauvegardeAnimal(nouvelAnimal);
            }

        }            

        /// <summary>
        /// Effacement d'un Animal
        /// [répond à la question 1d pour les proprietaire
        /// </summary>
        override public void Effacer()
        {
            AnimalCRUDVue view = new AnimalCRUDVue(_facade, _io);
            Animal animal = view.Effacer();

            if (animal != null)
                {
                    _facade.EffaceAnimal(animal);
                }

        }

        override public void Modifier()
        {
            AnimalCRUDVue view = new AnimalCRUDVue(_facade, _io);
            Animal animalModifie = view.Modifier();
            if (animalModifie != null)
            {
                _facade.SaveChanges();
            }
        }

        public override void Afficher()
        {
            AnimalCRUDVue view = new AnimalCRUDVue(_facade, _io);
            view.Afficher();
        }

       
       
    }
}
