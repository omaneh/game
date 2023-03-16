using System;

class Program
{
    static void Main(string[] args)
    {
        Player player = new Player();

        for (int i = 0; i < 25; i++)
        {
            Console.WriteLine("Where do you want to move?");
            string? userAnswer = Console.ReadLine();

            if (userAnswer == "north")
            {
                player.PlayerHorizontalMove(player.North);
            }
            else if (userAnswer == "south")
            {
                player.PlayerHorizontalMove(player.South);
            } else if (userAnswer == "east")
            {
                player.PlayerMoveVertical(player.East);
            } else if (userAnswer == "west")
            {
                player.PlayerMoveVertical(player.West);
            }

            if(player.RoomReached()) {
                player.EnabledFountain(); 
                }
            player.DisplayGrid();
        }
        Console.ReadLine();
    }
}

public class Game : IRoom
{
    internal int Row { get; set; }
    internal int Column { get; set; }
    internal char?[,] GameGrid { get; } = { { null, null, null, null }, { null, null, null, null }, { null, null, null, null }, { null, null, null, null }, { null, null, null, null } };
    internal char PlayerLocater { get; set; } = 'x';

    public void DisplayGrid()
    {
        
        Console.WriteLine($"Player column: {Column} Player row: {Row}");

        char? location;
        for (int i = 0; i< 5; i++)
        {
            for (int j = 0; j< 4; j++)
            {
                location = GameGrid[i, j];
                if (i >= 1)
                {
                    Console.Write("|" + "  " + location + "  " + "|");
                } else {
                    Console.Write("  " + location);
                }

            }
            Console.WriteLine("\n------------------------");
        }
    }


    public bool ValidMove()
    {
        if (Column > 4 || Column < 0 || Row > 3 || Row < 0 )
        {
            Console.WriteLine($"Not a valid move");
            return false;
        } else
        {
            Console.WriteLine($"I validated this.. {Column} {Row}");
            return true;
        }
    }
}

public class Player: Room
{

    internal int North { get; set; } = -1;
    internal int South { get; set; } = +1;
    internal int East { get; set; } = 1;
    internal int West { get; set; } = -1;

    public void PlayerHorizontalMove(int direction)
    {
            GameGrid[Column, Row] = null;
            // where to place this function that it make sense...??
            if(ValidMove()) {
                 Column += direction;
                GameGrid[Column, Row] = PlayerLocater;
            }

    }

    public void PlayerMoveVertical(int direction)
    {

            GameGrid[Column, Row] = null;
            
            if(ValidMove()) {
                 Row += direction;
                GameGrid[Column, Row] = PlayerLocater;
            }
    }


}
public interface IRoom
{
    public void RoomBuilder() { }
   
}
public class Room : Game, IRoom
{
    public bool RoomReached()
    {
        if (Column == 1 && Row == 0)
        {
            Console.WriteLine("You see light coming from the cavern entrance");
            return false;
        }
        else if (Column ==2 && Row == 1)
        {
            Console.WriteLine("You hear water dripping in this room. The fountain of Objects is here!");
            Console.WriteLine("Would you like to enable the fountain?");
            string? userInput = Console.ReadLine();
            return true;
        }
        else if (Column ==1 && Row == 1 || Column == 1 && Row == 2 || Column == 2 && Row == 2)
        {
            Console.WriteLine("You hear the stomach of the wumpus grumbling");
            return false;
        }
        else if (Column ==3 && Row ==  2)
        {
            Console.WriteLine("You stumbled onto the hungry wumpus who takes a mere glance before launching itself at you. You have been devoured by the wumpus. \nGame over.");
            GameOver();
            return false;
        } else {
            Console.WriteLine("This room is empty");
            return false;
        }
    }

   public void EnabledFountain()
        {
            if (Column == 1 && Row == 0)
            {
                Console.WriteLine("The Fountain Of Objects has been reactivated, and you have esaped wtih your life. You win!");
                GameOver();
            }
            else if (Column == 2 && Row == 1)
            {
                Console.WriteLine("You hear water dripping in this room. The fountain of Objects is here and enabled. Return to the entrance.");
            }
            else if (Column == 1 && Row == 1 || Column == 1 && Row == 2 || Column == 2 && Row == 2)
            {
                Console.WriteLine("You hear the stomach of the wumpus grumbling");

            }
            else if (Column == 2 && Row == 2)
            {
                Console.WriteLine("You stumbled onto the hungry wumpus who takes a mere glance before launching itself at you. You have been devoured by the wumpus. \nGame over.");
                GameOver();
            }
        }
        public static bool GameOver() => true;
}
