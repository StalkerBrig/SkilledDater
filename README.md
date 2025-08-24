# SkilledDater
 Skilled Dater Game - Prototype.

Project currently abandoned.


## Notable Code

### Scripts Folder
[https://github.com/StalkerBrig/SkilledDater/tree/main/Assets/Scripts](https://github.com/StalkerBrig/SkilledDater/tree/main/Assets/Scripts)

This has all the code made for equipment/stat calculations/etc.

### StatManager
[https://github.com/StalkerBrig/SkilledDater/blob/main/Assets/Scripts/Stats/Manager/StatManager.cs](https://github.com/StalkerBrig/SkilledDater/blob/main/Assets/Scripts/Stats/Manager/StatManager.cs)

This is how to "trigger" updates for stats.
I.e. if you add a new piece of equipment, it will see that and then call the corresponding code to update the stats.

### PlayerStats
[https://github.com/StalkerBrig/SkilledDater/tree/main/Assets/Scripts/Stats/PlayerStats](https://github.com/StalkerBrig/SkilledDater/blob/main/Assets/Scripts/Stats/PlayerStats/PlayerCurrentStatsSO.cs)

This shows how the calculations for updating stats works. 
I.e. if something adds new stats, this is the code that will recalculate everything.

