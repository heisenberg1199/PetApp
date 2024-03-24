using System.Collections.Concurrent;
using System.Text.Json;

namespace PetMan.Framework
{
    public class ViewBase
    {
        protected Router Router = Router.Instance;
        public ViewBase() { }
        public virtual void Render() { }
    }

    public class ViewBase<T> : ViewBase
    {
        protected T Model;
        public ViewBase(T model) => Model = model;
        public virtual void WriteToFileJson(string path)
        {
            ViewHelp.WriteLine($"Saving data to file {path}", ConsoleColor.Green);
            var jsonString = JsonSerializer.Serialize(Model);
            File.WriteAllText(path, jsonString);
            ViewHelp.WriteLine("Done!", ConsoleColor.Cyan);
        }
    }
}