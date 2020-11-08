## BetterScp
BetterScp is a Synapse 2 Plugin for Scp:Sl that adds a command to the game to see all living Scps and swap with them (also with CustomScps like [056](https://github.com/SynapseSL/Scp056)).

Also does it adds a config for configuring how much Health scps get each kill/ 30 seconds,if it can regenaret over max Health and what items (also in which size) should it drop

## Credits
* [Cynaox](https://github.com/Cyanox62) : I used a majority of his code from the [scpswap](https://github.com/Cyanox62/SCPSwap) plugin for the .scp swap command

## Config
| Config | Type | Default | Description |
| :--: | :--: | :--: | :--: |
| SwapRequestTimeout | Float | 30 | After that amount of time scp's can' swap anymore |
| AllowNewScps | Boolean | true | If set to true an scp can change his class to a Scp which doen't exist yet. |
| BlackListedScps | Integer List | 10 | With which Scps cant you swap |
| ScpConfigs | SerializedScpConfig List | default | the configuration list for Scps (you can also configure CustomScp's like 056 by using it's id 56)

## Command
```
.scp list    => Gives you as Scp a list of all currently living Scp's
.scp swap id => Sends a swap request
```
