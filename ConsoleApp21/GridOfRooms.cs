namespace ConsoleApp21;

public class GridOfRooms
{
    private string?[,] rooms;
    
    public const string Maelstrom = "Maelstrom"; 
    public const string Pit = "Pit";
    public const string Fountain = "Fountain";
    public const string Amarok = "Amarok";
 
    public List<GamePlayEntity> pits = new();
    public List<GamePlayEntity> maelstroms = new();
    public List<GamePlayEntity> amaroks = new();

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
    

    public void RemoveAmarok(int row, int column)
    {
        var currentAmarok = GetGamePlayEntity(row, column, amaroks);
        amaroks.Remove(currentAmarok);
        rooms[row, column] = "";
    }

    private static bool IsNearGameplayEntity(int row, int column, List<GridOfRooms.GamePlayEntity> gameplayEntities)
    {
        foreach (var a in gameplayEntities)
        {
            for (int c = a.Column - 1; c <= a.Column + 1; c++)
            {
                for (int r = a.Row - 1; r <= a.Row + 1; r++)
                {
                    if (c == column && r == row)
                    {
                        return true;
                    }
                }
            }
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

    public GamePlayEntity GetGamePlayEntity(int row, int column, List<GridOfRooms.GamePlayEntity> gameplayEntities)
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

    public bool IsNearAmarok(int row, int column)
    {
        return IsNearGameplayEntity(row, column, amaroks);
    }

    public bool IsNearPit(int row, int column)
    {
        return IsNearGameplayEntity(row, column, pits);
    }

    public bool IsNearMaelstrom(int row, int column)
    {
       return IsNearGameplayEntity(row, column, maelstroms);
    }
}
