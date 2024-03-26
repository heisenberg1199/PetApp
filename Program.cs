using PetMan.Framework;

namespace PetMan
{
    public partial class Program
    {
        public static void Main(string[] args)
        {
            // Console.Clear();
            Config();
            while (true)
            {
                ViewHelp.Write("Command >>>", ConsoleColor.Green);
                string request = Console.ReadLine() ?? "";
                Router.Instance.Forward(request.ToLower().Trim());
            }
        }
        public static void About(Parameter parameter)
        {
            ViewHelp.WriteLine("PETHOME APP @version 1.0.0");
            ViewHelp.WriteLine("DESCRIPTION: PROJECT FIND A NEW HOME FOR ABANDONED DOG AND CAT!", ConsoleColor.Cyan);
            ViewHelp.WriteLine("@made by heisenberg1199", ConsoleColor.Red);
        }

        public static void Help(Parameter parameter)
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
    }
}