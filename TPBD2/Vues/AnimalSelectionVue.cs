using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TPBD2.Controlleurs;

namespace TPBD2.Vues
{
    /// <summary>
    /// Permet de choisir un animal parmi ceux existants.
    /// </summary>
    class AnimalSelectionVue
    {
        protected TPBD2e7654321Entities _context;
        protected IIO _io;

        public AnimalSelectionVue(TPBD2e7654321Entities context, IIO IO)
        {
            _context = context;
            _io = IO;
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

            IQueryable<Animal> animals = from a in _context.Animals
                                         select a;
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
