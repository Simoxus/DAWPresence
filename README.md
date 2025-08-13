A Discord Rich Presence app for several DAWs like FL Studio and Ableton.

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

- Make sure you have the latest .NET Desktop Runtime installed. You can download it [here through the official Microsoft website](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-desktop-8.0.6-windows-x64-installer).

- Download the latest release from the [releases](https://github.com/Simoxus/DAWPresence/releases/) tab.
- Run the install wizard to unpack the application.
- Once you launch DAWPresence, the application will continute to run in the background.
- Close the software by running the executable again. You can change the config to your liking using config.yml
- Upon running the software, if it has detected a DAW that is currently supported, your Discord presence should be updated automatically.

## Configuration
*Make sure* **config.yml** *is in the same directory as the application.*

```yml
UpdateInterval: 00:00:03
Offset: 00:00:00
IdleText: Not working on a project
WorkingPrefixText: 'Working on '
UseCustomImage: true
CustomImageKey: myuuiii
CheckForUpdates: false
DisablePopup: false
```
