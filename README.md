# Destruction Derby II - ScoreBoard Race Mod
------------

### What is it?
Hi, my name is ChuZZZta and this project is for one of my favorite childhood game. Destruction Derby II is old school game from 1996 but it lacks few crucial features:
- Lap numbers are static - no way to change them
- Lack of ingame scoreboard - only in chempionship mode after race
- Only three cars avaliable - no way to legit control CPU cars
- Many more...

This mod actually try to fix those issues and provide better gameplay. :)

------------

### How does it work and look?
It is just standalone app written in C# which use memory read/write features to manipulate game data and provide third part race score system. Also it include few other usefull modes.
##### Example youtube video
> Comming soon

------------

### How to use it?
1. Check current release and download it
2. Unpack it using 7zip
3. Start the game and race
4. Alt + tab and start the app
5. Select JSON driver file
6. Select DD2 process (or dos/psx emulator if supported)
7. Load config
8. If you wanna use lap mode, select map, type new lap number, select checkbox for lapmode

##### Youtube tutorial video
> Comming soon

------------

### Supported versions
##### Current support
- Windows 7 or newer (to run DD2 on newer system than Windows 98 you need to have special exe version, check my youtube channel for more info I am planning to upload tutorial for getting one)

##### Planed future support (work in progress)
- DosBox emulation (DOSBox)
- Play Station emulation (ePSX)

##### "I wanna to support other version"

Supporting other versions means getting a correct memory adresses for date (using for example programs such as CheatEngine) and providing that data to JSON driver config. You can play if you want, memory addresses in that file are for **position** of a driver and other addresses are calculated inside a DD2-SBR. If offsets does not match, than unfortunatly only option to make it work is hard code it.
In future I will post youtube video how to do it so once again check my youtube channel for more info.

------------


### Current release
#### v0.1 early alpha
##### 09.12.18

------------


### Known bugs
Too many to listed them all but i will try... ;)

- Scoreboard may be flickering a bit (duo to low refresh time, it refreshes only once per second)
- Ingame score is totaly random (prbly cannot fix it, i will try apply some overlay or smt in future)
- Lots of random processes in select list (app takes all system processes not only user ones - i will try to fix it but its not crucial)
- DD2 does not show in process list sometimes (you need to first launch a game, then a mod, i will try to fix it)
- Loooooots of app crashes (mostly duo to my bad code, but if you follow step by step the instruction u will prevend most of them, i will fix it sooner or later)
- Motorplex map is not supported (I dunno why but player data seams to be stored somewhere else in memory)
- Unfortunetly many others

------------


### Licence and copyright stuff
##### Used libs
Name  |Author| Source
------------- | ------------- | -------------
Json.NET  |JamesNK| https://github.com/JamesNK/Newtonsoft.Json

##### Other materials
Name  |Author| Source
------------- | ------------- | -------------
Info about tracks/drivers|Destruction Derby Wiki|http://destructionderby.wikia.com/wiki/Main_Page
Some code|People from stackoverflow/youtube/my friends|Too many to listed :)
Drivers pictures and overall all game code/name/data rights :)|Reflections / Psygnosis|Pictures are just printscreened from game menu and edited in paint  :DDD
##### Thanks
As author i need to say that this mod could not be done without outside materials. So i really thanks for all authors of them. But if you dont like that a used your code/library/materials contact me and I will delete it from this app. If i missed someone, contact me too, i will add you to list.

------------

### Some usefull links
##### My channel on youtube
https://www.youtube.com/channel/UCPDh7-ytuQNvxdUfF_hRpuQ
##### Destruction Derby Wiki
http://destructionderby.wikia.com/wiki/Main_Page
