# Project DD2-SbR
------------

### What is it?
Hi, my name is ChuZZZta and suddenly at the end of 2018 I decided to make a mod for my first game I ever played when I was a child. Destruction Derby II is old school game from 1996, it was released for DOS and PSX consoles, it is still a very fun way to spend free time but it lacks a few things:
- Lap numbers are static 
- Lack of ingame scoreboard
- Only three cars available
- Many more...

This mod actually tries to fix those issues and provide better gameplay. :) It is hard to list all my ideas how to improve this game, but as far as now, DD2-SbR Mod features:
 - Nice scoreboard in standalone app
 - Lap mode which allows players to change number of laps
 - Many more features coming soon... :)

------------

### How does it work and look?
It is just a standalone app written in C# which use memory read/write features to manipulate game data and provide additional score system. Also it include few other usefull
modes.
##### Example youtube video
> Comming soon

------------

### How to use it?
> Sorry, right now I can't provide instructions how to use it, because the app is changing frequently.

##### Youtube tutorial video
> Comming soon

------------

### Supported versions
##### Current support
- Windows 7 or newer (to run DD2 on newer systems than Windows 98 you need to have a special exe version, check my youtube channel for more info. I am planning to upload a tutorial for getting one)

##### Planed future support (work in progress)
- DosBox emulation (DOSBox)
- Play Station emulation (ePSX)

##### "I wanna to support other version"

Supporting other versions means getting a correct memory addresses for date (using for example programs such as CheatEngine) and providing that data to JSON driver config. You can play if you want, memory addresses in that file are for **position** of a driver and other addresses are calculated inside a DD2-SBR. If offsets do not match, then unfortunately the only option to make it work is hard code it.
In future I will post youtube video how to make it so once again check my youtube channel for more info.

------------


### Current release
##### Sorry, the app is still in development and I cannot provide a working version yet.
###### Planning release date: second quoter of 2019

------------


### Known bugs
Too many to list them all but i will try... ;)

- The scoreboard may be flickering a bit (due to low refresh rate, it refreshes only once per second)
- Ingame score is totally random (prbly cannot fix it, i will try to apply some overlay or smt in the future)
- Loooooots of app crashes (no exception mechanics)
- Motorplex map is not supported
-  Unfortunately, many others

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
Overall the game :)|Reflections / Psygnosis|Thank you for making great games. :)
##### Thanks
Thanks to everyone who contributed to this mod, I hope that it delivered what it promise.

------------

### Some usefull links
##### My channel on youtube
https://www.youtube.com/channel/UCPDh7-ytuQNvxdUfF_hRpuQ
##### Destruction Derby Wiki
http://destructionderby.wikia.com/wiki/Main_Page
