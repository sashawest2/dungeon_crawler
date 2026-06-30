using System.Drawing;
using ConsoleApp21;


GridOfRooms grid = Helper.PrepareGrid();

int currentRow = 0;
int currentColumn = 0;
int targetRow = 0;
int targetColumn = 0;
int amountOfArrows = 5;
bool isFountainEnabled = false;

do
{
    if (grid.IsOutOfArrows(amountOfArrows))
    {
        Helper.Message("You out of arrows so you can no longer shoot monsters.");
    }
    if (!grid.IsMaelstrom(currentRow, currentColumn))
    {
        Helper.Message($"You are in the room at (Row={currentRow}, Column={currentColumn}).");
    }
    
    if (currentRow == 0 && currentColumn == 0)
    {
        if (!isFountainEnabled)
        {
            Helper.Message("You see light coming from the cavern entrance.", ConsoleColor.Yellow);
        }
        else
        {
            Helper.Message("The Fountain of Objects has been reactivated, and you have escaped with your life!",
                ConsoleColor.Magenta);
            Helper.Message("You won", ConsoleColor.Magenta);
            break;
        }
    }

    if (grid.IsAmarok(currentRow, currentColumn))
    {
        Helper.Message("You are in the room with amarok, he kills you", 
            ConsoleColor.DarkRed);
        break;
    }

    if (grid.IsNearAmarok(currentRow, currentColumn))
    {
        
        Helper.Message("You can smell the rotten stench of an amarok in a nearby room.",
            ConsoleColor.Yellow);
    }
    
    if (grid.IsFountain(currentRow, currentColumn))
    {
        if (!isFountainEnabled)
        {
            Helper.Message("You hear water dripping in this room. The Fountain of Objects is here!",
                ConsoleColor.Blue);
        }
        else
        {
            Helper.Message("You hear the rushing waters from the Fountain of Objects. It has been reactivated!",
                ConsoleColor.Blue);
        }
    }

    if (grid.IsPit(currentRow, currentColumn))
    {
        Helper.Message("You've died in the pit, game over.");
        break;
    }

    if (grid.IsNearPit(currentRow, currentColumn))
    {
        Helper.Message("You feel a draft. There is a pit in a nearby room.");
    }

 
    
    if (grid.IsMaelstrom(currentRow, currentColumn))
    { 
        Helper.Message($"You are in the room at (Row={currentRow}, Column={currentColumn}).");
       grid.TeleportMaelstrom(currentRow, currentColumn);
       grid.TeleportPlayer(ref currentRow, ref currentColumn);
       Helper.Message("You've teleported into another place through the maelstrom.");
       Helper.Message($"You are in the room at (Row={currentRow}, Column={currentColumn}).");
    }

    if (grid.IsNearMaelstrom(currentRow, currentColumn))
    {
        Helper.Message("You hear the growling and groaning of a maelstrom nearby.",
            ConsoleColor.DarkCyan);
    }
    
    Helper.Message($"You have {amountOfArrows} arrows left.", ConsoleColor.DarkYellow);
    
    Helper.Message("What do you want to do?");

    

    Console.ForegroundColor = ConsoleColor.Cyan;
    string userInput = Console.ReadLine().Trim().ToLower();
    
    
    
    Helper.Message("----------------------------------------------------------------------------------");

    switch (userInput)
    {
        case "move east":
            if (currentColumn != grid._gridSize - 1)
            {
                currentColumn += 1;
            }
            else
            {
                Helper.Message("Your input out of range.");
            }
            break;
        case "move west":
            if (currentColumn != 0)
            {
                currentColumn -= 1;
            }
            else
            {
                Helper.Message("Your input out of range.");
            }
            break;
        case "move north":
            if (currentRow != 0)
            {
                currentRow -= 1;
            }
            else
            {
                Helper.Message("Your input out of range.");
            }
            break;
        case "move south":
            if (currentRow != grid._gridSize - 1)
            {
                currentRow += 1;
            }
            else
            {
                Helper.Message("Your input out of range.");
            }
            break;
        case "enable fountain":
            if (grid.IsFountain(currentRow, currentColumn))
            {
               isFountainEnabled = true;
            }
            break;
        case "shoot north":
        {
            targetRow = currentRow;
            targetColumn = currentColumn;
            
            if (currentRow != 0)
            {
                targetRow -= 1;
                if (grid.IsMonsterShot(amountOfArrows, targetRow, targetColumn))
                {
                    Helper.Message("You defeated a monster.", ConsoleColor.DarkRed);
                }
                else if (amountOfArrows != 0)
                {
                    Helper.Message("You didn't shot a monster.", ConsoleColor.Cyan);
                }
            }
            else
            {
                Helper.Message("You hear that your arrow went somewhere out of the forest");
            }
            
            if (amountOfArrows > 0)
            {
                amountOfArrows -= 1; 
            }
        }
           break;
        case "shoot south":
        {
            targetRow = currentRow;
            targetColumn = currentColumn;
            
            if (currentRow != grid._gridSize - 1)
            {
                targetRow += 1;
                if (grid.IsMonsterShot(amountOfArrows, targetRow, targetColumn))
                {
                    Helper.Message("You defeated a monster.", ConsoleColor.DarkRed);
                }
                else if (amountOfArrows != 0)
                {
                    Helper.Message("You didn't shot a monster.", ConsoleColor.Cyan);
                }
            }
            else
            {
                Helper.Message("You hear that your arrow went somewhere out of the forest");
            }
            
            if (amountOfArrows > 0)
            {
                amountOfArrows -= 1; 
            }
        }
            break;
        case "shoot east":
        {
            targetRow = currentRow;
            targetColumn = currentColumn;

            if (currentColumn != grid._gridSize - 1)
            {
                targetColumn += 1;
                if (grid.IsMonsterShot(amountOfArrows, targetRow, targetColumn))
                {
                    Helper.Message("You defeated a monster.", ConsoleColor.DarkRed);
                }
                else if (amountOfArrows != 0)
                {
                    Helper.Message("You didn't shot a monster.", ConsoleColor.Cyan);
                }
            }
            else
            {
                Helper.Message("You hear that your arrow went somewhere out of the forest");
            }
            
            if (amountOfArrows > 0)
            {
                amountOfArrows -= 1; 
            }
            break;
        }
        case "shoot west":
        {
            targetRow = currentRow;
            targetColumn = currentColumn;
            
            if (currentColumn != 0)
            {
                targetColumn -= 1;
                if (grid.IsMonsterShot(amountOfArrows, targetRow, targetColumn))
                {
                    Helper.Message("You defeated a monster.", ConsoleColor.DarkRed);
                }
                else if (amountOfArrows != 0)
                {
                    Helper.Message("You didn't shot a monster.", ConsoleColor.Cyan);
                }
            }
            else
            {
                Helper.Message("You hear that your arrow went somewhere out of the forest");
            }
            
            if (amountOfArrows > 0)
            {
                amountOfArrows -= 1; 
            }
            break;
        }
    }
} while (true);
