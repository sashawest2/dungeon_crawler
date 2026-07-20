namespace ConsoleApp21;
public static class Helper
{
   public static void Message(string message, ConsoleColor color =  ConsoleColor.White)
   {
      Console.ForegroundColor = color;
      Console.WriteLine(message);
   }
   

   private static string UserInput()
   {
      Console.WriteLine("Choose challenge you want to do: pits, maelstroms, amaroks " +
                        "(you can choose few of them, just enter them comma-separated)");
               
      return Console.ReadLine().Trim().ToLower();
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
               
               string userChallengeSmall = UserInput();

               if (userChallengeSmall.Contains("pits"))
               {
                  gridOfRooms.SetPit(3, 3);
                  Message("Look out for pits. You will feel a breeze if a pit is in an adjacent room. \n" +
                                 "If you enter a room with a pit, you will die.", ConsoleColor.DarkYellow);
               }

               if (userChallengeSmall.Contains("maelstroms"))
               {
                  gridOfRooms.SetMaelstrom(2,1); 
                  Message("Maelstroms are violent forces of sentient wind. \n" +
                                 "Entering a room with one could transport you to any other location in the caverns. \n" +
                                 "You will be able to hear their growling and groaning in nearby rooms.", ConsoleColor.DarkYellow);
               }

               if (userChallengeSmall.Contains("amaroks"))
               {
                  gridOfRooms.SetAmarok(2,0);
                  Message("Amaroks roam the caverns. Encountering one is certain death, \n" +
                          "but you can smell their rotten stench in nearby rooms.", ConsoleColor.DarkYellow);
                  
               }
               break;
            case "medium":
               gridOfRooms = new GridOfRooms(6,6);
               gridOfRooms.SetFountain(4, 2);
               
               string userChallengeMedium = UserInput();

               if (userChallengeMedium.Contains("pits"))
               {
                  gridOfRooms.SetPit(5,0);
                  gridOfRooms.SetPit(5,4);
               }

               if (userChallengeMedium.Contains("maelstroms"))
               {
                  gridOfRooms.SetMaelstrom(3,1);
               }

               if (userChallengeMedium.Contains("amaroks"))
               {
                  gridOfRooms.SetAmarok(3,2);
                  gridOfRooms.SetAmarok(3,5);
               }
               break;
            case "large":
               gridOfRooms = new GridOfRooms(8,8);
               gridOfRooms.SetFountain(4, 6);
               
               string userChallengeLarge = UserInput();
               
               if (userChallengeLarge.Contains("pits"))
               {
                  gridOfRooms.SetPit(6,4);
                  gridOfRooms.SetPit(7,2);
                  gridOfRooms.SetPit(7,4);
                  gridOfRooms.SetPit(1,3);
               }

               if (userChallengeLarge.Contains("maelstroms"))
               {
                  gridOfRooms.SetMaelstrom(5, 2);
                  gridOfRooms.SetMaelstrom(6,0);
               }

               if (userChallengeLarge.Contains("amaroks"))
               {
                  gridOfRooms.SetAmarok(3,1);
                  gridOfRooms.SetAmarok(2,5);
                  gridOfRooms.SetAmarok(4,6);
               }
               break;
            default:
               Console.WriteLine("Sorry, you do not have a valid option.");
               break;
         }

      } while (gridOfRooms == null);
      
      return gridOfRooms;
   }
   
   
}