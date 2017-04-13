using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TPBD2.Controlleurs;


namespace TPBD2.Vues
{
    //TODO: pourquoi cette classe est abstraite? elle n'a pas de méthode abstract. 
    abstract class AbstractVueConsole
    {
        protected TPBD2e7654321Entities _context;
        protected IIO _io;

        public AbstractVueConsole(TPBD2e7654321Entities context,  IIO IO)
        {
            _context = context;
            _io = IO;

        }
    }
}
