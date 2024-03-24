using PetMan.Framework;

namespace PetMan.View
{
    public class PetCreateView : ViewBase
    {
        public PetCreateView() { }

        public override void Render() 
        {
            ViewHelp.WriteLine("ADD A NEW PET!", ConsoleColor.Green);
            
            var nickname = ViewHelp.InputString("Nickname", "");
            var age = ViewHelp.InputInt("Age", 1);
            var species = ViewHelp.InputString("Species[dog/cat]", "");
            var physicalDescription = ViewHelp.InputString("Physical Description", "");
            var personalityDescription = ViewHelp.InputString("Personality Description", "");
            var image = ViewHelp.InputString("Image", "");
            var adopt = ViewHelp.InputBool("Adopt[y/n]", false);

            var request = $"pet create ? " +
                $"nickname = {nickname}" + 
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