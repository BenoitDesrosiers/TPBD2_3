using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPBD2.Vues
{
    interface IVue
    {
        /// <summary>
        /// Menu principale pour gérer la classe
        /// </summary>
        void Index();

        /// <summary>
        /// Création d'un item
        /// </summary>
        /// <returns>l'item créé</returns>
       void  Creer();

        /// <summary>
        /// Destruction d'un item
        /// </summary>
        /// <returns>l'item à détruire</returns>
        Object Effacer();

        /// <summary>
        /// Modification d'un item
        /// </summary>
        /// <returns>l'item modifié</returns>
        Object Modifier();
    }
}
