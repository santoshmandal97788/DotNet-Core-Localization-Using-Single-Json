using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System.Globalization;
using System.Text;

namespace LocalizationUsingSingleJsonFile
{
    public class JsonStringLocalizer : IStringLocalizer
    {
        private readonly Dictionary<string, Dictionary<string, string>> _resources;

        public JsonStringLocalizer()
        {
            string projectFolder = AppDomain.CurrentDomain.BaseDirectory;
            string scriptFolder = Path.Combine(projectFolder, "Resources");
            var resourceFile = Path.Combine(scriptFolder, "LargeDataResources.json");
            //var resourceFile = Path.Combine(scriptFolder, "Resources.json");
            if (File.Exists(resourceFile))
            {
                var json = File.ReadAllText(resourceFile, Encoding.UTF8);
                try
                {
                    _resources = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(json);

                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        public LocalizedString this[string name]
        {
            get
            {
                //1st approach
                //try
                //{
                //    var currentCulture = CultureInfo.CurrentCulture.Name;

                //    if (_resources.ContainsKey(name))
                //    {
                //        var value = _resources[name].GetValueOrDefault(currentCulture, _resources[name]["en-US"]);
                //        return new LocalizedString(name, value ?? name, true);
                //    }
                //    return new LocalizedString(name, name, true);
                //}
                //catch (Exception ex)
                //{
                //    return new LocalizedString(name, name, true);
                //}
                //2nd approach

                var currentCulture = CultureInfo.CurrentCulture.Name;

                // Check if the current culture exists in the resource dictionary
                if (_resources.ContainsKey(name))
                {
                    if (_resources[name].ContainsKey(currentCulture))
                    {
                        return new LocalizedString(name, _resources[name][currentCulture], true);
                    }
                    else
                    {
                        return new LocalizedString(name, name, true);
                    }
                }
                else
                {
                    return new LocalizedString(name, name, true);
                }
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var format = this[name]?.Value;
                return new LocalizedString(name, string.Format(format, arguments), true);
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            return _resources.Keys.Select(key => new LocalizedString(key, _resources[key]["en"], true));
        }

        public IStringLocalizer WithCulture(System.Globalization.CultureInfo culture)
        {
            return this;
        }
    }
}
