using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TPBD2.IO;

namespace TPBD2.Controlleurs
{
    class AbstractCtrl
    {
        protected TPBD2e7654321Entities _context;
        protected IIO _io;

        public AbstractCtrl(TPBD2e7654321Entities context, IIO IO)
        {
            _context = context;
            _io = IO;
        }
    }
}
