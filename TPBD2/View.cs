using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPBD2
{
    class Vue
    {
        //TODO: changer les public pour protected
        //TODO: factoriser les fonctions avec une lambda si possible

        /// <summary>
        /// Affiche une liste de chaine de caractères
        /// </summary>
        /// <param name="lignesMenu">la liste de chaine de caractères à afficher</param>
        public void AfficheListe(List<string> lignesMenu)
        {
            foreach(string ligne in lignesMenu)
            {
                Console.WriteLine(ligne);
            }
        }

        /// <summary>
        /// Permet de choisir un entier à partir d'une liste d'entier 
        /// </summary>
        /// <param name="choixValides">la liste des entiers pouvant être choisies</param>
        /// <param name="defaut">la valeur qui sera prise si l'usager appui sur return</param>
        /// <param name="question">la question qui est posée</param>
        /// <returns>l'entier entré</returns>
        public int ChoisirOption(List<int> choixValides, int? defaut = null, string question = "Votre choix: ")
        {
            int choix;

            do
            {
                Console.Write(question);
                if (defaut != null)
                {
                    Console.Write(" ( " + defaut + " )");
                }
                string reponse = Console.ReadLine();
                if (reponse == "" && defaut != null)
                {
                    reponse = defaut.ToString();
                }
                if (!Int32.TryParse(reponse, out choix))
                { choix = choixValides.Min()-1  ; }
            } while (!choixValides.Contains(choix));

                return choix;

        }

        /// <summary>
        /// Input d'une chaine de caractères
        /// </summary>
        /// <param name="question">la question à poser</param>
        /// <param name="defaut">la valeur par défaut si l'usager presse Return</param>
        /// <returns>la chaine de caractère entrée</returns>
        public string InputString(string question, string defaut = null)
        {
            Console.Write(question);
            if(defaut != null)
            {
                Console.Write(" ( " + defaut + " )");
            }
            string reponse = Console.ReadLine();
            if(reponse=="" && defaut != null )
            {
                reponse = defaut;
            }

            return reponse;
        }

        /// <summary>
        /// Input d'un caractère dans une liste
        /// </summary>
        /// <param name="question">La question à poser</param>
        /// <param name="choixValides">les caractères valides</param>
        /// <param name="majuscule">si vrai, un caractère minuscule en entré serra mis en majuscule </param>
        /// <param name="defaut">la valeur par défaut si l'usager presse Return</param>
        /// <returns>le caractère entré</returns>
        public char InputChar(string question, List<char> choixValides, bool majuscule = false, char? defaut = null)
        {
            string reponse;
            do
            {
                Console.Write(question);
                if (defaut != null)
                {
                    Console.Write(" ( " + defaut + " )");
                }
                reponse = Console.ReadLine();
                if(reponse == "" && defaut != null)
                    { reponse = defaut.ToString(); }
                if(majuscule) reponse=reponse.ToUpper();
            } while (reponse =="" || !choixValides.Contains(reponse[0]));

            return reponse[0];
        }

        /// <summary>
        /// Input d'un entier
        /// </summary>
        /// <param name="question">La question</param>
        /// <param name="defaut">la valeur par défaut si l'usager presse Return</param>
        /// <returns>l'entier entré</returns>
        public int InputInt(string question, int? defaut = null)
        {
            string reponse;
            int reponseInt;
            do
            {
                Console.Write(question);
                if (defaut != null)
                {
                    Console.Write(" ( " + defaut + " )");
                }
                reponse = Console.ReadLine();
                if (reponse == "" && defaut != null)
                {
                    reponse = defaut.ToString();
                }
            } while (!Int32.TryParse(reponse, out reponseInt));
            return reponseInt;

        }

        /// <summary>
        /// Input d'un date
        /// </summary>
        /// <param name="question">La question</param>
        /// <param name="defaut">la valeur par défaut si l'usager presse Return</param>
        /// <returns>la date entrée</returns>
        public DateTime InputDate(string question, DateTime? defaut = null)
        {
            string reponse;
            DateTime reponseDate = DateTime.Now;
            bool bonneDate;
            if (defaut == null)
            {
                defaut = DateTime.Now;
            }

            do
            {
                
                Console.Write(question+" ( "+defaut.Value.Date+" )");
                reponse = Console.ReadLine();
                bonneDate = true;
                try
                {
                    if (reponse == "")
                    {
                        reponseDate = defaut.Value;
                    }
                    else
                    {
                        reponseDate = Convert.ToDateTime(reponse);
                    }
                }
                catch (Exception)
                {
                    bonneDate = false;
                }
            } while (!bonneDate);
            return reponseDate;
        }
    }
}
