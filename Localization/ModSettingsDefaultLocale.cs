using Colossal;
using crud89.ExtractorsBegone;
using System.Collections.Generic;

namespace crud89.ExtractorsBegone.Localization
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
                { settings.GetOptionGroupLocaleID(ModSettings.ExtractorsGroupName), "Extractor Buildings" },
                { settings.GetOptionGroupLocaleID(ModSettings.WorkVehiclesGroupName), "Work Vehicles" },
                { settings.GetOptionGroupLocaleID(ModSettings.DespawnGroupName), "Despawn Existing" },

                { settings.GetOptionLabelLocaleID(nameof(ModSettings.DisableExtractorBuildings)), "Disable Extractor Buildings" },
                { settings.GetOptionDescLocaleID(nameof(ModSettings.DisableExtractorBuildings)), "Disables spawning new extractor buildings altogether." },
                { settings.GetOptionLabelLocaleID(nameof(ModSettings.AllowFarmExtractors)), "Allow Extractors on Farms" },
                { settings.GetOptionDescLocaleID(nameof(ModSettings.AllowFarmExtractors)), "Allows extractor buildings to spawn on farms." },
                { settings.GetOptionLabelLocaleID(nameof(ModSettings.AllowForestExtractors)), "Allow Extractors on Forest" },
                { settings.GetOptionDescLocaleID(nameof(ModSettings.AllowForestExtractors)), "Allows extractor buildings to spawn on forest." },
                { settings.GetOptionLabelLocaleID(nameof(ModSettings.AllowOilExtractors )), "Allow Extractors on Oil" },
                { settings.GetOptionDescLocaleID(nameof(ModSettings.AllowOilExtractors )), "Allows extractor buildings to spawn on oil." },
                { settings.GetOptionLabelLocaleID(nameof(ModSettings.AllowOreExtractors)), "Allow Extractors on Ore" },
                { settings.GetOptionDescLocaleID(nameof(ModSettings.AllowOreExtractors)), "Allows extractor buildings to spawn on ore." },
                { settings.GetOptionLabelLocaleID(nameof(ModSettings.AllowFishExtractors)), "Allow Extractors on Fish" },
                { settings.GetOptionDescLocaleID(nameof(ModSettings.AllowFishExtractors)), "Allows extractor buildings to spawn in fishing extractors (both land and sea)." },

                { settings.GetOptionLabelLocaleID(nameof(ModSettings.AllowFarmVehicles)), "Allow Farm Vehicles" },
                { settings.GetOptionDescLocaleID(nameof(ModSettings.AllowFarmVehicles)), "Allows vehicles to spawn on farmland." },
                { settings.GetOptionLabelLocaleID(nameof(ModSettings.AllowForestVehicles)), "Allow Forest Vehicles" },
                { settings.GetOptionDescLocaleID(nameof(ModSettings.AllowForestVehicles)), "Allows vehicles to spawn in forests." },
                { settings.GetOptionLabelLocaleID(nameof(ModSettings.AllowOilVehicles)), "Allow Oil Vehicles" },
                { settings.GetOptionDescLocaleID(nameof(ModSettings.AllowOilVehicles)), "Allows vehicles to spawn on oil extractors." },
                { settings.GetOptionLabelLocaleID(nameof(ModSettings.AllowOreVehicles)), "Allow Ore Vehicles" },
                { settings.GetOptionDescLocaleID(nameof(ModSettings.AllowOreVehicles)), "Allows vehicles to spawn on ore extractors." },
                { settings.GetOptionLabelLocaleID(nameof(ModSettings.AllowFishingBoats)), "Allow Fishing Boats/Vehicles" },
                { settings.GetOptionDescLocaleID(nameof(ModSettings.AllowFishingBoats)), "Allows boats and vehicles to spawn fishing extractors." },

                { settings.GetOptionLabelLocaleID(nameof(ModSettings.DespawnExtractors)), "Despawn Extractors" },
                { settings.GetOptionDescLocaleID(nameof(ModSettings.DespawnExtractors)), "Despawns all existing extractor sub-buildings." },
                { settings.GetOptionWarningLocaleID(nameof(ModSettings.DespawnExtractors)), "Do you want to permanently despawn all existing extractor buildings?" },
                { settings.GetOptionLabelLocaleID(nameof(ModSettings.DespawnVehicles)), "Despawn Work Vehicles" },
                { settings.GetOptionDescLocaleID(nameof(ModSettings.DespawnVehicles)), "Despawns all currently active work vehicles." },
                { settings.GetOptionWarningLocaleID(nameof(ModSettings.DespawnVehicles)), "Do you want to permanently despawn all existing work vehicles?" }
            };
        }

        public IEnumerable<KeyValuePair<string, string>> ReadEntries(IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts) => Entries;

        public void Unload()
        {
        }
    }
}
