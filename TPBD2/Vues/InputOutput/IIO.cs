using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPBD2.IO
{
    
    /// <summary>
    /// Interface pour les entrés/sortis
    /// </summary>
    interface IIO
    {

        /// <summary>
        /// Affiche une chaine de caractères
        /// Note: aucun formatage n'est fait
        /// </summary>
        /// <param name="texte">le texte à afficher</param>
        void AfficheTexte(string texte);

        /// <summary>
        /// Affiche une liste de string
        /// </summary>
        /// <param name="lignesMenu"></param>
        void AfficheListe(List<string> lignesMenu);

        /// <summary>
        /// Permet de choisir un entier à partir d'une liste d'entier 
        /// </summary>
        /// <param name="choixValides">la liste des entiers pouvant être choisies</param>
        /// <param name="defaut">la valeur qui sera prise si l'usager appui sur return</param>
        /// <param name="question">la question qui est posée</param>
        /// <returns>l'entier entré</returns>
        int ChoisirOption(List<int> choixValides, int? defaut = null, string question = "Votre choix: ");

        /// <summary>
        /// Attend une touche pour continuer
        /// </summary>
        /// <param name="question"></param>
        void AttendreTouche(string question);

        /// <summary>
        /// Input d'une chaine de caractères
        /// </summary>
        /// <param name="question">la question à poser</param>
        /// <param name="defaut">la valeur par défaut si l'usager presse Return</param>
        /// <returns>la chaine de caractère entrée</returns>
        string InputString(string question, string defaut = null);

        /// <summary>
        /// Input d'un caractère dans une liste
        /// </summary>
        /// <param name="question">La question à poser</param>
        /// <param name="choixValides">les caractères valides</param>
        /// <param name="majuscule">si vrai, un caractère minuscule en entré serra mis en majuscule </param>
        /// <param name="defaut">la valeur par défaut si l'usager presse Return</param>
        /// <returns>le caractère entré</returns>
        char InputChar(string question, List<char> choixValides, bool majuscule = false, char? defaut = null);


        /// <summary>
        /// Input d'un entier
        /// </summary>
        /// <param name="question">La question</param>
        /// <param name="defaut">la valeur par défaut si l'usager presse Return</param>
        /// <returns>l'entier entré</returns>
        int InputInt(string question, int? defaut = null);


        /// <summary>
        /// Input d'un date
        /// </summary>
        /// <param name="question">La question</param>
        /// <param name="defaut">la valeur par défaut si l'usager presse Return</param>
        /// <returns>la date entrée</returns>
        DateTime InputDate(string question, DateTime? defaut = null);
    }
}
