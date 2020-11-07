using Synapse.Api.Plugin;
using System.Collections.Generic;

namespace BetterScp
{
    [PluginInformation(
        Name = "BetterScp",
        Author = "Dimenzio",
        Description = "An Plugin which improves the Scps",
        LoadPriority = int.MinValue,
        SynapseMajor = 2,
        SynapseMinor = 0,
        SynapsePatch = 0,
        Version = "v.1.0.0"
        )]
    public class PluginClass : AbstractPlugin
    {
        private static PluginClass pclass;

        [Synapse.Api.Plugin.Config(section = "BetterScp")]
        public static Config Config;

        public override void Load()
        {
            pclass = this;

            var trans = new Dictionary<string, string>
            {
                {"spawn","As Scp you can use the .scp command in the console to swap your class or see all living scp's" },
                {"help","All Commands you can use as Scp:\nScp Swap {RoleID} - Swaps your current Scp Role\nScp List - Gives you a List of all Living Scp's" },
                {"noscp", "You are not an Scp so you cant use this command!" },
                {"gotrequest","<i>You have an SCP Swap request!\nCheck your console</i>" },
                {"gotrequestconsole","You have received a swap request from %player% who is SCP-%id%. Would you like to swap with them? Type \".scp swap yes\" to accept or \".scp swap no\" to decline." },
                {"norespond","The Player did not respong to your request" },
                {"timeout","Your swap request has timed out" },
                {"swapsucces","Swap succesful!" },
                {"roundnotstarted","The round hasn't started yet!" },
                {"expired", "SCP swap period has expired" },
                {"noswap","You do not have a swap request." },
                {"denied","Swap request denied" },
                {"denied2",">our swap request has been denied" },
                {"nothingtocancel","You do not have an outgoing swap request" },
                {"canceled","Your swap request has been cancelled." },
                {"blacklist","That SCP is blacklisted" },
                {"already","You already have a request pending" },
                {"sameswap",">ou cannot swap with your own role" },
                {"swapsent","Swap request sent" },
                {"noonetoswap","No players found to swap with" }
            };
            Translation.CreateTranslations(trans);

            new EventHandlers();
        }

        public static string GetTranslation(string key) => pclass.Translation.GetTranslation(key);
    }
}
