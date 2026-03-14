using Colossal.IO.AssetDatabase;
using Game.Modding;
using Game.Settings;
using Game.Simulation;
using Game.UI;
using System;
using Unity.Entities;

namespace crud89.ExtractorsBegone
{
    using Systems;

    [FileLocation("ModsSettings/" + nameof(ExtractorsBegone) + "/" + nameof(ExtractorsBegone))]
    [SettingsUIShowGroupName(ExtractorsGroupName, WorkVehiclesGroupName, DespawnGroupName)]
    public sealed class ModSettings : ModSetting
    {
        public const string ExtractorsGroupName = "Extractors";

        public const string WorkVehiclesGroupName = "WorkVehicles";

        public const string DespawnGroupName = "Despawn";

        [SettingsUISection(ExtractorsGroupName)]
        [SettingsUISetter(typeof(ModSettings), nameof(ToggleExtractorBuildings))]
        public bool DisableExtractorBuildings { get; set; }

        [SettingsUISection(ExtractorsGroupName)]
        [SettingsUIDisableByConditionAttribute(typeof(ModSettings), nameof(DisableExtractorBuildings))]
        public bool AllowFarmExtractors { get; set; }

        [SettingsUISection(ExtractorsGroupName)]
        [SettingsUIDisableByConditionAttribute(typeof(ModSettings), nameof(DisableExtractorBuildings))]
        public bool AllowForestExtractors { get; set; }

        [SettingsUISection(ExtractorsGroupName)]
        [SettingsUIDisableByConditionAttribute(typeof(ModSettings), nameof(DisableExtractorBuildings))]
        public bool AllowOilExtractors { get; set; }

        [SettingsUISection(ExtractorsGroupName)]
        [SettingsUIDisableByConditionAttribute(typeof(ModSettings), nameof(DisableExtractorBuildings))]
        public bool AllowOreExtractors { get; set; }

        [SettingsUISection(ExtractorsGroupName)]
        [SettingsUIDisableByConditionAttribute(typeof(ModSettings), nameof(DisableExtractorBuildings))]
        public bool AllowFishExtractors { get; set; }

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


        // TODO: Extended controls for work vehicle types.

        [SettingsUIButtonGroup(DespawnGroupName)]
        [SettingsUIButton]
        [SettingsUIConfirmation]
        public bool DespawnExtractors
        {
            set => DespawnExtractorBuildings(value);
        }

        [SettingsUIButtonGroup(DespawnGroupName)]
        [SettingsUIButton]
        [SettingsUIConfirmation]
        public bool DespawnVehicles
        {
            set => DespawnWorkVehicles(value);
        }

        public ModSettings(IMod mod) : 
            base(mod) 
        {
            this.SetDefaults();
        }

        public override void SetDefaults()
        {
            this.DisableExtractorBuildings = true;
            this.AllowFarmExtractors = false;
            this.AllowForestExtractors = false;
            this.AllowOilExtractors = false;
            this.AllowOreExtractors = false;
            this.AllowFishExtractors = false;

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
                var extractorAreaSystem = world.GetExistingSystem<ExtractorAreaSystem>();
                ref var state = ref world.Unmanaged.ResolveSystemStateRef(extractorAreaSystem);
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

            // TODO: Implement me.
        }

        private void DespawnWorkVehicles(bool despawn)
        {
            if (!despawn)
                return;

            // TODO: Implement me.
        }
    }
}
