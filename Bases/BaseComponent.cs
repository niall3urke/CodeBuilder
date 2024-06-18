using CodeBuilder.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CodeBuilder.Code;
using System.Globalization;
using CodeBuilder.Services.Modal;
using NuGet.Configuration;
using CodeBuilder.Services;

namespace CodeBuilder.Bases
{
    public class BaseComponent : ComponentBase
    {
        // ==============
        // ===== Injects
        // ==============

        [Inject]
        protected IDbContextFactory<ApplicationDbContext> ContextFactory { get; set; }

        [Inject]
        protected AuthenticationStateProvider Auth { get; set; }

        [Inject]
        protected DisplaySettingsService Settings { get; set; }

        [Inject]
        protected NavigationManager Nav { get; set; }

        [Inject]
        public ModalService Modal { get; set; }

        // =============
        // ===== Fields
        // =============

        protected bool _loading;

        // =============
        // ===== Events
        // =============

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await Settings.Load();
            }
        }

        // ========================================
        // ===== Methods: parsing data from inputs
        // ========================================

        protected bool GetValue<T>(ChangeEventArgs e, out T value)
        {
            if (typeof(T) == typeof(string) && GetValue(e, out string valueAsString))
            {
                value = (T)(object)valueAsString;
                return true;
            }

            if (typeof(T) == typeof(int) && GetValue(e, out int valueAsInt))
            {
                value = (T)(object)valueAsInt;
                return true;
            }

            if (typeof(T) == typeof(double) && GetValue(e, out double valueAsDouble))
            {
                value = (T)(object)valueAsDouble;
                return true;
            }

            if (typeof(T) == typeof(bool) && GetValue(e, out bool valueAsBool))
            {
                value = (T)(object)valueAsBool;
                return true;
            }

            if (typeof(T) == typeof(DateTime) && GetValue(e, out DateTime valueAsDateTime))
            {
                value = (T)(object)valueAsDateTime;
                return true;
            }

            if (typeof(T).IsEnum())
            {
                if (!EventIsNull(e))
                {
                    try
                    {
                        value = (T)Enum.Parse(typeof(T), e.Value.ToString());
                        return true;
                    }
                    catch { }
                }
            }

            value = default;
            return false;
        }

        protected bool GetValue(ChangeEventArgs e, out DateTime value)
        {
            value = default;

            if (EventIsNull(e))
                return false;

            string text = e.Value.ToString();

            if (string.IsNullOrWhiteSpace(text))
                return false;

            return DateTime.TryParse(text, out value);
        }

        protected bool GetEnumValue<T>(ChangeEventArgs e, out T value) where T : Enum
        {
            value = default;

            if (EventIsNull(e))
                return false;

            try
            {
                value = (T)Enum.Parse(typeof(T), e.Value.ToString());
            }
            catch { return false; }

            return true;
        }

        protected bool GetValue(ChangeEventArgs e, out string value)
        {
            value = "";

            if (EventIsNull(e))
                return false;

            value = e.Value.ToString();

            // We don't use is null or whitespace because we
            // want to allow users to empty the values for
            // most string inputs.

            return value != null;
        }

        protected bool GetValue(ChangeEventArgs e, out bool value)
        {
            value = false;

            if (EventIsNull(e))
                return false;

            string text = e.Value.ToString();

            if (string.IsNullOrWhiteSpace(text))
                return false;

            return bool.TryParse(text, out value);
        }

        protected bool GetValue(ChangeEventArgs e, out int value)
        {
            value = 0;

            if (EventIsNull(e))
                return false;

            string text = e.Value.ToString();

            if (string.IsNullOrWhiteSpace(text))
                return true; // Return early as 0

            return int.TryParse(text, out value);
        }

        protected bool GetValue(ChangeEventArgs e, out double value)
        {
            value = 0;

            if (EventIsNull(e))
                return false;

            string text = e.Value.ToString();

            if (string.IsNullOrWhiteSpace(text))
                return true; // Return early as 0

            if (text.StartsWith("£") || text.StartsWith("$") || text.StartsWith("€"))
            {
                return double.TryParse(text.Substring(1), NumberStyles.Currency, CultureInfo.InvariantCulture, out value);
            }

            return double.TryParse(text, out value);
        }

        protected bool EventIsNull(ChangeEventArgs e)
        {
            if (e == null || e.Value == null)
                return true;

            return false;
        }

        protected string Format(double value)
        {
            if (value == 0)
                return "0";

            return value.ToString("#.00E+0");
        }

        protected async Task ToggleExpand()
        {
            Settings.Expand = !Settings.Expand;
            await Settings.Save();
        }

    }
}
