using Colossal.IO.AssetDatabase;
using Colossal.Logging;
using Game;
using Game.Modding;
using Game.SceneFlow;
using Game.Simulation;
using Game.UI;
using Unity.Entities;

namespace crud89.ExtractorsBegone
{
    using Systems;
    using Localization;

    public class ExtractorsBegone : IMod
    {
        public static ILog log = LogManager.GetLogger(nameof(ExtractorsBegone)).SetShowsErrorsInUI(false);

        public static ExtractorsBegone Instance { get; private set; }

        internal ModSettings Settings { get; set; }

        public void OnLoad(UpdateSystem updateSystem)
        {
            Instance = this;

            // Register log level.
#if VERBOSE
            log.SetEffectiveness(Level.All);
            log.SetShowsErrorsInUI(true);
#elif DEBUG
            log.SetEffectiveness(Level.Debug);
            log.SetShowsErrorsInUI(true);
#else
            log.SetEffectiveness(Level.Info);
#endif 

            log.Info($"{nameof(ExtractorsBegone)}::{nameof(OnLoad)}");

            if (GameManager.instance.modManager.TryGetExecutableAsset(this, out var asset))
                log.Info($"Current mod asset at {asset.path}");

            // Disable built-in systems.
            DisableBuiltinSystems();
            log.Info("Builtin systems disabled.");

            // Inject custom systems.
            InjectSystemOverrides(updateSystem);
            log.Info("Custom systems injected.");

            // Register mod settings.
            Settings = new ModSettings(this);
            Settings.RegisterInOptionsUI();

            AssetDatabase.global.LoadSettings(nameof(ExtractorsBegone), Settings, new ModSettings(this));
            Settings.ApplySystemStates();
            log.Info("Settings loaded.");

            // Load default locate.
            GameManager.instance.localizationManager.AddSource("en-US", new ModSettingsDefaultLocale(Settings));
            log.Info("Default locale loaded.");

            // TODO: Load additional localizations.
        }

        public void OnDispose()
        {
            log.Info($"{nameof(ExtractorsBegone)}::{nameof(OnDispose)}");

            Settings?.UnregisterInOptionsUI();
            Settings = null;
        }

        private void DisableBuiltinSystems()
        {
            var world = World.DefaultGameObjectInjectionWorld;

            {
                var areaSpawnSystem = world.GetExistingSystem<AreaSpawnSystem>();
                ref var state = ref world.Unmanaged.ResolveSystemStateRef(areaSpawnSystem);
                state.Enabled = false;
            }

            {
                var workCarAiSystem = world.GetExistingSystem<WorkCarAISystem>();
                ref var state = ref world.Unmanaged.ResolveSystemStateRef(workCarAiSystem);
                state.Enabled = false;
            }
        }

        private void InjectSystemOverrides(UpdateSystem updateSystem)
        {
            updateSystem.UpdateAt<ExtractorAreaSystem>(SystemUpdatePhase.GameSimulation);
            updateSystem.UpdateAt<WorkCarSystem>(SystemUpdatePhase.GameSimulation);
        }
    }
}
