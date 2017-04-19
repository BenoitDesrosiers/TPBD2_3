using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TPBD2.Facade;
using TPBD2.IO;
using TPBD2.Vues;

namespace TPBD2.Controlleurs
{
    class MenuPrincipaleCtrl: AbstractCtrl
    {
        public MenuPrincipaleCtrl(BDFacade facade, IIO IO):base(facade, IO)
        {}

        public void Index()
        {
            MenuPrincipaleVue menuPrincipale = new MenuPrincipaleVue(_facade, _io);
            int choix; 
            do
            {
                choix = menuPrincipale.Index();

                if (choix != 0)
                {
                    switch (choix)
                    {
                        case 1:
                            AnimalIndexCtrl animalCtrl = new AnimalIndexCtrl(_facade, _io);
                            animalCtrl.Index();
                            break;
                        case 2:

                            // Requete2(context);
                            break;
                        case 4:
                            EspeceIndexCtrl especeCtrl = new EspeceIndexCtrl(_facade, _io);
                            especeCtrl.Index();
                            break;
                        default:
                            break;
                    }
                }
            } while (choix != 0);
        }
    }
}
