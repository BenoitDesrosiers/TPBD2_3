using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TPBD2.Vues;
using TPBD2.IO;
using TPBD2.Facade;


// TODO: à enlever probablement car il n'y a pas de rapport pour les espèces



namespace TPBD2.Controlleurs
{
    class EspeceRapportCtrl: AbstractCtrl
    {
        public EspeceRapportCtrl(BDFacade facade, IIO io):base(facade,io)
        { }
       /*
        /// <summary>
        /// affiche le contenu de la table Animal et la quantié de soins qu'ils ont recu
        /// [répond à la question 4 a et b]
        /// </summary>
        public void RapportNombreSoin()
        {
            EspeceRapportVue view = new EspeceRapportVue(_facade, _io);
            view.RapportNombreSoin();
        }
        */
    }
}
