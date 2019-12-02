# KAMI_Solver

This is the application used to find solutions of the KAMI game. 

Game link => [steam](https://store.steampowered.com/app/272040/KAMI/)

## How to Start

* Download the execution file from the [Release Page](https://github.com/C6H4Cl2/KAMI_Solver/releases)

or

* Clone this repo and compile it. See [Build Environment](https://github.com/C6H4Cl2/KAMI_Solver#build-environment) below for more infos.


## How to Use

> Note: This application does not require any administrator permission

![Demo](https://user-images.githubusercontent.com/10542217/69923348-ffd44380-1458-11ea-95ab-bed2be0bc77b.jpg)
    
1. X-axis and Y-axis of each tile
2. A tile. Click a tile to change its color
3. Select a color
4. Solve current board
5. Set maximum steps. Default value is -1, which means no limitation.
6. Set all tiles to one color.
7. Load xml files from the KAMI game. Those files are in "&lt;KAMI game installation path&gt;/puzzles/&lt;folder&gt;". There should be 8 folders: StageA, StageB, StageC, StageD, StageE, StagePat1, 5Col and Convolution.
8. Set the size of current board. 

> Note: if an xml file is loaded, then the board size will be adjusted automatically.

**For example:** 

1. load "SEL9_SevenMovesBlack.xml" from "&lt;KAMI game installation path&gt;/puzzles/StageE/"
2. Click "Solve"

![SEL9_SevenMovesBlack Demo](https://user-images.githubusercontent.com/10542217/69927975-fbb32080-146e-11ea-8bad-b85987612934.jpg)


## Build Environment

Use Visual Studio 2019 or later to open the sln file, then build and run.

You can use other IDEs. The IDE should support C# and WPF.

(No need to install extra packages.)


## How does the application find a solution

Please see wiki -- [How does it work](https://github.com/C6H4Cl2/KAMI_Solver/wiki/How-does-it-work)


## Further Improvements

So far this application can guarantee to find and return a solution with an appropriate maximum steps. However, in some cases, it can take longer time than expectation to find the result. 

Please see wiki -- [Further Improvements](https://github.com/C6H4Cl2/KAMI_Solver/wiki/Further-Improvements)

