using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TPBD2.IO;
using TPBD2.Facade;

namespace TPBD2.Vues
{
    /// <summary>
    /// Permet de choisir un espece parmi ceux existants.
    /// </summary>
    class EspeceSelectionVue: AbstractVueConsole
    {

        public EspeceSelectionVue(BDFacade facade, IIO IO): base(facade, IO)
        {
        }

        //
        // Helpers
        //
        /// <summary>
        /// Permet de sélectionner une espèce dans la liste complète
        /// </summary>
        /// <param name="optionAnnuler">Si vrai, ajoute l'option 0 pour annuler</param>
        /// <returns>l'id de l'espèce choisi ou 0</returns>
        public int ChoisirEspece(bool optionAnnuler = false)
        {

            IQueryable<Espece> especes = _bdFacade.Especes();
            List<string> especesMenu = new List<string>();
            List<int> especesIdValide = new List<int>();

            foreach (Espece espece in especes)
            {
                especesMenu.Add(string.Format("id: {0} nom: {1}", espece.ID, espece.Nom));
                especesIdValide.Add(espece.ID);
            }
            if (optionAnnuler)
            {
                especesMenu.Add("0 pour annuler");
                especesIdValide.Add(0);
            }
            _io.AfficheListe(especesMenu);

            return _io.ChoisirOption(especesIdValide);


        }
    }
}
