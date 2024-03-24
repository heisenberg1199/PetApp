namespace PetMan.Framework
{
    public class Parameter
    {
        private readonly Dictionary<string,string> _pairs = new Dictionary<string, string>();

        public string this[string key]
        {
            get
            {
                if (_pairs.ContainsKey(key))
                    return _pairs[key];
                else 
                    return null;
            }
            set { _pairs[key] = value; }
        }
        public bool ContainsKey(string key)
        {
            return _pairs.ContainsKey(key);
        }
        public Parameter(string parameter)
        {
            var pairs = parameter.Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var pair in pairs)
            {
                var p = pair.Split('=');
                if (p.Length == 2)
                {
                    var key = p[0].Trim();
                    var value = p[1].Trim();
                    _pairs[key] = value;
                }
            }
        }
    }
}