using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace BetterScp
{
    public class Config : AbstractConfigSection
    {
        public float SwapRequestTimeout = 30f;

        public bool AllowNewScps = true;

        public List<int> BlackListedScps = new List<int> { (int)RoleType.Scp0492 };

        [Description("The main Configuration for Scps")]
        public List<SerializedScpConfig> ScpConfigs = new List<SerializedScpConfig>
        {
            new SerializedScpConfig((int)RoleType.Scp173,100f,25f,false,new List<SerializedItem>()),
            new SerializedScpConfig((int)RoleType.Scp049,100f,25f,false,new List<SerializedItem>()),
            new SerializedScpConfig((int)RoleType.Scp0492,75f,10f,false,new List<SerializedItem>()),
            new SerializedScpConfig((int)RoleType.Scp096,50f,25f,false,new List<SerializedItem>()),
            new SerializedScpConfig((int)RoleType.Scp106,25f,10f,false,new List<SerializedItem>()),
            new SerializedScpConfig((int)RoleType.Scp93953,100f,25f,false,new List<SerializedItem>()),
            new SerializedScpConfig((int)RoleType.Scp93989,100f,25f,false,new List<SerializedItem>())
        };
    }

    public class SerializedScpConfig
    {
        public SerializedScpConfig() { }

        public SerializedScpConfig(int id,float killhp,float timehp,bool regenerateovermax,List<SerializedItem> drops) { Id = id;Killhp = killhp; TimeHp = timehp;Regenerateovermax = regenerateovermax; Drops = drops; }

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
