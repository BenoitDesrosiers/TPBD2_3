using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TPBD2.Controlleurs;
using TPBD2.IO;
using TPBD2.Facade;

namespace TPBD2
{
    class Program
    {
        static void Main(string[] args)
        {
            IOconsole io = new IOconsole();
            TPBD2e7654321Entities context = new TPBD2e7654321Entities();
            BDFacade bdFacade = new BDFacade(context);

            using (context)
            {
                MenuPrincipaleCtrl menuPrincipale = new MenuPrincipaleCtrl(bdFacade, io);
                menuPrincipale.Index();
            }
        }
    }
}

