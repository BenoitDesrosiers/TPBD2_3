using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TPBD2.IO;
using TPBD2.Facade;

namespace TPBD2.Vues
{
    /// <summary>
    /// Permet de choisir un animal parmi ceux existants.
    /// </summary>
    class AnimalSelectionVue: AbstractVueConsole
    {

        public AnimalSelectionVue(BDFacade facade, IIO IO): base(facade, IO)
        {
        }

        //
        // Helpers
        //
        /// <summary>
        /// Permet de sélectionner un animal dans la liste complète
        /// </summary>
        /// <param name="optionAnnuler">Si vrai, ajoute l'option 0 pour annuler</param>
        /// <returns>l'id de l'animal choisi ou 0</returns>
        public int ChoisirAnimal(bool optionAnnuler = false)
        {

            IQueryable<Animal> animals = _bdFacade.Animals();
            List<string> animauxMenu = new List<string>();
            List<int> animauxIdValide = new List<int>();

            foreach (Animal animal in animals)
            {
                animauxMenu.Add(string.Format("id: {0} nom: {1}", animal.ID, animal.Nom));
                animauxIdValide.Add(animal.ID);
            }
            if (optionAnnuler)
            {
                animauxMenu.Add("0 pour annuler");
                animauxIdValide.Add(0);
            }
            _io.AfficheListe(animauxMenu);

            return _io.ChoisirOption(animauxIdValide);


        }
    }
}
