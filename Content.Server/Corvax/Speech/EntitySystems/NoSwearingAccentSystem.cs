using Content.Server.Corvax.Speech.Components;
using Robust.Shared.Random;
using System.Text.RegularExpressions;

namespace Content.Server.Corvax.Speech.EntitySystems;

public sealed class NoSwearingAccentSystem : EntitySystem
{
    [Dependency] private readonly ReplacementAccentSystem _replacement = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<NoSwearingAccentComponent, AccentGetEvent>(OnAccentGet);
    }

    // converts left word when typed into the right word.
    public string Accentuate(string message, NoSwearingAccentComponent component)
    {
        var msg = message;

        msg = _replacement.ApplyReplacements(msg, "no-swear");
        return msg;
    }

    private void OnAccentGet(EntityUid uid, NoSwearingAccentComponent component, AccentGetEvent args)
    {
        args.Message = Accentuate(args.Message, component);
    }
}
