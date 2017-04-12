using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TPBD2.Vues;

namespace TPBD2.Controlleurs
{
    class AnimalRapportCtrl: AbstractCtrl
    {
        public AnimalRapportCtrl(TPBD2e7654321Entities context, IIO io):base(context,io)
        { }
        /// <summary>
        /// affiche le contenu de la table Animal et la quantié de soins qu'ils ont recu
        /// [répond à la question 4 a et b]
        /// </summary>
        public void RapportNombreSoin()
        {
            AnimalRapportVue view = new AnimalRapportVue(_context, this, _io);
            view.RapportNombreSoin();
        }

        /// <summary>
        /// Liste un animal et ses propriétaires
        
        /// </summary>
        public void ListeProprietaires()
        {
            AnimalRapportVue view = new AnimalRapportVue(_context, this, _io);
            view.Afficher();
        }

    }
}
