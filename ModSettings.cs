using Colossal.IO.AssetDatabase;
using Game.Modding;
using Game.Settings;
using Game.Simulation;
using System;
using Unity.Entities;

namespace crud89.ExtractorsBegone
{
    using Systems;

    [FileLocation("ModsSettings/" + nameof(ExtractorsBegone) + "/" + nameof(ExtractorsBegone))]
    [SettingsUIShowGroupName(ExtractorsGroupName, DespawnGroupName, WorkVehiclesGroupName)]
    public sealed class ModSettings : ModSetting
    {
        public const string ExtractorsGroupName = "Extractors";

        public const string WorkVehiclesGroupName = "WorkVehicles";

        public const string DespawnGroupName = "Despawn";

        [SettingsUISection(ExtractorsGroupName)]
        [SettingsUISetter(typeof(ModSettings), nameof(ToggleExtractorBuildings))]
        public bool DisableExtractorBuildings { get; set; }

        [SettingsUISection(ExtractorsGroupName)]
        [SettingsUIDisableByCondition(typeof(ModSettings), nameof(DisableExtractorBuildings))]
        [SettingsUICustomFormat(fractionDigits = 1, maxValueWithFraction = 10.0f, separateThousands = false)]
        [SettingsUISlider(min = 0.0f, max = 10.0f, scalarMultiplier = 1.0f, step = 0.1f)]
        public float FarmExtractorsSpawnFactor { get; set; }

        [SettingsUISection(ExtractorsGroupName)]
        [SettingsUIDisableByCondition(typeof(ModSettings), nameof(DisableExtractorBuildings))]
        [SettingsUICustomFormat(fractionDigits = 1, maxValueWithFraction = 10.0f, separateThousands = false)]
        [SettingsUISlider(min = 0.0f, max = 10.0f, scalarMultiplier = 1.0f, step = 0.1f)]
        public float ForestExtractorsSpawnFactor { get; set; }

        [SettingsUISection(ExtractorsGroupName)]
        [SettingsUIDisableByCondition(typeof(ModSettings), nameof(DisableExtractorBuildings))]
        [SettingsUICustomFormat(fractionDigits = 1, maxValueWithFraction = 10.0f, separateThousands = false)]
        [SettingsUISlider(min = 0.0f, max = 10.0f, scalarMultiplier = 1.0f, step = 0.1f)]
        public float OilExtractorsSpawnFactor { get; set; }

        [SettingsUISection(ExtractorsGroupName)]
        [SettingsUIDisableByCondition(typeof(ModSettings), nameof(DisableExtractorBuildings))]
        [SettingsUICustomFormat(fractionDigits = 1, maxValueWithFraction = 10.0f, separateThousands = false)]
        [SettingsUISlider(min = 0.0f, max = 10.0f, scalarMultiplier = 1.0f, step = 0.1f)]
        public float OreExtractorsSpawnFactor { get; set; }

        [SettingsUISection(ExtractorsGroupName)]
        [SettingsUIDisableByCondition(typeof(ModSettings), nameof(DisableExtractorBuildings))]
        [SettingsUICustomFormat(fractionDigits = 1, maxValueWithFraction = 10.0f, separateThousands = false)]
        [SettingsUISlider(min = 0.0f, max = 10.0f, scalarMultiplier = 1.0f, step = 0.1f)]
        public float FishExtractorsSpawnFactor { get; set; }

        [SettingsUIButtonGroup(DespawnGroupName)]
        [SettingsUIButton]
        [SettingsUIConfirmation]
        public bool DespawnExtractors
        {
            set => DespawnExtractorBuildings(value);
        }

        [SettingsUIButtonGroup(DespawnGroupName)]
        [SettingsUIButton]
        public bool ResetDefaultSpawnFactors
        {
            set => ResetSpawnFactors(value);
        }

        [SettingsUISection(WorkVehiclesGroupName)]
        public bool AllowFarmVehicles { get; set; }

        [SettingsUISection(WorkVehiclesGroupName)]
        public bool AllowForestVehicles { get; set; }

        [SettingsUISection(WorkVehiclesGroupName)]
        public bool AllowOilVehicles { get; set; }

        [SettingsUISection(WorkVehiclesGroupName)]
        public bool AllowOreVehicles { get; set; }

        [SettingsUISection(WorkVehiclesGroupName)]
        public bool AllowFishingBoats { get; set; }

        public ModSettings(IMod mod) : 
            base(mod) 
        {
            this.SetDefaults();
        }

        public override void SetDefaults()
        {
            this.DisableExtractorBuildings = false;
            this.FarmExtractorsSpawnFactor = 0.0f;
            this.ForestExtractorsSpawnFactor = 0.0f;
            this.OilExtractorsSpawnFactor = 0.0f;
            this.OreExtractorsSpawnFactor = 0.0f;
            this.FishExtractorsSpawnFactor = 0.0f;

            this.AllowFarmVehicles = false;
            this.AllowForestVehicles = false;
            this.AllowOilVehicles = false;
            this.AllowOreVehicles = false;
            this.AllowFishingBoats = true;
        }

        public void ApplySystemStates()
        {
            ToggleExtractorBuildings(DisableExtractorBuildings);
        }

        private void ToggleExtractorBuildings(bool disabled)
        {
            // Get default game object.
            var world = World.DefaultGameObjectInjectionWorld;

            // Disable area spawn system, which is spawning sub-buildings and storage facilities.
            ExtractorsBegone.log.InfoFormat("Toggle area spawn system: {0}.", !disabled);

            try
            {
                var areaSpawnSystem = world.GetExistingSystem<AreaSpawnSystem>();
                ref var state = ref world.Unmanaged.ResolveSystemStateRef(areaSpawnSystem);
                state.Enabled = !disabled;
            }
            catch (Exception ex)
            {
                ExtractorsBegone.log.Error(ex);
            }
        }

        private void DespawnExtractorBuildings(bool despawn)
        {
            if (!despawn)
                return;

            // Get default game object.
            var world = World.DefaultGameObjectInjectionWorld;

            // Enable the extractor sub buildings despawn system for one frame.
            ExtractorsBegone.log.InfoFormat("Despawn existing extractors...");
            var system = world.GetOrCreateSystemManaged<DespawnExtractorSubBuildingsSystem>();
            system.Enabled = true;
        }

        private void ResetSpawnFactors(bool reset)
        {
            if (!reset)
                return;

            this.DisableExtractorBuildings = false;
            this.FarmExtractorsSpawnFactor = 2.0f;
            this.ForestExtractorsSpawnFactor = 2.0f;
            this.OilExtractorsSpawnFactor = 2.0f;
            this.OreExtractorsSpawnFactor = 2.0f;
            this.FishExtractorsSpawnFactor = 2.0f;
        }
    }
}
