namespace PetMan.Framework
{
    public class ControllerBase
    {
        public virtual void Render(ViewBase view) { view.Render(); }
        public virtual void Render<T> (ViewBase<T> view, string path = "") 
        { 
            if (!String.IsNullOrEmpty(path))
            {
                view.WriteToFileJson(path);
                return;
            }
            view.Render(); 
        }

        public virtual void Render(Message message) => Render(new MessageView(message));
        public virtual void Success(string text, string label = "SUCCESS") 
            => Render(new Message { Text = text, Label = label, Type = MessageType.Success });
        public virtual void Info(string text, string label = "INFORMATION") 
            => Render(new Message { Text = text, Label = label, Type = MessageType.Information });
        public virtual void Error(string text, string label = "ERROR") 
            => Render(new Message { Text = text, Label = label, Type = MessageType.Error });
        public virtual void Confirm(string text, string route, string label = "CONFIRMATION") 
            => Render(new Message { Text = text, Label = label, Type = MessageType.Confirmation, BackRoute = route });
    }
}