﻿using Gameengine;
using OnlineLudoGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace OnlineLudoGame.Controllers
{
    public class HomeController : Controller
    {
        public string PlayerID { get; private set; }
        public static int GameID { get; set; }
        private static bool IsStart = true;

        // GET: Home        
        public ActionResult StartPage()
        {
            if (Request.Cookies["User"] == null)
            {
                HttpCookie cookie = new HttpCookie("User");
                Guid guid = Guid.NewGuid();
                cookie.Value = guid.ToString();
                cookie.Expires = DateTime.Now.AddDays(2);
                cookie.Path = "";
                Response.SetCookie(cookie);
            }
            return View();
        }
        public ActionResult LoginUser(Gameengine.User player, string startbtn, string joinbtn)
        {
            if (startbtn == "Start a game")
            {
                player.PlayerID = PlayerID = Request.Cookies["User"].Value;
                player.Side = "O";
                GameID = Gameengine.GameSession.GenerateRandomGameID();
                Gameengine.Lobby.CreateGame(GameID, player);
            }
            if (joinbtn == "Join an existing game")
            {
                player.PlayerID = PlayerID = Request.Cookies["User"].Value;
                player.Side = "X";
                Gameengine.Lobby.FindGame(player);
            }
            return RedirectToAction("Game");
        }

        public ActionResult Game()
        {
            // Find some way to fetch GameID
            string cookieValue = Request.Cookies["User"].Value;
            var gameSession = Gameengine.ActiveGame.GetSession(cookieValue);

            string[] side = new string[9];
            for (int i = 0; i < 9; i++)
            {
                try
                {
                    //if (Gameengine.ActiveGame.Game[0].Board[i].Side != null)
                    if(gameSession.Board[i].Side != null)
                    {
                        side[i] = gameSession.Board[i].Side;
                    }
                }
                catch
                {
                    side[i] = "";
                }
            }
            var board = new Board
            {  
                GameID = GameID,
                Cell1 = side[0],
                Cell2 = side[1],
                Cell3 = side[2],
                Cell4 = side[3],
                Cell5 = side[4],
                Cell6 = side[5],
                Cell7 = side[6],
                Cell8 = side[7],
                Cell9 = side[8]
            };
            return View(board);
        }

        public ActionResult PlayerMove(string buttonclickid)
        {
            string currentPlayer = Request.Cookies["User"].Value;
            try
            {
                if (IsStart == true)
                {
                    var session = Gameengine.ActiveGame.Game.FindIndex(x => x.Players[0].PlayerID == currentPlayer);
                    if (currentPlayer == Gameengine.ActiveGame.Game[session].Players[0].PlayerID)
                    {
                        var player1 = Gameengine.ActiveGame.Game[session].Players[0];
                        int indexPressed = int.Parse(buttonclickid) - 1;
                        Gameengine.ActiveGame.Game[session].Board[indexPressed] = player1;
                        IsStart = false;
                    }
                }
                else if (IsStart == false)
                {
                    var session = Gameengine.ActiveGame.Game.FindIndex(x => x.Players[1].PlayerID == currentPlayer);
                    if (currentPlayer == Gameengine.ActiveGame.Game[session].Players[1].PlayerID)
                    {
                        var player2 = Gameengine.ActiveGame.Game[session].Players[1];
                        int indexPressed = int.Parse(buttonclickid) - 1;
                        Gameengine.ActiveGame.Game[session].Board[indexPressed] = player2;
                        IsStart = true;
                    }
                }
            }
            catch { }
            return RedirectToAction("Game");
        }
    }
}