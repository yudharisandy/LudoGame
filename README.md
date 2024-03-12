# LudoGame

This repository is part of The Bootcamp project of Formulatrix Software Engineering, at Formulatrix Company, Salatiga City, Indonesia.

## Table of Contents
- [Progress Update]()
- [Repository Foldering Structure]()
- [Game Features]()
- [Issues]()
- [Board Coordinate Scheme]()
- [How to Use]()
- [References]()

### Progress Update
![progress-game-app](assets/progress3.png)

### Repository Foldering Structure
```
LudoGame
├── Game
├── GameObject
├── LudoObjects
├── Utility
LudoGameGUI
└── bin
   └── Debug
      └── net8.0-windows
          └── LudoGameGUI.exe   
```

### Game Features
- Support for 2-4 players
- Provide a simple graphical user interface to be played around
- Support specific Ludo rules: 
    - Got number six: the same player holds
    - Collision rule: 
    - Specific path/route of play for each player    

### Issues (To be solved soon)
- Still working on collision rule
- Got number six: the same player holds

### Board Coordinate Scheme
This library is built based on the following board coordinate scheme.
![Board-Scheme](assets/ludoScheme.jpg)

### How to Use
- Register Player
```
bool status = _ludoGameScene.ludoContext.RegisterPlayers(_ludoPlayer);
```
- Second, ...
- Third, ...

### References
- [Class Diagram](https://github.com/probabilitynokami/ClassDiagram/blob/main/Ludo.md)
- [Formulatrix Bootcamp Repository](https://github.com/yudharisandy/Bootcamp-Formulatrix-CSharp)