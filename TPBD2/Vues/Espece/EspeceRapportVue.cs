using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TPBD2.IO;
using TPBD2.Facade;


// TODO: obsolete
namespace TPBD2.Vues
{
    class EspeceRapportVue: AbstractVueConsole
    {

        private EspeceSelectionVue selecteur;

        public EspeceRapportVue(BDFacade facade, IIO IO):base(facade, IO)
        {
            selecteur = new EspeceSelectionVue(facade, IO);

        }

      
        
    }
}
