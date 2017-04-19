using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TPBD2.Controlleurs;
using TPBD2.Facade;
using TPBD2.IO;

namespace TPBD2.Vues
{
    class MenuPrincipaleVue: AbstractVueConsole
    {
        public MenuPrincipaleVue(BDFacade facade, IIO IO):base(facade,IO)
        {
        }
        public int Index()
        {

            List<String> optionsMenu = new List<string>();

            optionsMenu.Add("1) Gestion des Animaux");
            optionsMenu.Add("2) Gestion des Propriétaires");
            optionsMenu.Add("3) Gestion des Médicaments");
            optionsMenu.Add("4) Gestion des ");
            optionsMenu.Add("5) Agrégation ");
            optionsMenu.Add("0) sortir");

            _io.AfficheListe(optionsMenu);
            return _io.ChoisirOption(new List<int> { 0, 1, 2, 3, 4, 5 });
                
        }
    }
}
