#FlappyRogue-MG
FlappyRogue is a Monogame-based Flappy bird game, roguelike, made with MonoGame

## Installing dependencies
This project has been built and tested with .Net 6.0
1. Setup Visual Stusio 2022 zith this guide: https://docs.monogame.net/articles/getting_started/2_choosing_your_ide_visual_studio.html
  - dont forget the Monogame.Extended & Monogame.Extended.Content.Pipeline plugins
3. Commands to install monogame (requires a dotnet runtime)
```sh
dotnet new install MonoGame.Templates.CSharp --add-source https://git.aristurtle.net/api/packages/aristurtle/nuget/index.json
dotnet add package MonoGame.Extended --version 4.0.0
dotnet add package MonoGame.Extended.Content.Pipeline --version 4.0.0
```
