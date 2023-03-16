using System;

class Program
{
    static void Main(string[] args)
    {
        Player player = new Player();
        Console.WriteLine("Welcome to the fountain of objects!\n\nThe rules of this game are simple. Your task is to enable the fountain of objects inside the abandoned Power Plant and return back safely to the outside world. You are able to move north, south, east, or west. Staying within the bounds of the game. However, there is one caveat. Lurking within the shadows of these rooms lives a hungry ferious wumpus. If you are to encounter this wumpus, then I am afraid your time will have ended. Good luck. \n\n");
        player.DisplayGrid();
        for (int i = 0; i < 25; i++)
        {
            if (player.GameOver) {
                break;
            }
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
            
            
             player.PlayerMove(); 
             player.DisplayGrid();
            Console.Clear();
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
        if(Column == 0 && Row == 0) {
            GameGrid[Column, Row] = PlayerLocater;
        }
        
        Console.WriteLine($"\nPlayer column: {Column} Player row: {Row}\n");

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


    public bool ValidMove(int targetRow, int targetColumn)
    {
        if (targetColumn > 5 || targetColumn < 0 || targetRow > 4 || targetRow < 0 )
        {
            Console.WriteLine($"Not a valid move");
            return false;
        } else
        {
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
        int targetColumn = Column + direction;
            GameGrid[Column, Row] = null;
            if(ValidMove(targetColumn, Row)) {
                 Column += direction;
                GameGrid[Column, Row] = PlayerLocater;
             } else {
                GameGrid[Column, Row] = PlayerLocater;
            }

    }

    public void PlayerMoveVertical(int direction)
    {
        int targetRow = Row + direction;
            GameGrid[Column, Row] = null;
            
            if(ValidMove(Column, targetRow)) {
                 Row += direction;
                GameGrid[Column, Row] = PlayerLocater;
            } else {
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
    
    internal string Enabled {get; set;}
    internal bool GameOver {get; set;} = false;
    
    public void PlayerMove() 
    {
        if (Enabled == "yes") {
            EnabledFountain();
        } else {
            RoomReached();
        }
    }
    public void RoomReached()
    {
        if (Column == 1 && Row == 0)
        {
            Console.WriteLine("You see light coming from the cavern entrance");
        }
        else if (Column ==4 && Row == 1)
        {
            Console.WriteLine("You hear water dripping in this room. The fountain of Objects is here!");
            Console.WriteLine("Would you like to enable the fountain?");
            string? userInput = Console.ReadLine();
            Enabled = userInput;
            if (Enabled == "yes") {
                Console.WriteLine("Return to the exit!");
            }
        } else if (Column == 3 && Row == 0 || Column == 4 && Row == 0) {
            Console.WriteLine("You hear water dripping nearby");
        } else if (Column == 3 && Row == 1) {
            Console.WriteLine("Your ears pick up the drips of the fountain but your nose picks up the stench of a large hungry wumpus.");
        }
        else if (Column ==1 && Row == 1 || Column == 1 && Row == 2 || Column == 2 && Row == 2)
        {
            Console.WriteLine("You hear the stomach of the wumpus grumbling");
        }
        else if (Column ==3 && Row ==  2)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You stumbled onto the hungry wumpus who takes a mere glance before launching itself at you. You have been devoured by the wumpus. \nGame over.");
            GameOver = true;
        } else {
            Console.WriteLine("This room is empty");
        }
    }

   public void EnabledFountain()
        {
            if (Column == 1 && Row == 0)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("The Fountain Of Objects has been reactivated, and you have esaped wtih your life. You win!");
                GameOver= true;
            }
            else if (Column == 4 && Row == 1)
            {
                Console.WriteLine("The fountain of Objects is here and enabled. Return to the entrance.");
            }
            else if (Column == 3 && Row == 0 || Column == 4 && Row == 0) {
            Console.WriteLine("You hear water dripping nearby");
        } else if (Column == 3 && Row == 1) {
            Console.WriteLine("Your ears pick up the silence of the fountain but your nose picks up the stench of a large hungry wumpus.");
        }
        else if (Column ==1 && Row == 1 || Column == 1 && Row == 2 || Column == 2 && Row == 2)
        {
            Console.WriteLine("You hear the stomach of the wumpus grumbling");
        }
            else if (Column == 3 && Row == 2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You stumbled onto the hungry wumpus who takes a mere glance before launching itself at you. You have been devoured by the wumpus. \nGame over.");
                GameOver = true;
            } else {
                Console.WriteLine("This room is empty.");
            }
        }
}
