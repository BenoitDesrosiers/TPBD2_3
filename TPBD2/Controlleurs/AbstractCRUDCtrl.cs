using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TPBD2.Vues;

namespace TPBD2.Controlleurs
{
    abstract class AbstractCRUDCtrl: AbstractCtrl, ICRUDControlleur
    {
        public AbstractCRUDCtrl(TPBD2e7654321Entities context, IIO IO):base(context, IO)
        { }

        abstract public void Ajout();

        abstract public void Effacer();

        abstract public void Modifier();

        abstract public void Afficher();
    }
}
