using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TPBD2
{
    class Program
    {
        static void Main(string[] args)
        {

            List<String> optionsMenu = new List<string>();

            optionsMenu.Add("1) Gestion des Animaux");
            optionsMenu.Add("2) Gestion des Propriétaires");
            optionsMenu.Add("3) Gestion des Médicaments");
            optionsMenu.Add("4) Gestion des ");
            optionsMenu.Add("5) Agrégation ");
            optionsMenu.Add("0) sortir");

            View menu = new View();
            int choix;

            do
            {
                menu.AfficheListe(optionsMenu);
                choix = menu.ChoisirOption(new List<int> { 0, 1, 2, 3, 4, 5 });
                if (choix != 0)
                {
                    
                    TPBD2e7654321Entities context = new TPBD2e7654321Entities();
                    using (context)
                    {
                        switch (choix)
                        {
                            case 1:
                                AnimalCtrl animalCtrl = new AnimalCtrl(context);
                                animalCtrl.Index();
                                break;
                            case '2':
                                
                                // Requete2(context);
                                break;
                            case '4':
                                //Requete4(context);
                                break;
                            default:
                                break;
                        }
                    }
                }
            } while (choix != 0);
            }
        }

      
       

    }

