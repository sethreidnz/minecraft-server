# MinecraftServer

This project was generated using the `create-dotnet-react-app` command line tool. This means the project contains:

- In `src/MinecraftServer.Web`A ASP.NET Core MVC app created using [`dotnet new react`](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-new?tabs=netcore21) that will serve up the frontend application and serve as an API.
- In `src/MinecraftServer.Web/ClientApp` a React.js app that was generated using [`create-react-app`](https://github.com/facebook/create-react-app). For more information on usage of `create-react-app` see their [user guide](https://facebook.github.io/create-react-app/).

You can deploy this to azure using the button below. Currently it will install the Blightfall mod.

[![Deploy to Azure](https://azurecomcdn.azureedge.net/mediahandler/acomblog/media/Default/blog/deploybutton.png)](https://azuredeploy.net/)

## Getting started

For frontend developers it recommended to use [Visual Studio Code](https://code.visualstudio.com/) and for backend developers [Visual Studio](https://visualstudio.microsoft.com/vs/).

### Visual Studio Code

To run the project you will need the following installed in addition to Visual Studio Code:

- [.NET Core SDK](https://dotnet.microsoft.com/download)
- [C# extension for Visual Studio Code](https://marketplace.visualstudio.com/items?itemName=ms-vscode.csharp)

To run the project with Visual Studio Code open this folder in vscode and press f5. This will launch backend and the front end together and load in your browser.

### Visual Studio

To run the project with Visual Studio open this solution file `src\MinecraftServer.sln` in Visual Studio and press f5. This will launch backend and the front end together and load in your browser.

## Project structure

The project structure is as follows with READMEs in some of the folders to explain further their purpose and intent.

``` bash
.appveyor.yml # the appveyor build file
/src # where all the source files go

# .NET
/src/MinecraftServer.Web.sln # the .NET  solution file
/src/MinecraftServer.Web # the .NET Core MVC web app

# ClientApp
/src/MinecraftServer.Web/ClientApp # The React.js client app
/src/MinecraftServer.Web/ClientApp/src/api # for functions that call the api
/src/MinecraftServer.Web/ClientApp/src/assets # for shared assets
/src/MinecraftServer.Web/ClientApp/src/components # for shared components
/src/MinecraftServer.Web/ClientApp/src/pages # for each 'page' of the site with it's own subfolders and components
```

## Useful links

- [Create React App User Guide](https://facebook.github.io/create-react-app)
- [.NET Core CLI](https://docs.microsoft.com/en-us/dotnet/core/tools/?tabs=netcore2x)