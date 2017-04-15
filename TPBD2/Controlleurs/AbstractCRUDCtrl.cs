using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TPBD2.IO;
using TPBD2.Facade;

namespace TPBD2.Controlleurs
{
    abstract class AbstractCRUDCtrl: AbstractCtrl, ICRUDControlleur
    {
        public AbstractCRUDCtrl(BDFacade bdFacade, IIO IO):base(bdFacade, IO)
        { }

        abstract public void Ajout();

        abstract public void Effacer();

        abstract public void Modifier();

        abstract public void Afficher();
    }
}
