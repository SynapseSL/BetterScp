﻿using Synapse;
using MEC;
using System.Collections.Generic;
using System.Linq;

namespace BetterScp
{
    public class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Player.PlayerDeathEvent += OnDeath;
            Server.Get.Events.Player.PlayerSetClassEvent += OnSetClass;
            Server.Get.Events.Round.RoundStartEvent += OnStart;
            Server.Get.Events.Round.RoundRestartEvent += OnRestart;

            Timing.RunCoroutine(TimeHeal());
        }

        private void OnSetClass(Synapse.Api.Events.SynapseEventArguments.PlayerSetClassEventArgs ev)
        {
            Timing.CallDelayed(0.1f, () =>
             {
                 if (ev.Player.RealTeam == Team.SCP)
                 {
                     ev.Player.GiveTextHint(PluginClass.GetTranslation("spawn"), 7.5f);

                     var config = PluginClass.Config.ScpConfigs.FirstOrDefault(x => x.Id == ev.Player.RoleID);
                     if (config == null || config.Health < 0) return;
                     ev.Player.MaxHealth = config.Health;
                     ev.Player.Health = config.Health;
                 }
             });
        }

        private void OnStart()
        {
            ScpCommand.allowSwaps = true;
            Timing.CallDelayed(PluginClass.Config.SwapRequestTimeout, () => ScpCommand.allowSwaps = false);
        }

        private IEnumerator<float> TimeHeal()
        {
            for(; ; )
            {
                yield return Timing.WaitForSeconds(30f);
                foreach(var player in Server.Get.GetPlayers(x => x.RealTeam == Team.SCP))
                {
                    var config = PluginClass.Config.ScpConfigs.FirstOrDefault(x => x.Id == player.RoleID);
                    if (config == null) continue;

                    if (!config.Regenerateovermax && player.Health + config.TimeHp > player.MaxHealth)
                        player.Health = player.MaxHealth;
                    else
                        player.Health += config.TimeHp;
                }
            }
        }

        private void OnRestart()
        {
            Timing.KillCoroutines(ScpCommand.reqCoroutines.Values.ToArray());
            ScpCommand.reqCoroutines.Clear();
        }

        private void OnDeath(Synapse.Api.Events.SynapseEventArguments.PlayerDeathEventArgs ev)
        {
            if (ev.Victim == null) return;

            if(ev.Victim.RealTeam == Team.SCP)
            {
                var config = PluginClass.Config.ScpConfigs.FirstOrDefault(x => x.Id == ev.Victim.RoleID);
                if (config == null) return;

                foreach(var item in config.Drops)
                {
                    var synapseitem = item.Parse();
                    synapseitem.Position = ev.Victim.Position;
                    synapseitem.Drop();
                }
            }

            if (ev.Killer == null) return;

            if(ev.Killer.RealTeam == Team.SCP && ev.Victim.RealTeam != Team.SCP)
            {
                var config = PluginClass.Config.ScpConfigs.FirstOrDefault(x => x.Id == ev.Killer.RoleID);
                if (config != null)
                {
                    if (!config.Regenerateovermax && ev.Killer.Health + config.Killhp > ev.Killer.MaxHealth)
                        ev.Killer.Health = ev.Killer.MaxHealth;
                    else
                        ev.Killer.Health += config.Killhp;
                }
            }
        }
    }
}
