using System.ComponentModel;
using Synapse.Translation;

namespace BetterScp
{
    public class PluginTranslation : IPluginTranslation
    {
        public string Spawn { get; set; } = "As Scp you can use the .scp command in the console to swap your class or see all living scp's";

        public string Help { get; set; } = "All Commands you can use as Scp:\\nScp Swap {RoleID} - Swaps your current Scp Role\\nScp List - Gives you a List of all Living Scp's";

        public string NoSCP { get; set; } = "You are not an Scp so you cant use this command!";

        public string GotRequest { get; set; } = "<i>You have an SCP Swap request!\\nCheck your console</i>";

        public string GotRequestConsole { get; set; } = "You have received a swap request from %player% who is SCP-%id%. Would you like to swap with them? Type \".scp swap yes\" to accept or \".scp swap no\" to decline.";

        public string NoRespond { get; set; } = "The Player did not respond to your request";

        public string Timeout { get; set; } = "Your swap request has timed out";

        public string SwapSuccess { get; set; } = "Swap succesful!";

        public string RoundNotStarted { get; set; } = "The round hasn't started yet!";

        public string Expired { get; set; } = "SCP swap period has expired";

        public string NoSwap { get; set; } = "You do not have a swap request.";

        public string Denied { get; set; } = "Swap request denied";

        public string Denied2 { get; set; } = "Your swap request has been denied";

        public string NothingToCancel { get; set; } = "You do not have an outgoing swap request";

        public string Cancelled { get; set; } = "Your swap request has been cancelled.";

        public string Blacklist { get; set; } = "That SCP is blacklisted";

        public string Already { get; set; } = "You already have a request pending";

        public string SameSwap { get; set; } = "You cannot swap with your own role";

        public string SwapSent { get; set; } = "Swap request sent";

        public string NoOneToSwap { get; set; } = "No players found to swap with";
    }
}
