using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Specialized; // pour ListDictionnary
using System.Collections; // pour DictionnaryEntry

using TPBD2.IO;
using TPBD2.Facade;

namespace TPBD2.Vues
{
    class AnimalCRUDVue: AbstractVueConsole
    {
        private AnimalSelectionVue selecteur;

        public AnimalCRUDVue(BDFacade facade, IIO IO): base(facade, IO)
        {
            selecteur = new AnimalSelectionVue(facade, IO);
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
            _io.AfficheTexte("Voulez-vous vraiment ajouter :");
            _io.AfficheTexte(String.Format("{0} {1} {2} {3} {4} {5}",
                nouvelAnimal.Nom,
                nouvelAnimal.Espece.Nom,
                nouvelAnimal.Couleur,
                nouvelAnimal.Sexe,
                nouvelAnimal.Poids,
                nouvelAnimal.DateNaissance
                ));

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
            _io.AfficheTexte("Quel animal désirez-vous effacer?");
            int animalIdChoisi = selecteur.ChoisirAnimal(true);
            Animal animal = null;
            if (animalIdChoisi != 0)
            {
                _io.AfficheTexte("Désirez-vous vraiment effacer cet animal");
                animal = _bdFacade.AnimalParID(animalIdChoisi);
                AfficheAnimalComplet(animal);
                char effacer = _io.InputChar("O/N ", new List<char> { 'O', 'N' }, true);
                if (effacer.Equals('N'))
                {
                    animal = null; //TODO: animal est suivit par le context. Que cé ca fait de le mettre à null?
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
            //TODO: étant donné que le context.remove se fait dans le controlleur, 
            //      si je retourne voir les changements avant de sortir, ils ne sont pas faits
            //      exemple: enlever un propriétaire, retourner à enlever, celui enlever la première fois est encore dans la liste. 
            _io.AfficheTexte("Quel animal désirez-vous modifier?");
            int animalIdChoisi = selecteur.ChoisirAnimal(true);
            Animal animal = null;
            if (animalIdChoisi == 0)
            {
                return null;
            }


            animal = _bdFacade.AnimalParID(animalIdChoisi);
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

                    _io.AfficheTexte("");

                }
            } while (choix != 0);


            return animal;
        }

        /// <summary>
        /// Liste un animal et ses propriétaires
        /// [répond à la question 1b puisque la table Animal à une relation
        /// plusieurs à plusieurs avec Propriétaire.] 
        /// </summary>
        public void Afficher()
        {
            _io.AfficheTexte("Quel animal désirez-vous afficher");
            int animalIdChoisi = selecteur.ChoisirAnimal(true);

            if (animalIdChoisi != 0)
            {
                Animal animal = _bdFacade.AnimalParID(animalIdChoisi);                
                AfficheAnimalComplet(animal);
            }
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
            var especes = _bdFacade.Especes().Select(e => new { e.ID, e.Nom}) ;
            List<string> especesMenu = new List<string>();
            List<int> especesIdValide = new List<int>();
            foreach (var espece in especes)
            {
                especesMenu.Add(string.Format("id: {0} espece: {1}", espece.ID, espece.Nom));
                especesIdValide.Add(espece.ID);
            }
            _io.AfficheListe(especesMenu);
            int especeIDouNullSi0 = _io.ChoisirOption(especesIdValide, animal.EspeceID != 0 ? animal.EspeceID : (int?)null);
            animal.Espece = _bdFacade.EspeceParID(especeIDouNullSi0);

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
            
            var proprietaires = _bdFacade.Proprietaires().
                Select(p => new { p.ID, p.Nom }) ;
            List<string> proprietairesMenu = new List<string>();
            List<int> proprietairesIdValide = new List<int>();

            _io.AfficheTexte("Choisissez un id de propriétaire");
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
                    _io.AfficheTexte("et une autre proprietaire ...");
                };

            } while (proprioChoisit != 0);

