using PetMan.Framework;
using PetMan.Models;

namespace PetMan.View
{
    public class PetSingleView : ViewBase<Pet>
    {
        public PetSingleView(Pet model) : base(model) { }
       
        public override void Render()
        {
            if (Model == null)
            {
                ViewHelp.WriteLine("NO PET FOUND!", ConsoleColor.Red);
                return;
            }
            var model = Model as Pet;
            ViewHelp.WriteLine("PET INFORMATION!", ConsoleColor.Green);
            Console.WriteLine($"Nickname:                   {model.NickName}");
            Console.WriteLine($"Age:                        {model.Age}");
            Console.WriteLine($"Species:                    {model.Species}");
            Console.WriteLine($"Physical Description:       {model.PhysicalDescription}");
            Console.WriteLine($"Persionality Description:   {model.PersonalityDescription}");
            Console.WriteLine($"Image:                      {model.Image}");
            Console.WriteLine($"Adopt:                      {model.Adopt}");
        }
    }
}