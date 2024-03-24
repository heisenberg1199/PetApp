using PetMan.Framework;
using PetMan.Models;

namespace PetMan.View
{
    public class PetUpdateView : ViewBase<Pet>
    {
        public PetUpdateView(Pet model) : base(model) { }
      
        public override void Render()
        {
          
            ViewHelp.WriteLine("PET UPDATE INFORMATION!", ConsoleColor.Green);

            var model = Model as Pet;

            var nickname = ViewHelp.InputString("Nickname", model.NickName);
            var age = ViewHelp.InputInt("Age", model.Age);
            var species = ViewHelp.InputString("Species[dog/cat]", model.Species.ToString());
            var physicalDescription = ViewHelp.InputString("Physical Description", model.PhysicalDescription);
            var personalityDescription = ViewHelp.InputString("Personality Description", model.PersonalityDescription);
            var image = ViewHelp.InputString("Image", model.Image);
            var adopt = ViewHelp.InputBool("Adopt[y/n]", model.Adopt);
            
            var request = $"pet update ? " +
                $"id = {Model.Id}" +
                $" & nickname = {nickname}" + 
                $" & age = {age}" + 
                $" & species = {species}" + 
                $" & physicalDescription = {physicalDescription}" + 
                $" & personalityDescription = {personalityDescription}" + 
                $" & image = {image}" + 
                $" & adopt = {adopt}";
            
            Router.Forward(request);
        }
    }
}