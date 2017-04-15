using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TPBD2.Vues;
using TPBD2.IO;
using TPBD2.Facade;

namespace TPBD2.Controlleurs
{
    class AnimalRapportCtrl: AbstractCtrl
    {
        public AnimalRapportCtrl(BDFacade facade, IIO io):base(facade,io)
        { }
        /// <summary>
        /// affiche le contenu de la table Animal et la quantié de soins qu'ils ont recu
        /// [répond à la question 4 a et b]
        /// </summary>
        public void RapportNombreSoin()
        {
            AnimalRapportVue view = new AnimalRapportVue(_facade, _io);
            view.RapportNombreSoin();
        }

    }
}
