using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TPBD2.IO;
using TPBD2.Vues;
using TPBD2.Facade;

namespace TPBD2.Controlleurs
{
    class EspeceCRUDCtrl : AbstractCRUDCtrl
    {
        
        public EspeceCRUDCtrl(BDFacade facade, IIO io):base(facade, io)
        { }


        //
        // CRUD
        //

        /// <summary>
        /// Ajout d'une espece  
        /// </summary>
        override public void Ajout()
        {
            EspeceCRUDVue view = new EspeceCRUDVue(_facade, _io);
            Espece nouvelEspece = view.Creer();
            if ( nouvelEspece != null)
            {
                _facade.SauvegardeEspece(nouvelEspece);
            }

        }            

        /// <summary>
        /// Effacement d'un Espece
        /// [répond à la question 1d pour les proprietaire
        /// </summary>
        override public void Effacer()
        {
            EspeceCRUDVue view = new EspeceCRUDVue(_facade, _io);
            Espece espece = view.Effacer();

            if (espece != null)
                {
                    _facade.EffaceEspece(espece);
                }

        }

        override public void Modifier()
        {
            EspeceCRUDVue view = new EspeceCRUDVue(_facade, _io);
            Espece especeModifie = view.Modifier();
            if (especeModifie != null)
            {
                _facade.SaveChanges();
            }
        }

        public override void Afficher()
        {
            EspeceCRUDVue view = new EspeceCRUDVue(_facade, _io);
            view.Afficher();
        }

       
       
    }
}
