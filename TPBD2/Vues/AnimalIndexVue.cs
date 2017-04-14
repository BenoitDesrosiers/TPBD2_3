using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TPBD2.Controlleurs;
using TPBD2.IO;

namespace TPBD2.Vues
{
    class AnimalIndexVue:AbstractVueConsole
    {
        private ICRUDControlleur _CRUDctrl;
        private AnimalRapportCtrl _rapportCtrl;

        public AnimalIndexVue(TPBD2e7654321Entities context, AnimalCRUDCtrl CRUDctrl, AnimalRapportCtrl rapportCtrl, IIO IO): base(context, IO)
        {
            //TODO: ajouter des check pour null 
            _CRUDctrl = CRUDctrl;
            _rapportCtrl = rapportCtrl;

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
            optionsMenu.Add("4) Affichage d'un animal"); 
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
                            _CRUDctrl.Ajout();
                            break;
                        case 2:
                            _CRUDctrl.Effacer();
                            break;
                        case 3:
                            _CRUDctrl.Modifier();
                            break;
                        case 4:
                            _CRUDctrl.Afficher();
                            break;
                        case 5:
                            _rapportCtrl.RapportNombreSoin();
                            break;

                    }

                    Console.WriteLine("");

                }
            } while (choix != 0);
        }
    }
}
