using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPBD2
{
    class AnimalView : View
    {
        TPBD2e7654321Entities _context;
        AnimalCtrl _ctrl;

        public AnimalView(TPBD2e7654321Entities context, AnimalCtrl ctrl)
        {
            _context = context;
            _ctrl = ctrl;
        }


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
                            _ctrl.Ajout(_context);
                            break;
                        case 2:
                            _ctrl.Effacer(_context);
                            break;
                        case 4:
                            _ctrl.ListeProprietaires(_context);
                            break;
                        case 5:
                            _ctrl.RapportNombreSoin(_context);
                            break;

                    }

                    Console.WriteLine("");

                }
            } while (choix != 0);
        }

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

            return nouvelAnimal;
        }
    }
}
