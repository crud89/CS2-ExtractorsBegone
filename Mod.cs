using Colossal.Logging;
using Game;
using Game.Modding;
using Game.SceneFlow;
using Game.Simulation;
using Unity.Entities;

namespace crud89.ExtractorsBegone
{
    public class ExtractorsBegoneMod : IMod
    {
        public static ILog log = LogManager.GetLogger("ExtractorsBegone").SetShowsErrorsInUI(false);

        public void OnLoad(UpdateSystem updateSystem)
        {
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

            log.Info(nameof(OnLoad));

            if (GameManager.instance.modManager.TryGetExecutableAsset(this, out var asset))
                log.Info($"Current mod asset at {asset.path}");

            var world = World.DefaultGameObjectInjectionWorld;

            // Disable area spawn system, which is spawning sub-buildings and storage facilities.
            log.Info("Disabling area spawn system...");
            var areaSpawnSystem = world.GetExistingSystem<AreaSpawnSystem>();
            ref var state = ref world.Unmanaged.ResolveSystemStateRef(areaSpawnSystem);
            state.Enabled = false;

            // Disable work car AI system.
            log.Info("Disabling work car AI system...");
            var workCarAiSystem = world.GetExistingSystem<WorkCarAISystem>();
            state = ref world.Unmanaged.ResolveSystemStateRef(workCarAiSystem);
            state.Enabled = false;

            log.Info("Systems disabled.");
        }

        public void OnDispose()
        {
            log.Info(nameof(OnDispose));
        }
    }
}
