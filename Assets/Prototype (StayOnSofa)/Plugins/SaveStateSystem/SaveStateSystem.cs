using System.Collections.Generic;
using System.Globalization;
using Dialogue;

namespace SaveState
{
    public class SaveStateSystem : MonoSingleton<SaveStateSystem>
    {
        private Dictionary<string, bool> _dictionary = new();

        public bool GetState(string value)
        {
            if (_dictionary.ContainsKey(value))
                return _dictionary[value];

            return false;
        }

        public void SaveState(string key, bool value)
        {
            if (_dictionary.ContainsKey(key))
                _dictionary[key] = value;
            else
                _dictionary.Add(key, value);
        }

        public bool GetState(int value) => GetState(value.ToString());
        public bool GetState(float value) => GetState(value.ToString(CultureInfo.InvariantCulture));

        public void SaveState(int key, bool value) => SaveState(key.ToString(), value);
        public void SaveState(float key, bool value) => SaveState(key.ToString(CultureInfo.InvariantCulture), value);
    }
}