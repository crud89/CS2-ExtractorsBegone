using Colossal.IO.AssetDatabase;
using Colossal.Logging;
using ExtractorsBegone.Localization;
using Game;
using Game.Modding;
using Game.SceneFlow;

namespace crud89.ExtractorsBegone
{
    public class ExtractorsBegone : IMod
    {
        public static ILog log = LogManager.GetLogger(nameof(ExtractorsBegone)).SetShowsErrorsInUI(false);

        internal ModSettings Settings { get; set; }

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

            log.Info($"{nameof(ExtractorsBegone)}::{nameof(OnLoad)}");

            if (GameManager.instance.modManager.TryGetExecutableAsset(this, out var asset))
                log.Info($"Current mod asset at {asset.path}");

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
    }
}
