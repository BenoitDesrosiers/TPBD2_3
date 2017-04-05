using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPBD2
{
    class View
    {
        //TODO: changer les public pour protected
        //TODO: factoriser les fonctions avec une lambda si possible

        public void AfficheListe(List<string> lignesMenu)
        {
            foreach(string ligne in lignesMenu)
            {
                Console.WriteLine(ligne);
            }
        }

        /// <summary>
        /// Permet de choisir un valeur à partir d'une liste de valeur 
        /// </summary>
        /// <param name="choixValides">la liste des valeurs pouvant être choisies</param>
        /// <param name="defaut">la valeur qui sera prise si l'usager appui sur return</param>
        /// <param name="question">la question qui est posée</param>
        /// <returns></returns>
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
            } while (!choixValides.Contains(reponse[0]));

            return reponse[0];
        }

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
