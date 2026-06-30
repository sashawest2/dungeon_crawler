namespace ConsoleApp21;

public class GridOfRooms
{
    private string?[,] rooms;
    
    private const string Maelstrom = "Maelstrom";
    private const string Pit = "Pit";
    private const string Fountain = "Fountain";
    private const string Amarok = "Amarok";
 
    private List<GamePlayEntity> pits = new();
    private List<GamePlayEntity> maelstroms = new();
    private List<GamePlayEntity> amaroks = new();

    private class GamePlayEntity
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
            var currentMaelstrom = GetMaelstrom(row, column);
            currentMaelstrom.Row = row;
            currentMaelstrom.Column = column;
    
    }

    // private bool IsOccupied(int row, int column)
    // {
    //     if (IsMaelstrom(row, column) || IsPit(row, column) || IsFountain(row, column))
    //     {
    //         return true;
    //     }
    //     return false;
    // }

    private GamePlayEntity GetMaelstrom(int row, int column)
    {
        foreach (var maelstrom in maelstroms)
        {
            if (maelstrom.Row == row && maelstrom.Column == column)
            {
                return maelstrom;
            }
        }

        throw new Exception("Maelstrom not found");
    }

    private GamePlayEntity GetAmarok(int row, int column)
    {
        foreach (var amarok in amaroks)
        {
            if (amarok.Row == row && amarok.Column == column)
            {
                return amarok;
            }
        }
        throw new Exception("Amarok not found");
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

    public bool IsFountain(int row, int column)
    {
        return rooms[row, column] == "Fountain";
    }

    public bool IsMaelstrom(int row, int column)
    {
        return rooms[row, column] == "Maelstrom";
    }

    public bool IsAmarok(int row, int column)
    {
        return rooms[row, column] == "Amarok";
    }

    public bool IsPit(int row, int column)
    {
        return rooms[row, column] == "Pit";
    }

    public bool IsOutOfArrows(int amountOfArrows)
    {
        return amountOfArrows == 0;
    }

    public bool IsMonsterShot(int amountOfArrows, int row, int column)
    {
        if (!IsOutOfArrows(amountOfArrows))
        {
            if (IsAmarok(row, column))
            {
                var currentAmarok = GetAmarok(row, column);
                amaroks.Remove(currentAmarok);
                rooms[row, column] = "";
                
                return true;
            }
        }
        return false;
    }

    public bool IsNearAmarok(int row, int column)
    {
        foreach (GamePlayEntity a in amaroks)
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

    public bool IsNearPit(int row, int column)
    {
        foreach (GamePlayEntity p in pits)
        {
            for (int c = p.Column - 1; c <= p.Column + 1; c++)
            {
                for (int r = p.Row - 1; r <= p.Row + 1; r++)
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

    public bool IsNearMaelstrom(int row, int column)
    {
        foreach (GamePlayEntity m in maelstroms)
        {
            for (int c = m.Column - 1; c <= m.Column + 1; c++)
            {
                for (int r = m.Row - 1; r <= m.Row + 1; r++)
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

 
}