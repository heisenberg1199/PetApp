

using System.Linq.Expressions;
using System.Threading.Tasks.Dataflow;
using PetMan.Controller;
using PetMan.Data;
using PetMan.Framework;
using PetMan.Models;


DataAccess data = new DataAccess();
PetController controller = new PetController(data);

Router r = Router.Instance;
r.Register(route: "single", action: p => controller.Single(Extension.ToInt(p["id"])), help: "[single ? id = <value>]\rDislay Pet information by ID!");
r.Register(route: "list", action: p => controller.List(), help: "[list]\rDisplay all Pet!");
r.Register(route: "create", action: p => controller.Create(), help: "[create]\rAdd a new Pet!");
r.Register(route: "pet create", action: p => controller.Create(toPet(p)), help: "only use in this code");
r.Register(route: "update", action: p => controller.Update(Extension.ToInt(p["id"])), help: "[update ? id = <value>]\rUpdate Pet information by ID!");
r.Register(route: "pet update", action: p => controller.Update(Extension.ToInt(p["id"]), toPet(p)), help: "only use in this code");
r.Register(route: "about", About);
r.Register(route: "help", Help);

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

while (true)
{
    ViewHelp.Write("Command >>>", ConsoleColor.Green);
    string request = Console.ReadLine() ?? "";
    Router.Instance.Forward(request.ToLower().Trim());
}

void About(Parameter parameter)
{
    ViewHelp.WriteLine("PETHOME APP @version 1.0.0");
    ViewHelp.WriteLine("DESCRIPTION: PROJECT FIND A NEW HOME FOR ABANDONED DOG AND CAT!", ConsoleColor.Cyan);
    ViewHelp.WriteLine("@made by heisenberg1199", ConsoleColor.Red);
}

void Help(Parameter parameter)
{
    if (parameter == null)
    {
        ViewHelp.WriteLine("SUPPORT COMMAND!", ConsoleColor.Green);
        ViewHelp.WriteLine(Router.Instance.GetRoutes(), ConsoleColor.Yellow);
        ViewHelp.WriteLine("type: [help ? cmd = <command>] to get command details!", ConsoleColor.Cyan);
        return;
    }
    Console.BackgroundColor = ConsoleColor.Blue;
    var command = parameter["cmd"].ToLower();
    ViewHelp.WriteLine(Router.Instance.GetHelp(command));
}