using MEC;
using Synapse;
using Synapse.Api;
using Synapse.Api.Roles;
using Synapse.Command;
using System.Collections.Generic;
using System.Linq;

namespace BetterScp
{
    [CommandInformation(
        Name = "scp",
        Aliases = new[] { "betterscp" },
        Description = "A Command for Scp's to swap Roles and see all Living Scp's",
        Permission = "",
        Platforms = new[] { Platform.ClientConsole },
        Usage = "just use the Command with no parameter in order to get help"
        )]
    public class ScpCommand : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {
            var result = new CommandResult();

            if(context.Player.RealTeam != Team.SCP)
            {
                result.Message = PluginClass.Translation.ActiveTranslation.NoSCP;
                result.State = CommandResultState.Error;
                return result;
            }

            if (context.Arguments.Count < 1)
                context.Arguments = new System.ArraySegment<string>(new[] { "" });

            switch (context.Arguments.First().ToUpper())
            {
                //Thanks to Cyanox62 from which is the original Code for ScpSwap => https://github.com/Cyanox62/SCPSwap
                case "SWAP":
                    if (!addedCustomScps)
                    {
                        foreach (var role in Server.Get.RoleManager.CustomRoles)
                        {
                            if (Server.Get.RoleManager.GetCustomRole(role.ID).GetTeamID() != (int)Team.SCP) continue;

                            valid.Add(role.Name, role.ID);
                            valid.Add(role.ID.ToString(), role.ID);
                        }
                        addedCustomScps = true;
                    }

                    if (!Server.Get.Map.Round.RoundIsActive)
                    {
                        result.Message = PluginClass.Translation.ActiveTranslation.RoundNotStarted;
                        result.State = CommandResultState.Error;
                        return result;
                    }

                    if (!allowSwaps)
                    {
                        result.Message = PluginClass.Translation.ActiveTranslation.Expired;
                        result.State = CommandResultState.Error;
                        return result;
                    }

                    if (PluginClass.Config.BlackListedScps.Contains(context.Player.RoleID))
                        return new CommandResult
                        {
                            State = CommandResultState.Error,
                            Message = PluginClass.Translation.ActiveTranslation.Blacklist
                        };

                    if (context.Arguments.Count < 2)
                        context.Arguments = new System.ArraySegment<string>(new[] { "","" });

                    switch (context.Arguments.ElementAt(1).ToUpper())
                    {
                        case "YES":
                            var player = ongoingReqs.FirstOrDefault(x => x.Value == context.Player).Key;
                            if(player == null)
                            {
                                result.Message = PluginClass.Translation.ActiveTranslation.NoSwap;
                                result.State = CommandResultState.Error;
                                return result;
                            }

                            PerformSwap(player, context.Player);

                            result.Message = PluginClass.Translation.ActiveTranslation.SwapSuccess;
                            result.State = CommandResultState.Ok;
                            Timing.KillCoroutines(reqCoroutines[player]);
                            reqCoroutines.Remove(player);
                            break;

                        case "NO":
                            player = ongoingReqs.FirstOrDefault(x => x.Value == context.Player).Key;
                            if (player == null)
                            {
                                result.Message = PluginClass.Translation.ActiveTranslation.NoSwap;
                                result.State = CommandResultState.Error;
                                return result;
                            }

                            result.Message = PluginClass.Translation.ActiveTranslation.Denied;
                            player.SendConsoleMessage(PluginClass.Translation.ActiveTranslation.Denied2);
                            Timing.KillCoroutines(reqCoroutines[player]);
                            reqCoroutines.Remove(player);
                            ongoingReqs.Remove(player);
                            break;

                        case "CANCEL":
                            if (!ongoingReqs.ContainsKey(context.Player))
                            {
                                result.Message = PluginClass.Translation.ActiveTranslation.NothingToCancel;
                                result.State = CommandResultState.Error;
                                return result;
                            }

                            var target = ongoingReqs[context.Player];
                            target.SendConsoleMessage(PluginClass.Translation.ActiveTranslation.Cancelled);
                            Timing.KillCoroutines(reqCoroutines[context.Player]);
                            reqCoroutines.Remove(context.Player);
                            ongoingReqs.Remove(context.Player);
                            result.Message = PluginClass.Translation.ActiveTranslation.Cancelled;
                            result.State = CommandResultState.Ok;
                            break;

                        default:
                            if (!valid.ContainsKey(context.Arguments.ElementAt(1)))
                            {
                                result.Message = "Invalid Scp";
                                result.State = CommandResultState.Error;
                                break;
                            }

                            if (ongoingReqs.ContainsKey(context.Player))
                            {
                                result.Message = PluginClass.Translation.ActiveTranslation.Already;
                                result.State = CommandResultState.Error;
                                break;
                            }

                            var roleid = valid[context.Arguments.ElementAt(1)];

                            if(PluginClass.Config.BlackListedScps.Contains(roleid))
                            {
                                result.Message = PluginClass.Translation.ActiveTranslation.Blacklist;
                                result.State = CommandResultState.Error;
                                break;
                            }

                            if(context.Player.RoleID == roleid)
                            {
                                result.Message = PluginClass.Translation.ActiveTranslation.SameSwap;
                                result.State = CommandResultState.Error;
                                break;
                            }

                            player = Server.Get.GetPlayers(x => roleid == 16 ? x.RoleID == roleid || x.RoleID == 177 : x.RoleID == roleid).FirstOrDefault();
                            if(player != null)
                            {
                                reqCoroutines.Add(context.Player, Timing.RunCoroutine(SendRequest(context.Player, player)));
                                result.Message = PluginClass.Translation.ActiveTranslation.SwapSent;
                                result.State = CommandResultState.Ok;
                                break;
                            }

                            if (PluginClass.Config.AllowNewScps)
                            {
                                context.Player.RoleID = roleid;
                                result.Message = PluginClass.Translation.ActiveTranslation.SwapSuccess;
                                result.State = CommandResultState.Ok;
                                break;
                            }
                            result.Message = PluginClass.Translation.ActiveTranslation.NoOneToSwap;
                            result.State = CommandResultState.Error;
                            break;
                    }
                    break;

                case "LIST":
                    var msg = "All Living Scp's:";
                    foreach (var player in Server.Get.GetPlayers(x => x.RealTeam == Team.SCP))
                    {
                        var rolename = player.RoleID >= 0 && player.RoleID <= RoleManager.HighestRole ? player.RoleType.ToString() : player.CustomRole.GetRoleName();
                        msg += $"\n{player} - {rolename}";
                    }
                    result.Message = msg;
                    result.State = CommandResultState.Ok;
                    break;

                default:
                    result.Message = PluginClass.Translation.ActiveTranslation.Help.Replace("\\n","\n");
                    result.State = CommandResultState.Ok;
                    break;
            }

