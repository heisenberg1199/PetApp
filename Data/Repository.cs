using PetMan.Models;

namespace PetMan.Data
{
    public class Repository
    {
        private readonly DataAccess _context;
        public Repository(DataAccess context)
        {
            _context = context;
            _context.Load();
        }
        public List<Pet> Pets => _context.Pets;
        public Pet[] Select() => _context.Pets.ToArray();
        public Pet Select(int id)
        {
            foreach (var p in Pets)
            {
                if (p.Id == id) return p;
            }
            return null;
        }
        public Pet[] Select(string key)
        {
            var temp = new List<Pet> ();
            var search = key.ToLower();
            foreach (var p in _context.Pets)
            {
                if (p.NickName.ToLower().Contains(search) || 
                    p.Age.ToString().Contains(search) ||
                    p.Species.ToString().ToLower().Contains(search)
                    )
                {
                    temp.Add(p);
                }
            }
            return temp.ToArray();
        }
        public void InsertPet(Pet pet)
        {
            int lastIndex = _context.Pets.Count() - 1;
            var id = lastIndex < 0 ? 1 : _context.Pets[lastIndex].Id+ 1;
            pet.Id = id;
            _context.Pets.Add(pet);
        }
        public bool UpdatePet(int id, Pet pet)
        {
            var p = Select(id);
            if (p == null) return false;
            p.NickName = pet.NickName;
            p.Age = pet.Age;
            p.Species = pet.Species;
            p.PhysicalDescription = pet.PhysicalDescription;
            p.PersonalityDescription = pet.PersonalityDescription;
            p.Adopt = p.Adopt;
            p.Image = p.Image;
            return true;
        }

        public bool DeletePet(int id)
        {
            var p = Select(id);
            if (p == null) return false;
            _context.Pets.Remove(p);
            return true;
        }
    }
}