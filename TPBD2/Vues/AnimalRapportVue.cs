using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TPBD2.Controlleurs;

namespace TPBD2.Vues
{
    class AnimalRapportVue
    {
        protected TPBD2e7654321Entities _context;
        protected IIO _io;

        private AnimalSelectionVue selecteur;

        public AnimalRapportVue(TPBD2e7654321Entities context, AnimalRapportCtrl ctrl, IIO IO)
        {
            _context = context;
            _io = IO;

            selecteur = new AnimalSelectionVue(context, IO);

        }

        

        /// <summary>
        /// Affiche le contenu de la table Animal et la quantié de soins qu'ils ont recu
        /// [répond à la question 4 a et b]
        /// </summary>
        public void RapportNombreSoin()
        {
            Console.WriteLine("Rapport sur quel animal:");
            int animalIdChoisi =  selecteur.ChoisirAnimal(true);

            if (animalIdChoisi != 0)
            {
                Console.WriteLine("Version avec syntaxe par requêtes");

                var animauxEtcompte = from a in _context.Animals
                                      let ac = new
                                      {
                                          a,
                                          compte = a.Soins.Count
                                      }
                                      where a.ID.Equals(animalIdChoisi)
                                      select (ac);

                foreach (var animal in animauxEtcompte)
                {
                    Console.WriteLine("id: {0} nom: {1}  # de soins: {2}", animal.a.ID, animal.a.Nom, animal.compte);
                }

                Console.WriteLine("----------------------");

                Console.WriteLine("Version avec syntaxe par méthodes");
                var animauxEtcompte2 = _context.Animals
                                    .Where(c => c.ID == animalIdChoisi)
                                    .Select(a => new { a, compte = a.Soins.Count });

                foreach (var animal in animauxEtcompte2)
                {
                    Console.WriteLine("id: {0} nom: {1}  # de soins: {2}", animal.a.ID, animal.a.Nom, animal.compte);
                }

                Console.WriteLine("----------------------");

                Console.WriteLine("");
                Console.WriteLine("une touche pour continuer");
                Console.ReadKey();
            }
        }

        
    }
}
