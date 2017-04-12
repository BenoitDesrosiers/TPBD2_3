using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPBD2.Controlleurs
{
    interface ICRUDControlleur
    {
        /// <summary>
        /// Menu principale
        /// </summary>
        void Index();


        /// <summary>
        /// Ajout d'une entité
        /// </summary>
        void Ajout();

        /// <summary>
        /// Effacement d'une entité
        /// </summary>
        void Effacer();

        /// <summary>
        /// Modification d'une entité
        /// </summary>
        void Modifier();

        /// <summary>
        /// Affiche une entité
        /// </summary>
        void Afficher();
    }
}
