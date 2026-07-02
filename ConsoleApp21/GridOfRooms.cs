namespace ConsoleApp21;

public class GridOfRooms
{
    private string?[,] rooms;
    
    public const string Maelstrom = "Maelstrom"; 
    public const string Pit = "Pit";
    public const string Fountain = "Fountain";
    public const string Amarok = "Amarok";
 
    private List<GamePlayEntity> pits = new();
    private List<GamePlayEntity> maelstroms = new();
    private List<GamePlayEntity> amaroks = new();

    public class GamePlayEntity
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public Type Type { get; }

        public GamePlayEntity(int row, int column, Type type)
        {
            Type = type;
            Row = row;
            Column = column;
        }
    }
    public int _gridSize {get; }

    public GridOfRooms(int row, int column)
    {
        rooms = new string?[row, column];
        _gridSize = row;
    }
    
    public bool IsNearBorder(int coordinate)
    {
        if (coordinate == 0 || coordinate == _gridSize - 1)
        {
            Helper.Message("Your input out of range.");
            return true;
        }
        return false;
    }
    
    public void SetMaelstrom(int row, int column)
    { 
        rooms[row, column] = "Maelstrom";
        GamePlayEntity m = new GamePlayEntity(row, column, Type.Maelstrom);
        maelstroms.Add(m);
    }

    public void SetAmarok(int row, int column)
    {
        rooms[row, column] = "Amarok";
        GamePlayEntity a = new GamePlayEntity(row, column, Type.Amarok);
        amaroks.Add(a);
    }
    
    public void TeleportMaelstrom(int row, int column)
    {
        int tempColumn = (column + 2) % _gridSize;
        int tempRow = (row + 1) % _gridSize;

        rooms[row, column] = null;

 
            rooms[tempRow, tempColumn] = "Maelstrom";
            var currentMaelstrom = GetGamePlayEntity(row, column, maelstroms);
            currentMaelstrom.Row = row;
            currentMaelstrom.Column = column;
    
    }

    private GamePlayEntity GetGamePlayEntity(int row, int column, List<GridOfRooms.GamePlayEntity> gameplayEntities)
    {
        foreach (var gameplayEntity in gameplayEntities)
        {
            if (gameplayEntity.Row == row && gameplayEntity.Column == column)
            {
                return gameplayEntity;
            }
        }
        throw new Exception("Gameplay Entity not found");
    }

    public void TeleportPlayer(ref int row, ref int column)
    {
        if (row == 0)
        {
            row = _gridSize - 1;
        }
        else
        {
            row -= 1;
        }

        for (int i = 0; i <= 1; i++)
        {
            if (column == _gridSize - 1)
            {
                column = 0;
            }
            else
            {
                column += 1;
            }
        }
    }
    
    public void SetPit(int row, int column)
    {
        rooms[row, column] = "Pit";
        GamePlayEntity p = new GamePlayEntity(row, column, Type.Pit);
        pits.Add(p);
    }

    public void SetFountain(int row, int column)
    {
        rooms[row, column] = "Fountain";
    }
    

    public bool IsGameplayEntity(int row, int column, string type)
    {
        return rooms[row, column] == type;
    }
    
    public bool IsOutOfArrows(int amountOfArrows)
    {
        if (amountOfArrows == 0)
        {
            Console.WriteLine("You are out of arrows.");
            return true;
        }
        return false;
    }

    public bool IsMonsterShot(int amountOfArrows, int row, int column)
    {
            if (IsGameplayEntity(row, column, GridOfRooms.Amarok))
            {
                var currentAmarok = GetGamePlayEntity(row, column, amaroks);
                amaroks.Remove(currentAmarok);
                rooms[row, column] = "";
                Helper.Message("You defeated a monster.", ConsoleColor.DarkRed);
                
                return true;
            }
            else
            {
                Helper.Message("You didn't shot a monster.", ConsoleColor.Cyan);
                return false;
            }
    }
    
    public bool IsNearAmarok(int row, int column)
    {
        return Helper.IsNearGameplayEntity(row, column, amaroks);
    }

    public bool IsNearPit(int row, int column)
    {
        return Helper.IsNearGameplayEntity(row, column, pits);
    }

    public bool IsNearMaelstrom(int row, int column)
    {
       return Helper.IsNearGameplayEntity(row, column, maelstroms);
    }
}
