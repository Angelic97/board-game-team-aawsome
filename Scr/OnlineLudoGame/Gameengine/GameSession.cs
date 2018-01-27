﻿
using System;
using System.Collections.Generic;
using System.Text;

namespace Gameengine
{
    public class GameSession
    {
        public int GameID { get; set; }
        public User[] Players { get; set; }
        public User[] Board = new User[9];

        public static string WinConditions(User[] boardCells, User currentPlayer)
        {
            // condition 1
            if (boardCells[0] == currentPlayer && boardCells[1] == currentPlayer && boardCells[2] == currentPlayer)
            {
                return currentPlayer.Name + " has won!";
            }
            // condition 2
            else if
              (boardCells[0] == currentPlayer && boardCells[3] == currentPlayer && boardCells[6] == currentPlayer)
            {
                return currentPlayer.Name + " has won!";
            }
            // condition 3
            else if
              (boardCells[6] == currentPlayer && boardCells[7] == currentPlayer && boardCells[8] == currentPlayer)
            {
                return currentPlayer.Name + " has won!";
            }
            // condition 4
            else if
              (boardCells[2] == currentPlayer && boardCells[5] == currentPlayer && boardCells[8] == currentPlayer)
            {
                return currentPlayer.Name + " has won!";
            }
            // condition 5
            else if
              (boardCells[0] == currentPlayer && boardCells[4] == currentPlayer && boardCells[8] == currentPlayer)
            {
                return currentPlayer.Name + " has won!";
            }
            // condition 6
            else if
              (boardCells[2] == currentPlayer && boardCells[4] == currentPlayer && boardCells[6] == currentPlayer)
            {
                return currentPlayer.Name + " has won!";
            }
            // condition 7
            else if
              (boardCells[1] == currentPlayer && boardCells[4] == currentPlayer && boardCells[7] == currentPlayer)
            {
                return currentPlayer.Name + " has won!";
            }
            // condition 8
            else if
              (boardCells[3] == currentPlayer && boardCells[4] == currentPlayer && boardCells[5] == currentPlayer)
            {
                return currentPlayer.Name + " has won!";
            }
            // all boardspaces used but no win = Draw
            else if (boardCells[0] != null && boardCells[1] != null && boardCells[2] != null &&
                    boardCells[3] != null && boardCells[4] != null && boardCells[5] != null &&
                    boardCells[6] != null && boardCells[7] != null && boardCells[8] != null)
            {
                return "Draw";
            }

            return "";
        }

        public static int GenerateRandomGameID()
        {
            Random random = new Random();
            int result = random.Next(10000, 50000);
            return result;
        }
    }

}
