# Minesweeper in .NET

The classic game of Minesweeper built using C# and Windows Forms. This application contains all the base functionality of a Minesweeper clone, but also comes with optional hints + tips (explained below), which can be used to help you improve your play or to avoid annoying 50/50 guess situations.

## Instructions to Play

-   Select the game size under the `New Game` menu.
-   Left click opens the cell.
-   Right click flags/unflags a cell.
-   To win, correctly flag all cells that contain a mine.

## Hints and Tips

### **Show Mines**

This can be used to look at where the mines are located on the board. I would suggest you use this only in 50/50 situations to avoid losing a game to guessing. However, you can use this so fully cheat the game anytime you like...

### **Show Percentages**

This option shows you a calculated percentage for any cells we have information for as to their likelyhood of either being a mine `(100%)` or being safe `(0%)`.

### **Show Hints**

This option highlights cells when it is able to compute that a cell is always going to contain a mine (red) or always going to be safe (green).
