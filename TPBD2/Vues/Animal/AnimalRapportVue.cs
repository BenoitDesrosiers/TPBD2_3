using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TPBD2.IO;
using TPBD2.Facade;

namespace TPBD2.Vues
{
    class AnimalRapportVue: AbstractVueConsole
    {

        private AnimalSelectionVue selecteur;

        public AnimalRapportVue(BDFacade facade, IIO IO):base(facade, IO)
        {
            selecteur = new AnimalSelectionVue(facade, IO);

        }

        

        /// <summary>
        /// Affiche le nombre de soins recu par un Animal
        /// [répond à la question 4 a et b]
        /// </summary>
        public void RapportNombreSoin()
        {
            _io.AfficheTexte("Rapport sur quel animal:");
            int animalIdChoisi =  selecteur.ChoisirAnimal(true);

            if (animalIdChoisi != 0)
            {
                _io.AfficheTexte("Version avec syntaxe par requêtes");

                BDFacade.StructAnimalEtCompteDeSoin animalEtcompteParRequetes = _bdFacade.AnimalEtCompteDeSoinParRequêtes(animalIdChoisi);
                _io.AfficheTexte(String.Format("id: {0} nom: {1}  # de soins: {2}",animalEtcompteParRequetes.animal.ID, animalEtcompteParRequetes.animal.Nom, animalEtcompteParRequetes.compteDeSoin));
                _io.AfficheTexte("----------------------");

                _io.AfficheTexte("Version avec syntaxe par méthodes");
                BDFacade.StructAnimalEtCompteDeSoin animalEtcompteParMethodes = _bdFacade.AnimalEtCompteDeSoinParMéthodes(animalIdChoisi);
                _io.AfficheTexte(String.Format("id: {0} nom: {1}  # de soins: {2}", animalEtcompteParMethodes.animal.ID, animalEtcompteParMethodes.animal.Nom, animalEtcompteParMethodes.compteDeSoin));
                _io.AfficheTexte("----------------------");

                _io.AfficheTexte("");
                _io.AfficheTexte("une touche pour continuer");
                Console.ReadKey();
            }
        }

        
    }
}