            return result;
        }

        internal static bool allowSwaps = false;
        private bool addedCustomScps = false;
        private readonly Dictionary<string, int> valid = new Dictionary<string, int>
        {
            {"173", 0},
            {"peanut", 0},
            {"939", 16},
            {"dog", 16 },
            {"079", 7},
            {"computer", 7},
            {"106", 3},
            {"larry", 3},
            {"096", 9},
            {"shyguy", 9},
            {"049", 5},
            {"doctor", 5},
            {"0492", 10},
            {"zombie", 10}
        };
        internal static Dictionary<Player, CoroutineHandle> reqCoroutines = new Dictionary<Player, CoroutineHandle>();
        private readonly Dictionary<Player, Player> ongoingReqs = new Dictionary<Player, Player>();

        private IEnumerator<float> SendRequest(Player player,Player target)
        {
            ongoingReqs.Add(player, target);
            target.GiveTextHint(PluginClass.Translation.ActiveTranslation.GotRequest.Replace("\\n","\n"));
            target.SendConsoleMessage(PluginClass.Translation.ActiveTranslation.GotRequestConsole.Replace("%player%",player.NickName).Replace("%id%",player.RoleID.ToString()),"yellow");
            yield return Timing.WaitForSeconds(PluginClass.Config.SwapRequestTimeout);
            TimeoutRequest(player);
        }

        private void TimeoutRequest(Player player)
        {
            if (ongoingReqs.ContainsKey(player))
            {
                var target = ongoingReqs[player];
                ongoingReqs.Remove(player);

                player.SendConsoleMessage(PluginClass.Translation.ActiveTranslation.NoRespond);
                target.SendConsoleMessage(PluginClass.Translation.ActiveTranslation.Timeout);
            }
        }

        private void PerformSwap(Player player,Player target)
        {
            var playerid = player.RoleID;
            var targetid = target.RoleID;

            player.RoleID = targetid;
            target.RoleID = playerid;
            ongoingReqs.Remove(player);
        }
    }
}
