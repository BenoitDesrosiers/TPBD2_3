using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TPBD2.Controlleurs;
using TPBD2.IO;
using TPBD2.Facade;

namespace TPBD2.Vues
{
    class AnimalIndexVue:AbstractVueConsole
    {
     

        public AnimalIndexVue(BDFacade facade, IIO IO): base(facade, IO)
        {}

        /// <summary>
        /// Menu principal pour les Animaux
        /// </summary>
        public int Index()
        {
            List<String> optionsMenu = new List<string>();

            optionsMenu.Add("1) Ajout d'un animal");
            optionsMenu.Add("2) Effacer un animal");
            optionsMenu.Add("3) Modifier un animal");
            optionsMenu.Add("4) Affichage d'un animal"); 
            optionsMenu.Add("5) Rapport sur le nombre de médicament par animal ");
            optionsMenu.Add("0) sortir");

            _io.AfficheListe(optionsMenu);
            return _io.ChoisirOption(new List<int> { 0, 1, 2, 3, 4, 5 });
        }
    }
}
