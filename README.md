# Extractors Begone

A small mod for Cities Skylines 2 that disables spawning of sub-buildings and vehicles for extractor areas.

## Contents

This mod disables two in-game systems:

- The area spawn system is used to spawn extractor and storage sub-buildings.
- The work car AI system, which controls cars that spawn within extractor areas.

Resources are still accumulating in the central buildings and are transported, so this should not break your game. It might even marginally improve performance, but don't expect miracles.

There might be some unintended side-effects of using this mod. First, it might be that some storage-related buildings do no longer work as intended. I haven't encountered any of those situations and "not working" only refers to the visual functionality. Mechanically, all buildings I've tested still worked as intended. Second, this mod disables those systems altogether. It is not possible to only disable those systems per industry. This also means that fishing boats (and other waterborne vehicles) that are going to be released with the upcoming DLC will not spawn. Their code appears to be already implemented in the current game and they re-use the same system as the other specialized industry areas.

As always, use this at your own risk. Back up your savegames and test this in an fresh city first. It should work with existing saves and uninstalling the mod should restore the original functionality. After installing this mod you can (and might to) remove existing sub-buildings and work vehicles using better bulldozer. Note that work vehicles will not despawn, when this mod is used with a savegame where they are already spawned.

## Contribute

If you want to contribute, feel free to open a PR! This mod is currently bare-bones and I do not intend to spent much time on it. However, I would be grateful for any contribution, especially regarding an UI and a thumbnail.

## License

Everything in this repository is licensed under [MIT license terms](https://github.com/crud89/CS2-ExtractorsBegone/LICENSE).