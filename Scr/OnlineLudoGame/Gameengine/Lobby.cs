﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Web;


namespace Gameengine
{
    public class Lobby
    {
        public static List<GameSession> PendingGame = new List<GameSession>();

        // Creates new game
        /// <summary>
        /// Creates a new game with empty boardcells. These will be filled with User-objects when a button is clicked.
        /// </summary>
        /// <param name="id">A random selected number to represent the game</param>
        /// <param name="p">User object that identifies who clicked the button </param>
        public static void CreateGame(int id, User p)
        {
            GameSession gameSession = new GameSession
            {
                GameID = id,
                Players = new User[2],
                Board = new User[9]
                {
                    new User { Side = "-" },
                    new User { Side = "-" },
                    new User { Side = "-" },
                    new User { Side = "-" },
                    new User { Side = "-" },
                    new User { Side = "-" },
                    new User { Side = "-" },
                    new User { Side = "-" },
                    new User { Side = "-" }
                },
                ActiveGame = false
            };
            gameSession.Players[0] = p;
            PendingGame.Add(gameSession);
        }

        // Finds pending game
        /// <summary>
        /// Finds a pending game for player 2 to join. Chooses the first in the array.
        /// </summary>
        /// <param name="p">Player object</param>
        public static void FindGame(User p)
        {
            PendingGame[0].Players[1] = (p);
            PendingGame[0].ActiveGame = true;
            ActiveGame.Game.Add(PendingGame[0]);
            PendingGame.Remove(PendingGame[0]);           
        }

        // Finds GameSession object 
        /// <summary>
        /// Finds GameSession object by looping current session arrays.
        /// </summary>
        /// <param name="cookieValue">PlayerID is set to its CookieID</param>
        /// <returns>The session that the player is a part of.</returns>
        public static GameSession GetSession(string cookieValue)
        {
            GameSession foundSession = null;
            for (int i = 0; i < 2; i++)
            {
                foundSession = PendingGame.Find(x => x.Players[i].PlayerID == cookieValue);
                if (foundSession.Players[i].PlayerID == cookieValue)
                {
                    break;
                }
            }
            return foundSession;
        }
    }
}
