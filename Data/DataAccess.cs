using PetMan.Models;

namespace PetMan.Data
{
    public class DataAccess
    {
        public List<Pet> Pets { get; set; }
        public void Load()
        {
            Pets = new List<Pet>
            {
                new Pet {
                    Id=1, 
                    NickName="Lola", 
                    Species=Species.dog, 
                    PhysicalDescription="medium sized cream colored female golden retriever weighing about 65 puntds. housebroken.",
                    PersonalityDescription="love to have her belly rubbed and likes to chase her tail. gives lots or kisses"
                },
                new Pet {
                    Id=2, 
                    NickName="Loki", 
                    Species=Species.dog, 
                    PhysicalDescription="larged reddish-brown male golden retriever weighing about 85 pounds. housebroken.",
                    PersonalityDescription="love to have his ears rubbed when he greets you at the door, or at any time! loves to lean-in and give doggy hugs."
                },
                new Pet {
                    Id=3, 
                    NickName="Puss", 
                    Species=Species.cat, 
                    PhysicalDescription="small white female weighing about 8 pounds. litter box trained.",
                    PersonalityDescription="friendly"
                },
            };
        }
        public void SaveChanges() { }
    }
}