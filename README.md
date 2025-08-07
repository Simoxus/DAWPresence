![](https://cdn.myuuiii.com/projects/dawpresence/DAWRichPresence_v3.png)

A Discord Rich Presence app for several DAWs like FL Studio and Ableton.

| :----------------------------------------: | :------------------------------------------------: |

## How to use

- Make sure you have the latest .NET Desktop Runtime installed. You can download it [here through the official Microsoft website](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-desktop-8.0.6-windows-x64-installer).

- Download the latest release from the [releases](https://github.com/Myuuiii/DAWPresence/releases/) tab.
- Run the executable to create initial configuration files. The software will continute to run in the background.
- Close the software by running to executable again. You can change the config to your liking.
- Upon running the software, if it has detected a DAW that is currently supported, your Discord presence should be updated automatically.

## Custom Image Key

Some people might want a custom image on the rich presence. To bump the project a bit, I am making this exclusive to people that have starred this repository. For those that have, please contact `myuuiii` on Discord for more information. An example is shown below

![](https://ss.myuuiii.com/7634c47d-db45-4323-bc5c-7c6ab1993ea3.png)

###### config.yml

```yml
UpdateInterval: 00:00:03
Offset: 00:00:00
IdleText: Not working on a project
WorkingPrefixText: 'Working on '
UseCustomImage: true
CustomImageKey: myuuiii
DisablePopup: false
```

