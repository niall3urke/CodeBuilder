namespace CodeBuilder.Services.Modal
{
    public class ModalParameters
    {

        // Fields

        private readonly Dictionary<string, object> _parameters;

        // Constructors

        public ModalParameters() =>
            _parameters = new Dictionary<string, object>();

        // Methods

        public void Add(string name, object value) =>
            _parameters.Add(name, value);

        public T Get<T>(string name)
        {
            if (_parameters.TryGetValue(name, out object result))
            {
                return (T)result;
            }
            return (T)Activator.CreateInstance(typeof(T));
        }

        public bool TryGet<T>(string name, out T value)
        {
            if (_parameters.TryGetValue(name, out object result))
            {
                value = (T)result;
                return true;
            }

            value = (T)Activator.CreateInstance(typeof(T));
            return false;
        }

        public bool TryGet(string name, out string value)
        {
            if (_parameters.TryGetValue(name, out object result))
            {
                value = result.ToString();
                return true;
            }

            value = "";
            return false;
        }


    }
}
