namespace ConsoleApp21;
public static class Helper
{
   public static bool IsNearGameplayEntity(int row, int column, List<GridOfRooms.GamePlayEntity> gameplayEntities)
   {
      foreach (GridOfRooms.GamePlayEntity a in gameplayEntities)
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

  

   public static void ChooseChallenge(string userInput)
   {
      switch (userInput)
      {
         case "pits":
         {
            
         }
            break;
         case "maelstrom":
         {
            
         }
            break;
         case "amarok":
         {
            
         }
            break;
         case "getting armed":
         {
            
         }
            break;
      }
   }
   
   public static void Message(string message, ConsoleColor color =  ConsoleColor.White)
   {
      Console.ForegroundColor = color;
      Console.WriteLine(message);
   }
   
   public static GridOfRooms PrepareGrid()
   {
      GridOfRooms gridOfRooms = null;
      
      
      do
      {
         Console.WriteLine("Do you want to play a small, medium, or large game?");
         string userAnswer = Console.ReadLine().Trim().ToLower();
    
         switch (userAnswer)
         {
            case "small":
               gridOfRooms = new GridOfRooms(4,4);
               gridOfRooms.SetFountain(0, 2);
               gridOfRooms.SetPit(3, 3);
               gridOfRooms.SetMaelstrom(2,1);
               gridOfRooms.SetAmarok(2,0);
               
               break;
            case "medium":
               gridOfRooms = new GridOfRooms(6,6);
               gridOfRooms.SetFountain(4, 2);
               gridOfRooms.SetPit(5,0);
               gridOfRooms.SetPit(5,4);
               gridOfRooms.SetMaelstrom(3,1);
               gridOfRooms.SetAmarok(3,2);
               gridOfRooms.SetAmarok(3,5);
               break;
            case "large":
               gridOfRooms = new GridOfRooms(8,8);
               gridOfRooms.SetFountain(4, 6);
               gridOfRooms.SetPit(6,4);
               gridOfRooms.SetPit(7,2);
               gridOfRooms.SetPit(7,4);
               gridOfRooms.SetPit(1,3);
               gridOfRooms.SetMaelstrom(5, 2);
               gridOfRooms.SetMaelstrom(6,0);
               gridOfRooms.SetAmarok(3,1);
               gridOfRooms.SetAmarok(2,5);
               gridOfRooms.SetAmarok(4,6);
               break;
            default:
               Console.WriteLine("Sorry, you do not have a valid option.");
               break;
         }

      } while (gridOfRooms == null);
      
      return gridOfRooms;
   }
   
   
}