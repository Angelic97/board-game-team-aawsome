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
        public bool FirstPlayerTurn = true;
        public int State { get; set; }

        //Method for Switching active player.
        /// <summary>
        /// Method for Switching active player.
        /// </summary>
        /// <param name="cookieValue">The cookievalue identifies who clicked the button.</param>
        /// <param name="buttonClick">ButtonClick is the number of the box (1-9), that was chosen.</param>
        public void Turn(string cookieValue, string buttonClick)
        {
            if (this.FirstPlayerTurn == true)
            {
                var session = Gameengine.ActiveGame.Game.FindIndex(x => x.Players[0].PlayerID == cookieValue);
                if (cookieValue == this.Players[0].PlayerID)
                {
                    var player1 = this.Players[0];
                    int indexPressed = int.Parse(buttonClick);
                    if (this.Board[indexPressed].Side == "-")
                    {
                        this.Board[indexPressed] = player1;
                        this.FirstPlayerTurn = false;
                    }
                    else
                    {
                        this.FirstPlayerTurn = true;
                    }
                    Gameengine.ActiveGame.Game[session] = this;
                }
            }
            else if (this.FirstPlayerTurn == false)
            {
                var session = Gameengine.ActiveGame.Game.FindIndex(x => x.Players[1].PlayerID == cookieValue);
                if (cookieValue == this.Players[1].PlayerID)
                {
                    var player2 = this.Players[1];
                    int indexPressed = int.Parse(buttonClick);
                    if (this.Board[indexPressed].Side == "-")
                    {
                        this.Board[indexPressed] = player2;
                        this.FirstPlayerTurn = true;
                    }
                    else
                    {
                        this.FirstPlayerTurn = false;
                    }
                    Gameengine.ActiveGame.Game[session] = this;
                }
            }
        }

        //Method that writes GameBoard state to view.
        /// <summary>
        /// Method that writes GameBoard state to view.
        /// </summary>
        /// <returns>The board with the latest click.</returns>
        public string[] WriteBoard()
        {
            string[] cell = new string[9];
            for (int i = 0; i < 9; i++)
            {
                try
                {
                    if (this.Board[i].Side != "-")
                    {
                        cell[i] = this.Board[i].Side;
                    }
                    else if (this.Board[i].Side == "-")
                    {
                        cell[i] = "";
                    }
                }
                catch
                {
                    cell[i] = "";
                }
            }
            return cell;
        }

        //Method that checks if winning conditions are met.
        /// <summary>
        /// Method that checks if winning conditions are met.
        /// </summary>
        /// <returns>Feedback if the game is won and by who, or if it's a draw, or if game shall continue.</returns>
        public string WinConditions()
        {
            string winner = "";
            if (this.Board[0].Side.Equals(this.Board[1].Side) && this.Board[0].Side.Equals(this.Board[2].Side))
            {
                if (this.Board[0].Name == null) { }
                else { winner = this.Board[0].Name + " WON THE GAME!"; }
            }
            if (this.Board[3].Side.Equals(this.Board[4].Side) && this.Board[3].Side.Equals(this.Board[5].Side))
            {
                if (this.Board[3].Name == null) { }
                else { winner = this.Board[3].Name + " WON THE GAME!"; }
            }
            if (this.Board[6].Side.Equals(this.Board[7].Side) && this.Board[6].Side.Equals(this.Board[8].Side))
            {
                if (this.Board[6].Name == null) { }
                else { winner = this.Board[6].Name + " WON THE GAME!"; }
            }
            if (this.Board[0].Side.Equals(this.Board[3].Side) && this.Board[0].Side.Equals(this.Board[6].Side))
            {
                if (this.Board[0].Name == null) { }
                else { winner = this.Board[0].Name + " WON THE GAME!"; }
            }
            if (this.Board[1].Side.Equals(this.Board[4].Side) && this.Board[1].Side.Equals(this.Board[7].Side))
            {
                if (this.Board[1].Name == null) { }
                else { winner = this.Board[1].Name + " WON THE GAME!"; }
            }
            if (this.Board[2].Side.Equals(this.Board[5].Side) && this.Board[2].Side.Equals(this.Board[8].Side))
            {
                if (this.Board[2].Name == null) { }
                else { winner = this.Board[2].Name + " WON THE GAME!"; }
            }
            if (this.Board[0].Side.Equals(this.Board[4].Side) && this.Board[0].Side.Equals(this.Board[8].Side))
            {
                if (this.Board[0].Name == null) { }
                else { winner = this.Board[0].Name + " WON THE GAME!"; }
            }
            if (this.Board[2].Side.Equals(this.Board[4].Side) && this.Board[2].Side.Equals(this.Board[6].Side))
            {
                if (this.Board[2].Name == null) { }
                else { winner = this.Board[2].Name + " WON THE GAME!"; }
            }
            else if (!this.Board[0].Side.Equals("-") && !this.Board[1].Side.Equals("-") && !this.Board[2].Side.Equals("-") &&
                !this.Board[3].Side.Equals("-") && !this.Board[4].Side.Equals("-") && !this.Board[5].Side.Equals("-") &&
                !this.Board[6].Side.Equals("-") && !this.Board[7].Side.Equals("-"))
            {
                winner = "IT'S A DRAW";                
            }          
            return winner;
        }

        //Method that generates random GameID
        /// <summary>
        /// Method that generates random GameID
        /// </summary>
        /// <returns>An id between 10000-50000</returns>
        public static int GenerateRandomGameID()
        {
            Random random = new Random();
            int result = random.Next(10000, 50000);
            return result;
        }
    }
}
