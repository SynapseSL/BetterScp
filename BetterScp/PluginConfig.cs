using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace BetterScp
{
    public class PluginConfig : AbstractConfigSection
    {
        [Description("The time after which ScpSwap is no longer allowed")]
        public float SwapRequestTimeout = 30f;

        [Description("If you can Swap to Scp's that currently does not exist")]
        public bool AllowNewScps = true;

        [Description("Scp's that are not allowed to swap with Example:Zombie(10), Scp035(35)")]
        public List<int> BlackListedScps = new List<int> { (int)RoleType.Scp0492 };

        [Description("The main Configuration for Scps")]
        public List<SerializedScpConfig> ScpConfigs = new List<SerializedScpConfig>
        {
            new SerializedScpConfig(-1, 100f, 25f, false, new List<SerializedItem>() { new SerializedItem((int)ItemType.KeycardZoneManager, 0, 0, 0, 0, Vector3.one) })
        };
    }

    public class SerializedScpConfig
    {
        public SerializedScpConfig() { }

        public SerializedScpConfig(int id, float killhp, float timehp, bool regenerateovermax, List<SerializedItem> drops, int health = -1) { Id = id; Killhp = killhp; TimeHp = timehp; Regenerateovermax = regenerateovermax; Drops = drops; Health = health; }

        [Description("The Health with that they spawn with")]
        public int Health;

        [Description("The Role ID of the Scp you want to setup")]
        public int Id;

        [Description("The Amount of Hp the Scp gets for each kill")]
        public float Killhp;

        [Description("The Amount of Hp the Scp gets each 30 seconds")]
        public float TimeHp;

        [Description("If set to true Scps can regenerate over they max Health")]
        public bool Regenerateovermax;

        [Description("The Items which should Spawn when the Scp dies")]
        public List<SerializedItem> Drops;
    }
}
