using System.Data.Common;
using PetMan.Framework;
using PetMan.Models;

namespace PetMan.View
{
    public class PetListView : ViewBase<Pet[]>
    {
        public PetListView(Pet[] model) : base(model) { }
      
        public override void Render()
        {
            if (Model.Length == 0)
            {
                ViewHelp.WriteLine("NO PET LIST FOUND!", ConsoleColor.Magenta);
                return;
            }

            ViewHelp.WriteLine("PET LIST!", ConsoleColor.Green);
            ViewHelp.WriteLine("#ID\t\tNickName\tSpecies");
            for (int i = 0; i < Model.Length; i++)
            {
                ViewHelp.WriteLine($"[{Model[i].Id}]\t\t{Model[i].NickName}\t\t{Model[i].Species}", ConsoleColor.Yellow);
            }
            ViewHelp.WriteLine($"Count {Model.Length} Pet item(s)", ConsoleColor.Green);
        }
    }
}