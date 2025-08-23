A fork of the Discord Rich Presence app for DAW status displays (FL Studio, Ableton, and more), expanded with numerous QoL features

_Please show [Myuuiii](https://github.com/Myuuiii) some love for their hard work, they're a great developer :)_
##### ^ also this repository might be outdated since she's making a ton of great changes :joy:

## Supported DAWs

- Ableton 9-12
- FL Studio
- REAPER
- Renoise
- Studio One
- Bitwig Studio
- Reason 13
- Nuendo 13
- Cubase 13

## Usage

**Note:** *If you run into issues with running the application, make sure you have the .NET Desktop Runtime 8.0.6 installed. You can download it [here, through the official Microsoft website](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-desktop-8.0.6-windows-x64-installer).*

- Download the latest release from the [releases](https://github.com/Simoxus/DAWPresence/releases/) tab.
- Run the install wizard to unpack the application.
- Once you launch DAWPresence, the application will continute to run in the background. You can disable this popup in the **config.yml** file.
- Close the software by running the executable again.
- Upon running the software, if it has detected a DAW that is currently supported, your Discord presence will be updated automatically.

## Performance
This application is pretty lightweight, and only uses about 10 MB of RAM consistently. Checking for updates or enabling the Debug option may increase it to 15 MB temporarily.

## Configuration
*This **config.yml** file should be inside of /AppData/Roaming/DAWPresence. If it isn't, make sure to run the application at least once so it can generate the file/directory.*

```yml
UpdateInterval: 00:00:03
Offset: 00:00:00
IdleText: Not working on a project
WorkingPrefixText: ''
UseCustomImage: false
CustomImageKey: myuuiii
OpenOnStartup: false
TrayIcon: true
DisablePopup: false
CheckForUpdates: false
Debug: false
```

## Special Thanks
**Myuuiii**: For making the darn thing :D

**Simoxus**: For improving the darn thing (yet only slightly lol) :)
