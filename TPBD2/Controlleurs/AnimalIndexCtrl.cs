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
    class AnimalIndexCtrl:AbstractCtrl
    {
        public AnimalIndexCtrl(BDFacade facade, IIO io):base(facade, io)
        { }


        public void Index()
        {
            
            AnimalCRUDCtrl animalCRUDctrl = new AnimalCRUDCtrl(_facade,_io);
            AnimalRapportCtrl animalRapportCtrl = new AnimalRapportCtrl(_facade, _io);

            AnimalIndexVue menu = new AnimalIndexVue(_facade, animalCRUDctrl, animalRapportCtrl, _io);
            menu.Index();
        }

    }
}
