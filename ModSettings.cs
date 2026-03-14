using Colossal.IO.AssetDatabase;
using Game.Modding;
using Game.Settings;
using Game.Simulation;
using System;
using Unity.Entities;

namespace crud89.ExtractorsBegone
{
    [FileLocation("ModsSettings/" + nameof(ExtractorsBegone) + "/" + nameof(ExtractorsBegone))]
    public sealed class ModSettings : ModSetting
    {
        [SettingsUISetter(typeof(ModSettings), nameof(ToggleExtractorBuildings))]
        public bool DisableExtractorBuildings { get; set; }
        
        [SettingsUISetter(typeof(ModSettings), nameof(ToggleWorkVehicles))]
        public bool DisableWorkVehicles { get; set; }

        // TODO: Extended controls for work vehicle types.

        [SettingsUIButton]
        [SettingsUIConfirmation]
        [SettingsUIButtonGroup("Despawn")]
        public bool DespawnExtractors
        {
            set
            {

            }
        }

        [SettingsUIButton]
        [SettingsUIConfirmation]
        [SettingsUIButtonGroup("Despawn")]
        public bool DespawnWorkVehicles
        {
            set
            {

            }
        }

        public ModSettings(IMod mod) : 
            base(mod) 
        {
            this.SetDefaults();
        }

        public override void SetDefaults()
        {
            this.DisableExtractorBuildings = true;
            this.DisableWorkVehicles = true;
        }

        public void ApplySystemStates()
        {
            ToggleExtractorBuildings(DisableExtractorBuildings);
            ToggleWorkVehicles(DisableWorkVehicles);
        }

        public void ToggleExtractorBuildings(bool disabled)
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

        public void ToggleWorkVehicles(bool disabled)
        {
            // Get default game object.
            var world = World.DefaultGameObjectInjectionWorld;

            // Disable work car AI system.
            ExtractorsBegone.log.InfoFormat("Toggle work car AI system: {0}.", !disabled);

            try
            {
                var workCarAiSystem = world.GetExistingSystem<WorkCarAISystem>();
                ref var state = ref world.Unmanaged.ResolveSystemStateRef(workCarAiSystem);
                state.Enabled = !disabled;
            }
            catch(Exception ex)
            {
                ExtractorsBegone.log.Error(ex);
            }
        }
    }
}
