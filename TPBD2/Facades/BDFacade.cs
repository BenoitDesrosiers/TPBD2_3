using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPBD2.Facade
{
    class BDFacade
    {
        protected TPBD2e7654321Entities _context;

        public BDFacade(TPBD2e7654321Entities context)
        {
            _context = context;
        }

        // General

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        // Animal 

        public IQueryable<Animal> Animals()
        {
            return from a in _context.Animals select a;
        }

        /// <summary>
        /// Retourne un Animal à partir de son ID
        /// Les proprietaires et l'espece sont Eager Load
        /// </summary>
        /// <param name="animalId">l'id de l'animal à charger</param>
        /// <returns>l'animal trouvé, ou null </returns>
        public Animal AnimalParID(int animalId)
        {
            return _context.Animals
                    .Include(nameof(Animal.Proprietaires))
                    .Include(nameof(Animal.Espece))
                    .Where(a => a.ID == animalId).FirstOrDefault();
        }

        public void SauvegardeAnimal(Animal animal)
        {
            _context.Animals.Add(animal);
            _context.SaveChanges();
        }

        public void EffaceAnimal(Animal animal)
        {
            _context.Animals.Remove(animal);
            _context.SaveChanges();
        }


        // Espece
        public IQueryable<Espece>  Especes()
        {
            return from e in _context.Especes select e;
        }   

        public Espece EspeceParID(int especeId)
        {
            return _context.Especes
                .Find(especeId);
        }

        public void SauvegardeEspece(Espece espece)
        {
            _context.Especes.Add(espece);
            _context.SaveChanges();
        }

        public void EffaceEspece(Espece espece)
        {
            //TODO: ajouter la gestion d'une exception s'il y a des animaux associé à cette espèce. 
            _context.Especes.Remove(espece);
            _context.SaveChanges();
        }

        // Proprietaire
        public IQueryable<Proprietaire> Proprietaires()
        {
            return from p in _context.Proprietaires select p;
        }

        public Proprietaire ProprietaireParID(int proprietaireId)
        {
            return _context.Proprietaires
                .Find(proprietaireId);
        }

        // Soins
        public IQueryable<Soin> Soins()
        {
            return from s in _context.Soins select s;
        }

        public Soin SoinPourUnAnimalEtUnMedicament(Animal animal, int medicamentId)
        {
            return (from s in _context.Soins
                    where s.Animal == animal && s.MedicamentID == medicamentId
                    select s).SingleOrDefault();
        }

        public struct StructAnimalEtCompteDeSoin
        {
            public Animal animal;
            public int compteDeSoin;

            public StructAnimalEtCompteDeSoin(Animal unAnimal, int unCompte)
            {
                animal = unAnimal;
                compteDeSoin = unCompte;
            }
        }

        public StructAnimalEtCompteDeSoin AnimalEtCompteDeSoinParRequêtes(int animalId)
        {
            var requeteAnimalEtCompte = (from a in _context.Animals
                                        let ac = new
                                        {
                                            a,
                                            compte = a.Soins.Count
                                        }
                                        where a.ID.Equals(animalId)
                                        select (ac))
                                        .FirstOrDefault();
            //TODO: gérer les null
            return new StructAnimalEtCompteDeSoin(requeteAnimalEtCompte.a, requeteAnimalEtCompte.compte);
        }

        public StructAnimalEtCompteDeSoin AnimalEtCompteDeSoinParMéthodes(int animalId)
        {
            var requeteAnimalEtCompte = (_context.Animals
                .Where(a => a.ID == animalId)
                .Select(a => new { a, compte = a.Soins.Count }))
                .FirstOrDefault();
            //TODO: gérer les null
            return new StructAnimalEtCompteDeSoin(requeteAnimalEtCompte.a, requeteAnimalEtCompte.compte);
        }

        // Medicaments
        public IQueryable<Medicament> Medicaments()
        {
            return from m in _context.Medicaments select m;
        }
          
        /// <summary>
        /// Retourne les ID des médicaments pris par un animal
        /// </summary>
        /// <param name="animal">l'animal en question</param>
        /// <returns>une liste contenant les id des médicaments </returns>
                  
        public List<int> MedicamentsIDPourUnAnimal(Animal animal)
        {
            return (from s in _context.Soins
                   where s.AnimalID == animal.ID
                   select s.MedicamentID).ToList();
        }

        /// <summary>
        /// Retourne les médicaments qui ont été prescrits à un animal
        /// </summary>
        /// <param name="animal">l'animal en question</param>
        /// <returns>les médicaments qui ont été prescrits </returns>

        public IQueryable<Medicament> MedicamentsPrescritsPourUnAnimal(Animal animal)
        {
            return Medicaments()
                .Where(m => (MedicamentsIDPourUnAnimal(animal).Contains(m.ID)));
        }

        /// <summary>
        /// Retourne les médicaments qui n'ont pas encore été prescrits à un animal
        /// </summary>
        /// <param name="animal">l'animal en question</param>
        /// <returns>les médicaments qui n'ont pas été prescrits </returns>

        public IQueryable<Medicament> MedicamentsNonPrescritsPourUnAnimal(Animal animal)
        {
            return Medicaments()
                .Where(m => !(MedicamentsIDPourUnAnimal(animal).Contains(m.ID)));
        }

        



    }
}
