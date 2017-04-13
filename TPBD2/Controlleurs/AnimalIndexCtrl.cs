using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TPBD2.Vues;

namespace TPBD2.Controlleurs
{
    class AnimalIndexCtrl:AbstractCtrl
    {
        public AnimalIndexCtrl(TPBD2e7654321Entities context, IIO io):base(context, io)
        { }


        public void Index()
        {
            AnimalCRUDCtrl animalCRUDctrl = new AnimalCRUDCtrl(_context,_io);
            AnimalRapportCtrl animalRapportCtrl = new AnimalRapportCtrl(_context, _io);

            AnimalIndexVue menu = new AnimalIndexVue(_context, animalCRUDctrl, animalRapportCtrl, _io);
            menu.Index();
        }

    }
}
