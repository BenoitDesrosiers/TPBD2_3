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
    class EspeceIndexVue:AbstractVueConsole
    {
     

        public EspeceIndexVue(BDFacade facade, IIO IO): base(facade, IO)
        {}

        /// <summary>
        /// Menu principal pour les Animaux
        /// </summary>
        public int Index()
        {
            List<String> optionsMenu = new List<string>();

            optionsMenu.Add("1) Ajout d'une espèce");
            optionsMenu.Add("2) Effacer une espèce");
            optionsMenu.Add("3) Modifier une espèce");
            optionsMenu.Add("4) Affichage d'une espèce"); 
            optionsMenu.Add("0) sortir");

            _io.AfficheListe(optionsMenu);
            return _io.ChoisirOption(new List<int> { 0, 1, 2, 3, 4 });
        }
    }
}
