using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TPBD2.IO;
using TPBD2.Facade;

namespace TPBD2.Controlleurs
{
    class AbstractCtrl
    {
        protected BDFacade _facade;
        protected IIO _io;

        public AbstractCtrl(BDFacade facade, IIO IO)
        {
            _facade = facade;
            _io = IO;
        }
    }
}
