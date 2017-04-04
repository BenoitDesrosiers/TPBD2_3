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

        public void AfficheListe(List<string> lignesMenu)
        {
            foreach(string ligne in lignesMenu)
            {
                Console.WriteLine(ligne);
            }
        }

        public  int ChoisirOption(List<int> choixValides, string question = "Votre choix: ")
        {
            int choix;

            do
            {
                Console.Write(question);
                string c_choix = Console.ReadLine();
                if (!Int32.TryParse(c_choix, out choix))
                { choix = choixValides.Min()-1  ; }
            } while (!choixValides.Contains(choix));

                return choix;

        }


        public string InputString(string question)
        {
            Console.Write(question);
            string reponse = Console.ReadLine();
            return reponse;
        }

        public char InputChar(string question, List<char> choixValides, bool majuscule = false, char defaut = ' ')
        {
            string c_choix;
            do
            {
                Console.Write(question+" ("+defaut+")");
                c_choix = Console.ReadLine();
                if(c_choix == "")
                    { c_choix = defaut.ToString(); }
                if(majuscule) c_choix=c_choix.ToUpper();
            } while (!choixValides.Contains(c_choix[0]));

            return c_choix[0];
        }

        public int InputInt(string question)
        {
            string reponse;
            int reponseInt;
            do
            {
                Console.Write(question);
                reponse = Console.ReadLine();
            } while (!Int32.TryParse(reponse, out reponseInt));
            return reponseInt;

        }

        public DateTime InputDate(string question, DateTime dateNaissanceDefault = default(DateTime))
        {
            string reponse;
            DateTime reponseDate = DateTime.Now;
            bool bonneDate;
            if (dateNaissanceDefault.Equals(default(DateTime)))
            {
                dateNaissanceDefault = DateTime.Now;
            }

            do
            {
                
                Console.Write(question+" ("+dateNaissanceDefault.Date+")");
                reponse = Console.ReadLine();
                bonneDate = true;
                try
                {
                    if (reponse == "")
                    {
                        reponseDate = dateNaissanceDefault;
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
