using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TPBD2.IO;
using TPBD2.Facade;

namespace TPBD2.Vues
{
    //TODO: pourquoi cette classe est abstraite? elle n'a pas de méthode abstract. 
    abstract class AbstractVueConsole
    {
        protected BDFacade _bdFacade;
        protected IIO _io;

        public AbstractVueConsole(BDFacade facade,  IIO IO)
        {
            _bdFacade = facade;
            _io = IO;

        }
    }
}
