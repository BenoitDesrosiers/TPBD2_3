using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TPBD2.Vues;
using TPBD2.IO;
using TPBD2.Facade;

namespace TPBD2.Controlleurs
{
    class EspeceIndexCtrl:AbstractCtrl
    {
        public EspeceIndexCtrl(BDFacade facade, IIO io):base(facade, io)
        { }


        public void Index()
        {
            
            EspeceCRUDCtrl especeCRUDctrl = new EspeceCRUDCtrl(_facade,_io);
            EspeceRapportCtrl animalRapportCtrl = new EspeceRapportCtrl(_facade, _io);

            EspeceIndexVue menu = new EspeceIndexVue(_facade, _io);
            int choix;

            do
            {
                choix = menu.Index();
                if (choix != 0)
                {
                    switch (choix)
                    {
                        case 1:
                            especeCRUDctrl.Ajout();
                            break;
                        case 2:
                            especeCRUDctrl.Effacer();
                            break;
                        case 3:
                            especeCRUDctrl.Modifier();
                            break;
                        case 4:
                            especeCRUDctrl.Afficher();
                            break;


                    }
                }
            } while (choix != 0);
            
        }

    }
}
