using System.Drawing;
using ConsoleApp21;
using Type = System.Type;


GridOfRooms grid = Helper.PrepareGrid();

int currentRow = 0;
int currentColumn = 0;
int targetRow = 0;
int targetColumn = 0;
int amountOfArrows = 5;
bool isFountainEnabled = false;

while (true)
{
    if (grid.IsOutOfArrows(amountOfArrows))
    {
        Helper.Message("You out of arrows so you can no longer shoot monsters.");
    }
    if (!grid.IsGameplayEntity(currentRow, currentColumn, GridOfRooms.Maelstrom))
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

    if (grid.IsGameplayEntity(currentRow, currentColumn, GridOfRooms.Amarok))
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
    
    if (grid.IsGameplayEntity(currentRow, currentColumn, GridOfRooms.Fountain))
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

    if (grid.IsGameplayEntity(currentRow, currentColumn, GridOfRooms.Pit))
    {
        Helper.Message("You've died in the pit, game over.");
        break;
    }

    if (grid.IsNearPit(currentRow, currentColumn))
    {
        Helper.Message("You feel a draft. There is a pit in a nearby room.");
    }

 
    
    if (grid.IsGameplayEntity(currentRow, currentColumn, GridOfRooms.Maelstrom))
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
            if (!grid.IsNearBorder(currentColumn))
            {
                currentColumn += 1;
            }
            break;
        case "move west":
            if (!grid.IsNearBorder(currentColumn))
            {
                currentColumn -= 1;
            }
            break;
        case "move north":
            if (!grid.IsNearBorder(currentRow))
            {
                currentRow -= 1;
            }
            break;
        case "move south":
            if (!grid.IsNearBorder(currentRow))
            {
                currentRow += 1;
            }
            break;
        case "enable fountain":
            if (grid.IsGameplayEntity(currentRow, currentColumn, GridOfRooms.Fountain))
            {
               isFountainEnabled = true;
            }
            break;
        case "shoot north":
        {
            targetRow = currentRow;
            
            if (!grid.IsNearBorder(currentRow) && !grid.IsOutOfArrows(amountOfArrows))
            {
                amountOfArrows -= 1; 
                targetRow -= 1;
                
                grid.IsMonsterShot(amountOfArrows, targetRow, targetColumn);
            }
        }
           break;
        case "shoot south":
        {
            targetRow = currentRow;
            
            if (!grid.IsNearBorder(currentRow) && !grid.IsOutOfArrows(amountOfArrows))
            {
                amountOfArrows -= 1; 
                targetRow += 1;
                
                grid.IsMonsterShot(amountOfArrows, targetRow, targetColumn);
            }
        }
            break;
        case "shoot east":
        {
            targetColumn = currentColumn;

            if (!grid.IsNearBorder(currentColumn) && !grid.IsOutOfArrows(amountOfArrows))
            {
                amountOfArrows -= 1; 
                targetColumn += 1;
                
                grid.IsMonsterShot(amountOfArrows, targetRow, targetColumn);
            }
            break;
        }
        case "shoot west":
        {
            targetColumn = currentColumn;
            
            if (!grid.IsNearBorder(currentColumn) && !grid.IsOutOfArrows(amountOfArrows))
            {
                amountOfArrows -= 1; 
                targetColumn -= 1;
                
                grid.IsMonsterShot(amountOfArrows, targetRow, targetColumn);
            }
            break;
        }
    }
}
