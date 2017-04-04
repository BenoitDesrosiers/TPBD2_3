using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPBD2
{
    class AnimalView : View
    {
        private TPBD2e7654321Entities _context;
        private AnimalCtrl _ctrl;

        public AnimalView(TPBD2e7654321Entities context, AnimalCtrl ctrl)
        {
            _context = context;
            _ctrl = ctrl;
        }

        /// <summary>
        /// Menu principal pour les Animaux
        /// </summary>
        public void Index()
        {
            List<String> optionsMenu = new List<string>();

            optionsMenu.Add("1) Ajout d'un animal");
            optionsMenu.Add("2) Effacer un animal");
            optionsMenu.Add("3) Modifier un animal");
            optionsMenu.Add("4) Liste des animaux");
            optionsMenu.Add("5) Rapport sur le nombre de médicament par animal ");
            optionsMenu.Add("0) sortir");

            int choix;
            do
            {
                AfficheListe(optionsMenu);
                choix = ChoisirOption(new List<int> { 0, 1, 2, 3, 4, 5 });


                if (choix != 0)
                {
                    switch (choix)
                    {
                        case 1:
                            _ctrl.Ajout();
                            break;
                        case 2:
                            _ctrl.Effacer();
                            break;
                        case 4:
                            _ctrl.ListeProprietaires();
                            break;
                        case 5:
                            _ctrl.RapportNombreSoin();
                            break;

                    }

                    Console.WriteLine("");

                }
            } while (choix != 0);
        }

        //
        // CRUD 
        //

        /// <summary>
        /// Creation d'un animal
        /// </summary>
        /// <returns>l'Animal à ajouter, ou Null</returns>
        public Animal Creation()
        {
            Animal nouvelAnimal = new Animal();

            // Nom
            nouvelAnimal.Nom = InputString("Nom de l'animal: ");

            // Espèce à partir de la liste des espèces [répond à la question 2a ]
            var especes = (from e in _context.Especes
                           select (new { e.ID, e.Nom }));
            List<string> especesMenu = new List<string>();
            List<int> especesIdValide = new List<int>();
            foreach (var espece in especes)
            {
                especesMenu.Add(string.Format("id: {0} espece: {1}", espece.ID, espece.Nom));
                especesIdValide.Add(espece.ID);
            }
            AfficheListe(especesMenu);
            nouvelAnimal.Espece = _context.Especes.Find(ChoisirOption(especesIdValide));

            // Couleur
            nouvelAnimal.Couleur = InputString("Couleur de l'animal: ");

            // Sexe
            nouvelAnimal.Sexe = Convert.ToString(InputChar("Sexe (M/F): ", new List<char> { 'M', 'F' }, true, 'M'));

            // Poids
            nouvelAnimal.Poids = InputInt("Poids: ");

            // Date de naissance
            nouvelAnimal.DateNaissance = InputDate("Date de naissance (AAAA-MM-JJ): ");

            // Propriétaire(s)  réponds à la question 1a
            var proprietaires = (from p in _context.Proprietaires
                                 select (new { p.ID, p.Nom }));
            List<string> proprietairesMenu = new List<string>();
            List<int> proprietairesIdValide = new List<int>();

            Console.WriteLine("Choissisez un id de propriétaire");
            foreach (var proprietaire in proprietaires)
            {
                proprietairesMenu.Add(string.Format("id: {0} Nom: {1}", proprietaire.ID, proprietaire.Nom));
                proprietairesIdValide.Add(proprietaire.ID);
            }
            proprietairesMenu.Add("0 pour arrêter");
            proprietairesIdValide.Add(0);
            List<int> listeProprietaire = new List<int>();
            int proprioChoisit;
            do
            {
                AfficheListe(proprietairesMenu);
                proprioChoisit = ChoisirOption(proprietairesIdValide);
                if (proprioChoisit != 0)
                {
                    listeProprietaire.Add(proprioChoisit);
                    Console.WriteLine("et une autre proprietaire ...");
                };

            } while (proprioChoisit != 0);

            foreach (int idProprio in listeProprietaire)
            {
                nouvelAnimal.Proprietaires.Add(_context.Proprietaires.Find(idProprio));
            }

            // confirmation
            Console.WriteLine("Voulez-vous vraiment ajouter :");
            Console.WriteLine("{0} {1} {2} {3} {4} {5}",
                nouvelAnimal.Nom,
                nouvelAnimal.Espece.Nom,
                nouvelAnimal.Couleur,
                nouvelAnimal.Sexe,
                nouvelAnimal.Poids,
                nouvelAnimal.DateNaissance
                );

            char ajouter = InputChar("O/N", new List<char> { 'O', 'N' }, true);
            if (ajouter == 'N')
            {
                nouvelAnimal = null;
            }
            return nouvelAnimal;
        }

        /// <summary>
        /// Demande quel animal effacer
        /// </summary>
        /// <returns>l'Animal à effacer, ou null</returns>
        public Animal Effacement()
        {
            Console.WriteLine("Quel animal désirez-vous effacer?");
            int animalIdChoisi = ChoisirAnimal(true);
            Animal animal = null;
            if (animalIdChoisi != 0)
            {
                Console.WriteLine("Désirez-vous vraiment effacer cet animal");
                animal = _context.Animals
                    .Include(nameof(Animal.Proprietaires))
                    .Include(nameof(Animal.Espece))
                    .Where(a => a.ID == animalIdChoisi).First();
                AfficheAnimalComplet(animal);
                View view = new View();
                char effacer = view.InputChar("O/N ", new List<char> { 'O', 'N' }, true);
                if (effacer.Equals('N'))
                {
                    animal = null;
                }
            }
            return animal;
        }


        //
        // Rapports
        //

        /// <summary>
        /// Affiche le contenu de la table Animal et la quantié de soins qu'ils ont recu
        /// [répond à la question 4 a et b]
        /// </summary>
        public void RapportNombreSoin()
        {
            Console.WriteLine("Rapport sur quel animal:");
            int animalIdChoisi = ChoisirAnimal( true);

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

        /// <summary>
        /// Liste un animal et ses propriétaires
        /// [répond à la question 1b puisque la table Animal à une relation
        /// plusieurs à plusieurs avec Propriétaire.] 
        /// </summary>
        public void ListeProprietaires()
        {
            Console.WriteLine("Pour quel animal désirez-vous un rapport ");
            int animalIdChoisi = ChoisirAnimal(true);

            if (animalIdChoisi != 0)
            {
                var animaux = _context.Animals
                    .Include(nameof(Animal.Proprietaires))
                    .Include(nameof(Animal.Espece))
                    .Where(a => a.ID.Equals(animalIdChoisi));
                foreach (Animal animal in animaux)
                {
                    AfficheAnimalComplet(animal);
                }
            }
        }


        //
        // Helpers
        //
        /// <summary>
        /// Permet de sélectionner un animal dans la liste complète
        /// </summary>
        /// <param name="optionAnnuler">Si vrai, ajoute l'option 0 pour annuler</param>
        /// <returns>l'id de l'animal choisi ou 0</returns>
        private int ChoisirAnimal( bool optionAnnuler = false)
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
            AfficheListe(animauxMenu);

            return ChoisirOption(animauxIdValide);


        }

        /// <summary>
        /// Affiche la fiche complète d'un animal. 
        /// A noter qu'il est mieux de eager load les propriétaires et l'espece, 
        /// sinon ils seront lazy load. 
        /// </summary>
        /// <param name="animal"> l'animal à afficher</param>

        private void AfficheAnimalComplet( Animal animal)
        {
            Console.WriteLine("id: {0} nom: {1}  espece: {2}",
                       animal.ID, animal.Nom, animal.Espece.Nom);
            Console.WriteLine("couleur: {0} sexe: {1}  poids: {2} ",
                        animal.Couleur, animal.Sexe, animal.Poids);

            foreach (Proprietaire proprio in animal.Proprietaires)
            {
                Console.WriteLine("     Proprietaire: {0}", proprio.Nom);
            }
        }
    }

}
