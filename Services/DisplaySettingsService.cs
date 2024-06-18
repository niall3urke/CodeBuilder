using Blazored.LocalStorage;
using System.Text.Json.Serialization;

namespace CodeBuilder.Services
{
    public class DisplaySettingsService
    {

        // Properties

        [JsonIgnore]
        public Action Update { get; set; } = delegate () { };

        public bool Expand { get; set; }

        // Fields

        private readonly ILocalStorageService _localStorage;

        // Constructors

        public DisplaySettingsService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public DisplaySettingsService()
        {
            // For serialization
        }

        // Methods - public

        public async Task Save()
        {
            await _localStorage.SetItemAsync("display.settings", this);
            Update.Invoke();
        }

        public async Task<bool> Load()
        {
            var data = await _localStorage.GetItemAsync<DisplaySettingsService>("display.settings");

            if (data == null)
                return false;

            Expand = data.Expand;

            return true;
        }


    }
}
