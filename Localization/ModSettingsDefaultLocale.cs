using Colossal;
using System.Collections.Generic;

namespace crud89.ExtractorsBegone.Localization
{
    public class ModSettingsDefaultLocale : IDictionarySource
    {
        private Dictionary<string, string> Entries { get; set; }

        public ModSettingsDefaultLocale(ModSettings settings)
        {
            Entries = LoadSettingsLocale(settings);
        }

        public IEnumerable<KeyValuePair<string, string>> ReadEntries(IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts) => Entries;

        public void Unload()
        {
        }

        private static Dictionary<string, string> LoadSettingsLocale(ModSettings settings)
        {
            return new Dictionary<string, string>
            {
                { settings.GetSettingsLocaleID(), "Extractors Begone" },
                { settings.GetOptionGroupLocaleID(ModSettings.ExtractorsGroupName), "Extractor Buildings" },
                { settings.GetOptionGroupLocaleID(ModSettings.WorkVehiclesGroupName), "Work Vehicles" },
                { settings.GetOptionGroupLocaleID(ModSettings.DespawnGroupName), "Despawn Existing" },

                { settings.GetOptionLabelLocaleID(nameof(ModSettings.DisableExtractorBuildings)), "Disable Extractor Spawn System" },
                { settings.GetOptionDescLocaleID(nameof(ModSettings.DisableExtractorBuildings)), "Disables spawning new extractor buildings altogether. Note that this may have unintended side-effects for non-extractor buildings (storage and cargo areas). If you experience issues with those, leave the spawn system enabled and only disable the individual extractors below." },
                { settings.GetOptionLabelLocaleID(nameof(ModSettings.FarmExtractorsSpawnFactor)), "Farm Extractors Spawn Factor" },
                { settings.GetOptionDescLocaleID(nameof(ModSettings.FarmExtractorsSpawnFactor)), "Sets the base spawn rate for farm extractors. Set this to 0.0 to disable spawning extractor buildings. The default rate is 2.0." },
                { settings.GetOptionLabelLocaleID(nameof(ModSettings.ForestExtractorsSpawnFactor)), "Forest Extractor Spawn Factor" },
                { settings.GetOptionDescLocaleID(nameof(ModSettings.ForestExtractorsSpawnFactor)), "Sets the base spawn rate for forest extractors. Set this to 0.0 to disable spawning extractor buildings. The default rate is 2.0." },
                { settings.GetOptionLabelLocaleID(nameof(ModSettings.OilExtractorsSpawnFactor )), "Oil Extractor Spawn Factor" },
                { settings.GetOptionDescLocaleID(nameof(ModSettings.OilExtractorsSpawnFactor )), "Sets the base spawn rate for oil extractors. Set this to 0.0 to disable spawning extractor buildings. The default rate is 2.0." },
                { settings.GetOptionLabelLocaleID(nameof(ModSettings.OreExtractorsSpawnFactor)), "Ore Extractor Spawn Factor" },
                { settings.GetOptionDescLocaleID(nameof(ModSettings.OreExtractorsSpawnFactor)), "Sets the base spawn rate for ore extractors. Set this to 0.0 to disable spawning extractor buildings. The default rate is 2.0." },
                { settings.GetOptionLabelLocaleID(nameof(ModSettings.FishExtractorsSpawnFactor)), "Fish Extractor Spawn Factor" },
                { settings.GetOptionDescLocaleID(nameof(ModSettings.FishExtractorsSpawnFactor)), "Sets the base spawn rate for fish extractors. Set this to 0.0 to disable spawning extractor buildings. The default rate is 2.0." },

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
                { settings.GetOptionDescLocaleID(nameof(ModSettings.DespawnExtractors)), "Despawns all existing extractor sub-buildings for all extractor types, that have their spawn factor set to 0.0 above." },
                { settings.GetOptionWarningLocaleID(nameof(ModSettings.DespawnExtractors)), "Do you want to permanently despawn all existing extractor buildings?" },
                { settings.GetOptionLabelLocaleID(nameof(ModSettings.ResetDefaultSpawnFactors)), "Set Default Spawn Rates" },
                { settings.GetOptionDescLocaleID(nameof(ModSettings.ResetDefaultSpawnFactors)), "Resets all spawn rates to the default value. You can use this option before un-installing the mod from a savegame. Make sure the savegame is loaded first. After resetting the spawn factors, re-save the game." },
            };
        }
    }
}
