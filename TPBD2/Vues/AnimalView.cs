using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Specialized; // pour ListDictionnary
using System.Collections; // pour DictionnaryEntry

using TPBD2.Controlleurs;

namespace TPBD2.Vues
{
    class AnimalVue: VueConsole
    {
        public AnimalVue(TPBD2e7654321Entities context, AnimalCtrl ctrl, IIO IO): base(context, ctrl, IO)
        {}

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
                _io.AfficheListe(optionsMenu);
                choix = _io.ChoisirOption(new List<int> { 0, 1, 2, 3, 4, 5 });


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
                        case 3:
                            _ctrl.Modifier();
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
        public Animal Creer()
        {
            Animal nouvelAnimal = new Animal();

            ModifierAttributs(nouvelAnimal);

            AjouterProprietaires(nouvelAnimal);

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

            char ajouter = _io.InputChar("O/N", new List<char> { 'O', 'N' }, true, 'O');
            if (ajouter == 'N')
            {
                nouvelAnimal = null;
            }
            return nouvelAnimal;
        }

        /// <summary>
        /// Demande quel animal effacer
        /// [répond à 1d1, 1d2, 1d3]
        /// </summary>
        /// <returns>l'Animal à effacer, ou null</returns>
        public Animal Effacer()
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
                char effacer = _io.InputChar("O/N ", new List<char> { 'O', 'N' }, true);
                if (effacer.Equals('N'))
                {
                    animal = null; //FIXME: animal est suivit par le context. Que cé ca fait de le mettre à null?
                }
            }
            return animal;
        }


        /// <summary>
        /// Menu pour modifier un animal
        /// </summary>
        /// <returns>l'animal qui a été modifié</returns>
        public Animal Modifier()
        {
            //FIXME: étant donné que le context.remove se fait dans le controlleur, 
            //      si je retourne voir les changements avant de sortir, ils ne sont pas faits
            //      exemple: enlever un propriétaire, retourner à enlever, celui enlever la première fois est encore dans la liste. 
            Console.WriteLine("Quel animal désirez-vous modifier?");
            int animalIdChoisi = ChoisirAnimal(true);
            Animal animal = null;
            if (animalIdChoisi == 0)
            {
                return null;
            }


            animal = _context.Animals
                .Include(nameof(Animal.Proprietaires))
                .Include(nameof(Animal.Espece))
                .Where(a => a.ID == animalIdChoisi).First();
            AfficheAnimalComplet(animal);

            List<String> optionsMenu = new List<string>();
            optionsMenu.Add("Que désirez-vous changer? ");

            optionsMenu.Add("1) Les attributs de l'animal ");
            optionsMenu.Add("2) Ajouter des propriétaires");
            optionsMenu.Add("3) Enlever des propriétaires");
            optionsMenu.Add("4) Ajouter des soins");
            optionsMenu.Add("5) Enlever des soins");

            optionsMenu.Add("0) sortir");

            int choix;
            do
            {
                _io.AfficheListe(optionsMenu);
                choix = _io.ChoisirOption(new List<int> { 0, 1, 2, 3, 4, 5 });


                if (choix != 0)
                {
                    switch (choix)
                    {
                        case 1:
                            ModifierAttributs(animal);
                            break;
                        case 2:
                            AjouterProprietaires(animal);
                            break;
                        case 3:
                            EnleverProprietaires(animal);
                            break;
                        case 4:
                            AjouterSoins(animal);
                            break;
                        case 5:
                            EnleverSoins(animal);
                            break;
                            
                    }

                    Console.WriteLine("");

                }
            } while (choix != 0);


            return animal;
        }

        //
        // Helpers d'ajout et de modification
        //

        //TODO: pas clean, le modèle Animal passé en in est modifié en mémoire. Au moins, il est pas sauvegardé dans la BD (ca c'est fait dans le ctrl). 

        /// <summary>
        /// Change les attributs simple de Animal
        /// Fournit des valeurs par défaut
        /// [répond à 1c1]
        /// </summary>
        /// <param name="animal">l'animal a modifier</param>
        private void ModifierAttributs(Animal animal)
        {

            // Nom
            animal.Nom = _io.InputString("Nom de l'animal: ", animal.Nom);

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
            _io.AfficheListe(especesMenu);
            animal.Espece = _context.Especes.Find(_io.ChoisirOption(especesIdValide, animal.EspeceID != 0 ? animal.EspeceID : (int?)null));

            // Couleur
            animal.Couleur = _io.InputString("Couleur de l'animal: ", animal.Couleur);

            // Sexe
            animal.Sexe = Convert.ToString(_io.InputChar("Sexe (M/F): ", new List<char> { 'M', 'F' }, true, animal.Sexe != null ? animal.Sexe[0] : 'M'));

            // Poids
            animal.Poids = _io.InputInt("Poids: ", animal.Poids);

            // Date de naissance
            animal.DateNaissance = _io.InputDate("Date de naissance (AAAA-MM-JJ): ", animal.DateNaissance);


        }


        /// <summary>
        /// Association d'un ou de plusieurs propriétaires à un animal
        /// [ répond à la question 1a et 1c2 quand ca vient de ModifierProprietaires]
        /// </summary>
        /// <param name="animal">l'animal à modifier</param>
        private void AjouterProprietaires(Animal animal)
        {
            
            var proprietaires = (from p in _context.Proprietaires
                                 select (new { p.ID, p.Nom }));
            List<string> proprietairesMenu = new List<string>();
            List<int> proprietairesIdValide = new List<int>();

            Console.WriteLine("Choisisez un id de propriétaire");
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
                _io.AfficheListe(proprietairesMenu);
                proprioChoisit = _io.ChoisirOption(proprietairesIdValide);
                if (proprioChoisit != 0)
                {
                    listeProprietaire.Add(proprioChoisit);
                    Console.WriteLine("et une autre proprietaire ...");
                };

            } while (proprioChoisit != 0);

            foreach (int idProprio in listeProprietaire)
            {
                animal.Proprietaires.Add(_context.Proprietaires.Find(idProprio));
            }
        }

        /// <summary>
        /// Dissociation d'un ou de plusieurs propriétaires d'un animal
        /// [répond à la question 1c2]
        /// </summary>
        /// <param name="animal"></param>
        private void EnleverProprietaires(Animal animal)
        {
            var proprietaires = (from p in animal.Proprietaires
                                 orderby(p.ID)
                                 select (new { p.ID, p.Nom }));
            List<string> proprietairesMenu = new List<string>();
            List<int> proprietairesIdValide = new List<int>();

            Console.WriteLine("Quel propriétaire désirez-vous enlever");
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
                _io.AfficheListe(proprietairesMenu);
                proprioChoisit = _io.ChoisirOption(proprietairesIdValide);
                if (proprioChoisit != 0)
                {
                    listeProprietaire.Add(proprioChoisit);
                    Console.WriteLine("enlever un autre proprietaire ...");
                };

            } while (proprioChoisit != 0);


            // confirmation
            proprietaires = (from p in animal.Proprietaires
                             where listeProprietaire.Contains(p.ID)
                             orderby p.ID
                             select (new { p.ID, p.Nom }));
            proprietairesMenu = new List<string>();
            Console.WriteLine("Désirez-vous vraiment effacer ces propriétaires");
            foreach (var proprietaire in proprietaires)
            {
                proprietairesMenu.Add(string.Format("id: {0} Nom: {1}", proprietaire.ID, proprietaire.Nom));
            }
            _io.AfficheListe(proprietairesMenu);

            char effacer = _io.InputChar("O/N ", new List<char> { 'O', 'N' }, true, 'O');
            if (effacer == 'O')
            {
                foreach (int idProprio in listeProprietaire)
                {
                    animal.Proprietaires.Remove(_context.Proprietaires.Find(idProprio));
                }
            }
        }

        /// <summary>
        /// Association d'un ou de plusieurs soins à un animal
        /// [ répond à la question 1a et 1c2 ]
        /// </summary>
        /// <param name="animal">l'animal à modifier</param>
        private void AjouterSoins(Animal animal)
        {
            var soins = (from m in _context.Medicaments
                         where !(from s in _context.Soins
                                 where s.AnimalID == animal.ID
                                 select s.MedicamentID).Contains(m.ID)
                         select m);
            ListDictionary medicamentsMenu = new ListDictionary();
            
            Console.WriteLine("Choisisez un id de médicament");
            foreach (var soin in soins)
            {
                medicamentsMenu.Add(soin.ID, string.Format("id: {0} Nom: {1} Prix: {2}", soin.ID, soin.Nom, soin.PrixUnitaire));
            }
            medicamentsMenu.Add(0, "0 pour arrêter");
            
            Hashtable listeMedicamentIDetQte = new Hashtable();
            int medicamentChoisit;
            do
            {
                _io.AfficheListe(medicamentsMenu.Values.OfType<String>().ToList());
                medicamentChoisit = _io.ChoisirOption(medicamentsMenu.Keys.OfType<int>().ToList());
                if (medicamentChoisit != 0)
                {
                    medicamentsMenu.Remove(medicamentChoisit);
                    int qte = _io.InputInt("Quel quantité? : ", 1);
                    listeMedicamentIDetQte.Add(medicamentChoisit, qte); 
                    Console.WriteLine("et une autre médicament ...");
                };

            } while (medicamentChoisit != 0);

            foreach (DictionaryEntry idMedicamentEtQte in listeMedicamentIDetQte)
            {
                Soin soin = new Soin();
                soin.Animal = animal;
                soin.MedicamentID = (int)idMedicamentEtQte.Key;
                soin.Quantite = (int) idMedicamentEtQte.Value;
                animal.Soins.Add(soin);
            }
        }


        /// <summary>
        /// Dissociation d'un ou de plusieurs soins d'un animal
        /// [ répond à la question 1a et 1c2 ]
        /// </summary>
        /// <param name="animal">l'animal à modifier</param>
        private void EnleverSoins(Animal animal)
        {
            var soins = (from m in _context.Medicaments
                         where (from s in _context.Soins
                                 where s.AnimalID == animal.ID
                                 select s.MedicamentID).Contains(m.ID)
                         select m);
            ListDictionary medicamentsMenu = new ListDictionary();

            Console.WriteLine("Choisisez un id de médicament à enlever");
            foreach (var soin in soins)
            {
                medicamentsMenu.Add(soin.ID, string.Format("id: {0} Nom: {1} Prix: {2}", soin.ID, soin.Nom, soin.PrixUnitaire));
            }
            medicamentsMenu.Add(0, "0 pour arrêter");

            List<int> listeMedicamentID = new List<int>();
            int medicamentChoisit;
            do
            {
                _io.AfficheListe(medicamentsMenu.Values.OfType<String>().ToList());
                medicamentChoisit = _io.ChoisirOption(medicamentsMenu.Keys.OfType<int>().ToList());
                if (medicamentChoisit != 0)
                {
                    medicamentsMenu.Remove(medicamentChoisit);
                    listeMedicamentID.Add(medicamentChoisit);
                    Console.WriteLine("enlever une autre médicament ...");
                };

            } while (medicamentChoisit != 0);

            foreach (int idMedicament in listeMedicamentID)
            {
                Soin soin = (from s in _context.Soins
                            where s.AnimalID == animal.ID && s.MedicamentID == idMedicament
                            select s).Single();
                animal.Soins.Remove(soin);
            }
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
        public void Liste()
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
            _io.AfficheListe(animauxMenu);

            return _io.ChoisirOption(animauxIdValide);


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
