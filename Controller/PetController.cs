using PetMan.Data;
using PetMan.View;
using PetMan.Framework;
using System.Collections.Concurrent;
using PetMan.Models;

namespace PetMan.Controller
{
    public class PetController : ControllerBase
    {
        protected Repository Repository;
        public PetController(DataAccess context) 
        { 
            Repository = new Repository(context);
        }
        public void Single(int id, string path = "")
        {
            var pet = Repository.Select(id);
            Render(new PetSingleView(pet), path);
        }
        public void List()
        {
            var pet = Repository.Select();
            Render(new PetListView(pet));
        }
        public void Create(Pet pet = null)
        {
            if (pet == null)
            {
                Render(new PetCreateView());
                return;
            }
            Repository.InsertPet(pet);
            Success("Pet added!");
        }

        public void Update(int id, Pet pet = null)
        {
            if (pet == null)
            {
                var p = Repository.Select(id);
                Render(new PetUpdateView(p));
                return;
            }
            Repository.UpdatePet(id, pet);
            Success("Pet updated!");
        }
    }
}