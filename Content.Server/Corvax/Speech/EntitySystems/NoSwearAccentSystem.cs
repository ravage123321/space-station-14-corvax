using Content.Server.Corvax.Speech.Components;
using Robust.Shared.Random;
using System.Text.RegularExpressions;

namespace Content.Server.Speech.EntitySystems;

public sealed class NoSwearAccentSystem : EntitySystem
{
    [Dependency] private readonly ReplacementAccentSystem _replacement = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<NoSwearAccentComponent, AccentGetEvent>(OnAccentGet);
    }

    // converts left word when typed into the right word.
    public string Accentuate(string message, NoSwearAccentComponent component)
    {
        var msg = message;

        msg = _replacement.ApplyReplacements(msg, "noswear");
        return msg;
    }

    private void OnAccentGet(EntityUid uid, NoSwearAccentComponent component, AccentGetEvent args)
    {
        args.Message = Accentuate(args.Message, component);
    }
}