            foreach (int idProprio in listeProprietaire)
            {
                animal.Proprietaires.Add(_bdFacade.ProprietaireParID(idProprio));
            }
        }

        /// <summary>
        /// Dissociation d'un ou de plusieurs propriétaires d'un animal
        /// [répond à la question 1c2]
        /// </summary>
        /// <param name="animal"></param>
        private void EnleverProprietaires(Animal animal)
        {
            var proprietaires = _bdFacade.Proprietaires().OrderBy(p => p.ID).Select(p => new { p.ID, p.Nom}); 
                //TODO: bug, la liste des proprietaires devrait être juste ceux associés présentement. 
            List<string> proprietairesMenu = new List<string>();
            List<int> proprietairesIdValide = new List<int>();

            _io.AfficheTexte("Quel propriétaire désirez-vous enlever");
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
                    _io.AfficheTexte("enlever un autre proprietaire ...");
                };

            } while (proprioChoisit != 0);


            // confirmation
            proprietaires = _bdFacade.Proprietaires()
                            .Where(p => listeProprietaire.Contains(p.ID))
                            .OrderBy(p => p.ID)
                            .Select(p => new { p.ID, p.Nom }); 
            proprietairesMenu = new List<string>();
            _io.AfficheTexte("Désirez-vous vraiment effacer ces propriétaires");
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
                    animal.Proprietaires.Remove(_bdFacade.ProprietaireParID(idProprio));
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
            IQueryable<Medicament> medicamentsNonPrescrits = _bdFacade.MedicamentsNonPrescritsPourUnAnimal(animal);
                
            ListDictionary medicamentsMenu = new ListDictionary();

            _io.AfficheTexte("Choisissez un id de médicament");
            foreach (Medicament medicamentNonPrescrit in medicamentsNonPrescrits)
            {
                medicamentsMenu.Add(medicamentNonPrescrit.ID, string.Format("id: {0} Nom: {1} Prix: {2}", medicamentNonPrescrit.ID, medicamentNonPrescrit.Nom, medicamentNonPrescrit.PrixUnitaire));
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
                    _io.AfficheTexte("et une autre médicament ...");
                };

            } while (medicamentChoisit != 0);

            foreach (DictionaryEntry idMedicamentEtQte in listeMedicamentIDetQte)
            {
                Soin soin = new Soin();
                soin.Animal = animal;
                soin.MedicamentID = (int)idMedicamentEtQte.Key; //TODO: je devrais pas associer la clé, mais bien l'objet médicament
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
            IQueryable<Medicament> medicamentsPrescrits = _bdFacade.MedicamentsPrescritsPourUnAnimal(animal);
            ListDictionary medicamentsMenu = new ListDictionary();

            _io.AfficheTexte("Choisissez un id de médicament à enlever");
            foreach (Medicament medicamentPrescrit in medicamentsPrescrits)
            {
                medicamentsMenu.Add(medicamentPrescrit.ID, string.Format("id: {0} Nom: {1} Prix: {2}", medicamentPrescrit.ID, medicamentPrescrit.Nom, medicamentPrescrit.PrixUnitaire));
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
                    _io.AfficheTexte("enlever une autre médicament ...");
                };

            } while (medicamentChoisit != 0);

            foreach (int idMedicament in listeMedicamentID)
            {
                Soin soin = _bdFacade.SoinPourUnAnimalEtUnMedicament(animal, idMedicament);
                //TODO: checker pour Null
                animal.Soins.Remove(soin);
            }
        }


        /// <summary>
        /// Affiche la fiche complète d'un animal. 
        /// A noter qu'il est mieux de eager load les propriétaires et l'espece, 
        /// sinon ils seront lazy load. 
        /// [répond à la question 1b puisque la table Animal à une relation
        /// plusieurs à plusieurs avec Propriétaire.] 
        /// </summary>
        /// <param name="animal"> l'animal à afficher</param>

        private void AfficheAnimalComplet( Animal animal)
        {
            _io.AfficheTexte(String.Format("id: {0} nom: {1}  espece: {2}",
                       animal.ID, animal.Nom, animal.Espece.Nom));
            _io.AfficheTexte(String.Format("couleur: {0} sexe: {1}  poids: {2} ",
                        animal.Couleur, animal.Sexe, animal.Poids));

            foreach (Proprietaire proprio in animal.Proprietaires)
            {
                _io.AfficheTexte(String.Format("     Proprietaire: {0}", proprio.Nom));
            }
        }
    }

}
