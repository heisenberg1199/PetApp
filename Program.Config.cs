using PetMan.Controller;
using PetMan.Data;
using PetMan.Framework;
using PetMan.Models;

namespace PetMan
{
    public partial class Program
    {
        public static void Config()
        {
            DataAccess data = new DataAccess();
            PetController controller = new PetController(data);
            ShellController shellController = new ShellController(data);
            Router r = Router.Instance;
            r.Register(route: "single", action: p => controller.Single(Extension.ToInt(p["id"])), help: "[single ? id = <value>]\rDislay Pet information by ID!");
            r.Register(route: "list", action: p => controller.List(), help: "[list]\rDisplay all Pet!");
            r.Register(route: "create", action: p => controller.Create(), help: "[create]\rAdd a new Pet!");
            r.Register(route: "pet create", action: p => controller.Create(toPet(p)), help: "only use in this code");
            r.Register(route: "update", action: p => controller.Update(Extension.ToInt(p["id"])), help: "[update ? id = <value>]\rUpdate Pet information by ID!");
            r.Register(route: "pet update", action: p => controller.Update(Extension.ToInt(p["id"]), toPet(p)), help: "only use in this code");
            r.Register(route: "delete", action: p => controller.Delete(Extension.ToInt(p["id"])), help: "[deleted ? id = <value>]\rDelete Pet by ID!");
            r.Register(route: "pet delete", action: p => controller.Delete(Extension.ToInt(p["id"]), false), help: "only use in this code");
            r.Register(route: "filter", action: p => controller.Filter(p["key"]), help: "[filter ? key = <value>]\rSearch Pet by key (ex: nickname, age, etc.)");
            r.Register(route: "single file", action: p => controller.Single(Extension.ToInt(p["id"]), p["path"]), help:"[single file ? id = <value> & path = <value>]");
            r.Register(route: "sort", action: p => controller.Sort(p["key"]));
            r.Register(route: "about", About);
            r.Register(route: "help", Help);

            r.Register(route: "shell", action: p => shellController.Shell(p["path"]));
            r.Register(route: "report pdf", action: p => shellController.ToPdf(Extension.ToInt(p["id"]), p["path"]));
            r.Register(route: "read report", action: p => shellController.ReadReport(Extension.ToInt(p["id"])));
            r.Register(route: "clear", action: p => shellController.Clear());
            
            Pet toPet(Parameter p)
            {
                var pet = new Pet();
                if (p.ContainsKey("id")) pet.Id = Extension.ToInt(p["id"]);
                if (p.ContainsKey("nickname")) pet.NickName = p["nickname"];
                if (p.ContainsKey("age")) pet.Age = Extension.ToInt(p["age"]);
                if (p.ContainsKey("species"))
                {
                    string speciesString = p["species"];
                    if (Enum.TryParse(speciesString, out Species species))
                        pet.Species = species;
                }
                if (p.ContainsKey("physicalDescription")) pet.PhysicalDescription = p["physicalDescription"];
                if (p.ContainsKey("personalityDescription")) pet.PersonalityDescription = p["personalityDescription"];
                if (p.ContainsKey("image")) pet.Image = p["image"];
                if (p.ContainsKey("adopt")) pet.Adopt = Extension.ToBool(p["adopt"]);
                return pet;
            }
        }
    }
}