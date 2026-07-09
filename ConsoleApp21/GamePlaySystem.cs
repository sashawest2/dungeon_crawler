namespace ConsoleApp21;

public class GamePlaySystem
{
    private GridOfRooms grid;
    private Player player;
    private bool isFountainEnabled;

    private void PrepareGrid()
    {
        player = new Player();
        grid = Helper.PrepareGrid();
    }

    public void StartGame()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        PrepareGrid();
        MovePossibilityCheck();
        while (true)
        {
            string userInput = Console.ReadLine().Trim().ToLower();

            Helper.Message("----------------------------------------------------------------------------------");

            if (userInput.Contains("move"))
            {
                var coordinates = player.MoveInput(userInput);

                Move(coordinates.Item1, coordinates.Item2);
                bool moveResult = MovePossibilityCheck();
                if (!moveResult)
                {
                    break;
                }
            }
            else if (userInput.Contains("shoot"))
            {
                if (player.ShootPossibilityCheck())
                {
                    var coordinates = player.ShootInput(userInput);
                    Shoot(coordinates.Item1, coordinates.Item2);
                }
            }
            else if (userInput.Contains("enable"))
            {
                EnableFountain();
            }
            else
            {
                Helper.Message("Invalid input");
            }
        }
    }

    private void TeleportPlayer()
    {
        if (player.CurrentRow == 0)
        {
            player.CurrentRow = grid._gridSize - 1;
        }
        else
        {
            player.CurrentRow -= 1;
        }

        for (int i = 0; i <= 1; i++)
        {
            if (player.CurrentColumn == grid._gridSize - 1)
            {
                player.CurrentColumn = 0;
            }
            else
            {
                player.CurrentColumn += 1;
            }
        }
    }

    private void EnableFountain()
    {
        if (IsFountain())
        {
            Helper.Message("You hear the rushing waters from the Fountain of Objects. It has been reactivated!",
                ConsoleColor.Blue);
            isFountainEnabled = true;
        }
        else
        {
            Helper.Message("You can't enable fountain since you are in another place");
        }
    }

    private bool IsFountain()
    {
        if (grid.IsGameplayEntity(player.CurrentRow, player.CurrentColumn, GridOfRooms.Fountain))
        {
            return true;
        }

        return false;
    }

    private bool MovePossibilityCheck()
    {
        if (!grid.IsGameplayEntity(player.CurrentRow, player.CurrentColumn, GridOfRooms.Maelstrom))
        {
            Helper.Message($"You are in the room at (Row={player.CurrentRow}, Column={player.CurrentColumn}).");
        }

        if (player.CurrentRow == 0 && player.CurrentColumn == 0)
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
                return false;
            }
        }

        if (grid.IsGameplayEntity(player.CurrentRow, player.CurrentColumn, GridOfRooms.Amarok))
        {
            Helper.Message("You are in the room with amarok, he kills you",
                ConsoleColor.DarkRed);
            return false;
        }

        if (grid.IsNearAmarok(player.CurrentRow, player.CurrentColumn))
        {
            Helper.Message("You can smell the rotten stench of an amarok in a nearby room.",
                ConsoleColor.Yellow);
        }

        if (grid.IsGameplayEntity(player.CurrentRow, player.CurrentColumn, GridOfRooms.Fountain) && !isFountainEnabled)
        {
            Helper.Message("You hear water dripping in this room. The Fountain of Objects is here!",
                ConsoleColor.Blue);
        }

        if (grid.IsGameplayEntity(player.CurrentRow, player.CurrentColumn, GridOfRooms.Pit))
        {
            Helper.Message("You've died in the pit, game over.");
            return false;
        }

        if (grid.IsNearPit(player.CurrentRow, player.CurrentColumn))
        {
            Helper.Message("You feel a draft. There is a pit in a nearby room.");
        }


        if (grid.IsGameplayEntity(player.CurrentRow, player.CurrentColumn, GridOfRooms.Maelstrom))
        {
            Helper.Message($"You are in the room at (Row={player.CurrentRow}, Column={player.CurrentColumn}).");
            grid.TeleportMaelstrom(player.CurrentRow, player.CurrentColumn);
            TeleportPlayer();
            Helper.Message("You've teleported into another place through the maelstrom.");
            Helper.Message($"You are in the room at (Row={player.CurrentRow}, Column={player.CurrentColumn}).");
        }

        if (grid.IsNearMaelstrom(player.CurrentRow, player.CurrentColumn))
        {
            Helper.Message("You hear the growling and groaning of a maelstrom nearby.",
                ConsoleColor.DarkCyan);
        }

        Helper.Message($"You have {player.AmountOfArrows} arrows left.", ConsoleColor.DarkYellow);

        Helper.Message("What do you want to do?");

        return true;
    }

    private void Shoot(int targetRow, int targetColumn)
    {
        if (targetRow != player.CurrentRow)
        {
            if (!grid.IsNearBorder(player.CurrentRow))
            {
                player.AmountOfArrows -= 1;
                IsMonsterShot(targetRow, player.CurrentColumn);
                return;
            }
        }

        if (targetColumn != player.CurrentColumn)
        {
            if (!grid.IsNearBorder(player.CurrentColumn))
            {
                player.AmountOfArrows -= 1;
                IsMonsterShot(player.CurrentRow, targetColumn);
            }
        }
    }

    private void Move(int tempRow, int tempColumn)
    {
        if (tempRow < grid._gridSize && tempColumn < grid._gridSize
            && tempRow >= 0 && tempColumn >= 0)
        {
            player.CurrentRow = tempRow;
            player.CurrentColumn = tempColumn;
        }
    }

    private bool IsMonsterShot(int row, int column)
    {
        if (grid.IsGameplayEntity(row, column, GridOfRooms.Amarok))
        {
            grid.RemoveAmarok(row, column);
            Helper.Message("You defeated a monster.", ConsoleColor.DarkRed);

            return true;
        }

        Helper.Message("You didn't shot a monster.", ConsoleColor.Cyan);
        return false;
    }
}