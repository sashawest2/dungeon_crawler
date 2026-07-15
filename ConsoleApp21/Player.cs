namespace ConsoleApp21;

public class Player
{
    public int CurrentRow { get; set; }
    public int CurrentColumn { get; set; }
    
    public bool IsGettingArmed = false;
    public int AmountOfArrows { get; set; } = 5;
    

   
    private bool IsOutOfArrows()
    {
        if (AmountOfArrows == 0)
        {
            Console.WriteLine("You are out of arrows.");
            return true;
        }

        return false;
    }
   

    public bool ShootPossibilityCheck()
    {
        if (IsOutOfArrows())
        {
            Helper.Message("You out of arrows so you can no longer shoot monsters.");
            return false;
        }
        return true;
    }
    
    public (int, int) ShootInput(string userInput)
    {
        return userInput switch
        {
            "shoot north" => (CurrentRow - 1, CurrentColumn),
            "shoot south" => (CurrentRow + 1, CurrentColumn),
            "shoot east" => (CurrentRow, CurrentColumn + 1),
            "shoot west" => (CurrentRow, CurrentColumn - 1),
            _ => throw new Exception($"Unknown user input: {userInput}")
        };
    }

    public (int, int) MoveInput(string userInput)
    {
        int tempRow = CurrentRow;
        int tempColumn = CurrentColumn;
        
        return userInput switch
        {
            "move east" => (tempRow, tempColumn + 1),
            "move west" => (tempRow, tempColumn - 1),
            "move north" => (tempRow - 1, tempColumn),
            "move south" => (tempRow + 1, tempColumn),
            _ => throw new Exception($"Unknown user input: {userInput}")
        };
    }
}
