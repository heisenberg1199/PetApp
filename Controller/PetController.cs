using PetMan.Data;
using PetMan.View;
using PetMan.Framework;
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
        public void List(string path = "")
        {
            var pet = Repository.Select();
            Render(new PetListView(pet), path);
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
        
        public void Delete(int id, bool process = false)
        {
            if (process == false)
            {
                var pet = Repository.Select(id);
                Confirm($"Do you want to delete this pet ({pet.NickName}) ?", $"pet delete ? id = {pet.Id}");
            }
            Repository.DeletePet(id);
            Success("Pet deleted!");
        }
        public void Filter(string key)
        {
            var model = Repository.Select(key);
            if (model.Length == 0)
            {
                Info("No match pet found!");
                return;
            }
            Render(new PetListView(model));
        }

        public void Sort(string key)
        {
            var pet = Repository.Select();
            key = key.ToLower();
            if (key == "age")
            {
                Array.Sort(pet, delegate(Pet x, Pet y) { return x.Age.CompareTo(y.Age); });
                Render(new PetListView(pet));
                return;
            }
            if (key == "name")
            {
                Array.Sort(pet, delegate(Pet x, Pet y) { return x.NickName.CompareTo(y.NickName); });
                Render(new PetListView(pet));
                return;
            }
        }
    }
}