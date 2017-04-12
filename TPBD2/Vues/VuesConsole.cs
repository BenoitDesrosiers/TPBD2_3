using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TPBD2.Controlleurs;


namespace TPBD2.Vues
{
    abstract class VueConsole
    {
        protected TPBD2e7654321Entities _context;
        protected AnimalCtrl _ctrl;
        protected IIO _io;

        public VueConsole(TPBD2e7654321Entities context, AnimalCtrl ctrl, IIO IO)
        {
            _context = context;
            _ctrl = ctrl;
            _io = IO;

        }

        


    }
}
