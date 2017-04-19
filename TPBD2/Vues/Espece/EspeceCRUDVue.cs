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
    class EspeceCRUDVue: AbstractVueConsole
    {
        private EspeceSelectionVue selecteur;

        public EspeceCRUDVue(BDFacade facade, IIO IO): base(facade, IO)
        {
            selecteur = new EspeceSelectionVue(facade, IO);
        }


        //
        // CRUD 
        //

        /// <summary>
        /// Creation d'un espece
        /// </summary>
        /// <returns>l'Espece à ajouter, ou Null</returns>
        public Espece Creer()
        {
            Espece nouvelEspece = new Espece();

            ModifierAttributs(nouvelEspece);

           

            // confirmation
            _io.AfficheTexte("Voulez-vous vraiment ajouter :");
            _io.AfficheTexte(String.Format("{0} ",
                nouvelEspece.Nom
                ));

            char ajouter = _io.InputChar("O/N", new List<char> { 'O', 'N' }, true, 'O');
            if (ajouter == 'N')
            {
                nouvelEspece = null;
            }
            return nouvelEspece;
        }

        /// <summary>
        /// Demande quel espece effacer
        /// Il ne doit pas y avoir d'animaux associés à l'espèce qu'on veut effacer. 
        /// </summary>
        /// <returns>l'Espece à effacer, ou null</returns>
        public Espece Effacer()
        {
            _io.AfficheTexte("Quel espèce désirez-vous effacer?");
            int especeIdChoisi = selecteur.ChoisirEspece(true);
            Espece espece = null;
            if (especeIdChoisi != 0)
            {
                espece = _bdFacade.EspeceParID(especeIdChoisi);
                if (espece.Animals.Count != 0)
                {
                    _io.AfficheTexte(String.Format("Cette espèce ne peut être effacée car elle est associée aux animaux suivants"));
                    foreach(Animal animal in espece.Animals)
                    {
                        _io.AfficheTexte(String.Format("Nom de l'animal: {0}", animal.Nom));
                    }
                    _io.AttendreTouche("Une touche pour continuer");
                    espece = null;
                }
                else
                {
                    _io.AfficheTexte("Désirez-vous vraiment effacer cette espece");
                    AfficheEspeceComplet(espece);
                    char effacer = _io.InputChar("O/N ", new List<char> { 'O', 'N' }, true);
                    if (effacer.Equals('N'))
                    {
                        espece = null; //TODO: espece est suivit par le context. Que cé ca fait de le mettre à null?
                    }
                }
            }
            return espece;
        }


        /// <summary>
        /// Menu pour modifier un espece
        /// </summary>
        /// <returns>l'espece qui a été modifié</returns>
        public Espece Modifier()
        {
            //TODO: étant donné que le context.remove se fait dans le controlleur, 
            //      si je retourne voir les changements avant de sortir, ils ne sont pas faits
            _io.AfficheTexte("Quel espèce désirez-vous modifier?");
            int especeIdChoisi = selecteur.ChoisirEspece(true);
            Espece espece = null;
            if (especeIdChoisi == 0)
            {
                return null;
            }


            espece = _bdFacade.EspeceParID(especeIdChoisi);
            AfficheEspeceComplet(espece);

            List<String> optionsMenu = new List<string>();
            optionsMenu.Add("Que désirez-vous changer? ");

            optionsMenu.Add("1) Les attributs de l'espece ");
            optionsMenu.Add("2) Associer des animaux");
           
            optionsMenu.Add("0) sortir");

            //TODO: aurait-je besoin d'un sous-ctrl?
            int choix;
            do
            {
                _io.AfficheListe(optionsMenu);
                choix = _io.ChoisirOption(new List<int> { 0, 1, 2 });


                if (choix != 0)
                {
                    switch (choix)
                    {
                        case 1:
                            ModifierAttributs(espece);
                            break;
                        case 2:
                            ChangerAnimauxDEspece(espece);
                            break;                       
                    }

                    _io.AfficheTexte("");

                }
            } while (choix != 0);

            return espece;
        }

        /// <summary>
        /// Liste un espece et les animaux associés
        /// </summary>
        public void Afficher()
        {
            _io.AfficheTexte("Quel espèce désirez-vous afficher");
            int especeIdChoisi = selecteur.ChoisirEspece(true);

            if (especeIdChoisi != 0)
            {
                Espece espece = _bdFacade.EspeceParID(especeIdChoisi);                
                AfficheEspeceComplet(espece);
            }
        }


        //
        // Helpers d'ajout et de modification
        //

        //TODO: pas clean, le modèle Espece passé en in est modifié en mémoire. Au moins, il est pas sauvegardé dans la BD (ca c'est fait dans le ctrl). 

        /// <summary>
        /// Change les attributs simple de Espece
        /// Fournit des valeurs par défaut
        /// </summary>
        /// <param name="espece">l'espece a modifier</param>
        private void ModifierAttributs(Espece espece)
        {

            // Nom
            espece.Nom = _io.InputString("Nom de l'espèce: ", espece.Nom);

        }


        //TODO: ca fonctionne ... mais l'affichage est mélangeant car le savechanges se fait juste quand on sort. 

        /// <summary>
        /// Changement de l'espèce d'un animal.
        /// </summary>
        /// <param name="espece">l'espece à associer </param>
        private void ChangerAnimauxDEspece(Espece espece)
        {
            
            var animauxAssocies = _bdFacade.Animals().
                Select(p => new { p.ID, p.Nom, especeActuelle = p.Espece.Nom }) ;
            List<string> animauxAssociesMenu = new List<string>();
            List<int> animauxAssociesIdValide = new List<int>();

            _io.AfficheTexte("Choisissez un id d'animal");
            foreach (var animalAssocie in animauxAssocies)
            {
                animauxAssociesMenu.Add(string.Format("id: {0} Nom: {1} Espèce actuelle: {2}", animalAssocie.ID, animalAssocie.Nom, animalAssocie.especeActuelle));
                animauxAssociesIdValide.Add(animalAssocie.ID);
            }
            animauxAssociesMenu.Add("0 pour arrêter");
            animauxAssociesIdValide.Add(0);
            List<int> listeAnimauxAssocies = new List<int>();
            int animalChoisit;
            do
            {
                _io.AfficheListe(animauxAssociesMenu);
                animalChoisit = _io.ChoisirOption(animauxAssociesIdValide);
                if (animalChoisit != 0)
                {
                    listeAnimauxAssocies.Add(animalChoisit);
                    _io.AfficheTexte("et associer une autre animal ...");
                };

            } while (animalChoisit != 0);

            foreach (int idAnimal in listeAnimauxAssocies)
            {
                espece.Animals.Add(_bdFacade.AnimalParID(idAnimal));
            }
        }

       
        /// <summary>
        /// Affiche la fiche complète d'une espece, incluant les animaux de cette espèce. 
        /// A noter qu'il est mieux de eager load les animaux associés, 
        /// sinon ils seront lazy load.  
        /// </summary>
        /// <param name="espece"> l'espece à afficher</param>

        private void AfficheEspeceComplet( Espece espece)
        {
            _io.AfficheTexte(String.Format("id: {0} nom: {1}",
                       espece.ID, espece.Nom));

            foreach (Animal animal in espece.Animals)
            {
                _io.AfficheTexte(String.Format("     Animal associé: {0}", animal.Nom));
            }
        }
    }

}
