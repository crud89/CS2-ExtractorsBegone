using Colossal;
using crud89.ExtractorsBegone;
using System.Collections.Generic;

namespace ExtractorsBegone.Localization
{
    public class ModSettingsDefaultLocale : IDictionarySource
    {
        private ModSettings Settings { get; set; }

        private Dictionary<string, string> Entries { get; set; }

        public ModSettingsDefaultLocale(ModSettings settings)
        {
            Settings = settings;
            Entries = LoadSettingsLocale(settings);
        }

        private static Dictionary<string, string> LoadSettingsLocale(ModSettings settings)
        {
            return new Dictionary<string, string>
            {
                { settings.GetSettingsLocaleID(), "Extractors Begone" },
                { settings.GetOptionLabelLocaleID(nameof(ModSettings.DisableExtractorBuildings)), "Disable Extractor Buildings" },
                { settings.GetOptionDescLocaleID(nameof(ModSettings.DisableExtractorBuildings)), "Disables spawning new extractor buildings." },
                { settings.GetOptionLabelLocaleID(nameof(ModSettings.DisableWorkVehicles)), "Disable Work Vehicles" },
                { settings.GetOptionDescLocaleID(nameof(ModSettings.DisableWorkVehicles)), "Disables spawning new work vehicles." },
                { settings.GetOptionLabelLocaleID(nameof(ModSettings.DespawnExtractors)), "Despawn Extractors" },
                { settings.GetOptionDescLocaleID(nameof(ModSettings.DespawnExtractors)), "Despawns all existing extractor sub-buildings." },
                { settings.GetOptionWarningLocaleID(nameof(ModSettings.DespawnExtractors)), "Do you want to permanently despawn all existing extractor buildings?" },
                { settings.GetOptionLabelLocaleID(nameof(ModSettings.DespawnWorkVehicles)), "Despawn Work Vehicles" },
                { settings.GetOptionDescLocaleID(nameof(ModSettings.DespawnWorkVehicles)), "Despawns all currently active work vehicles." },
                { settings.GetOptionWarningLocaleID(nameof(ModSettings.DespawnWorkVehicles)), "Do you want to permanently despawn all existing work vehicles?" }
            };
        }

        public IEnumerable<KeyValuePair<string, string>> ReadEntries(IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts) => Entries;

        public void Unload()
        {
        }
    }
}
