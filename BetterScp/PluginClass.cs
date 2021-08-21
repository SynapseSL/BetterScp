using Synapse.Api.Plugin;
using Synapse.Translation;

namespace BetterScp
{
    [PluginInformation(
        Name = "BetterScp",
        Author = "Dimenzio",
        Description = "An Plugin which improves the Scps",
        LoadPriority = 0,
        SynapseMajor = 2,
        SynapseMinor = 6,
        SynapsePatch = 1,
        Version = "v.1.1.3"
        )]
    public class PluginClass : AbstractPlugin
    {
        [Config(section = "BetterScp")]
        public static PluginConfig Config { get; set; }

        [SynapseTranslation]
        public static new SynapseTranslation<PluginTranslation> Translation { get; set; }

        public override void Load()
        {
            Translation.AddTranslation(new PluginTranslation());
            Translation.AddTranslation(new PluginTranslation
            {
                Spawn = "Als SCP kannst du den .scp Befehl benutzen in der ö-Konsole um deine Klasse zu wechseln",
                Help = "Alle Befehle die du benutzen kannst:\\nScp Swap {RoleID} - Wechselt deine Klasse zu einem anderen SCP\\nScp List - Gibt dir eine Liste von allen am lebende SCPs",
                NoSCP = "Da du kein SCP bist kannst du diesen Befehl auch nicht verwenden",
                GotRequest = "<i>Du hast eine Tauschanfrage bekommen\\nÖffne deine Konsole mit ö zum auschecken</i>",
                GotRequestConsole = "Du hast eine Anfrage bekommen von %player%, welcher SCP-%id% ist. Möchtest du mit ihm tauschen? Gib \".scp swap yes\" ein zum akzeptieren oder \".scp swap no\" zum ablehnen",
                NoRespond = "Der Spieler hat nicht auf deine Anfrage reagiert",
                Timeout = "Deine Anfrage ist abgelaufen",
                SwapSuccess = "Erfolgreich getauscht!",
                RoundNotStarted = "Die Runde hat noch nicht einmal angefangen!",
                Expired = "Die Zeit zum tauschen ist abgelaufen",
                NoSwap = "Du hast keine aktive Tauschanfrage",
                Denied = "Tauschanfrage wurde abgelehnt",
                Denied2 = "Deine Tauschanfrage wurde abgelehnt",
                NothingToCancel = "Du hast keine Tauschanfrage gesendet",
                Cancelled = "Deine Tauschanfrage wurde abgebrochen",
                Blacklist = "Dieses SCP ist auf der Blacklist",
                Already = "Du hast bereits eine Tauschanfrage gesendet",
                SameSwap = "Du kannst nicht mit dir selber tauschen",
                SwapSent = "Tauschanfrage wurde gesendet",
                NoOneToSwap = "Es wurde kein Spieler gefunden um mit ihm zu tauschen"
            },"GERMAN");

            new EventHandlers();
        }
    }
}
