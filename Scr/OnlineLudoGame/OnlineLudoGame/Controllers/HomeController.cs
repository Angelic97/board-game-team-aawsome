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
            string cookieValue = Request.Cookies["User"].Value;
            var gameSession = Gameengine.ActiveGame.GetSession(cookieValue);
            if (gameSession == null)
            {
                gameSession = new GameSession();
            }
            var cell = gameSession.WriteBoard();
            var board = new Board
            {
                GameID = gameSession.GameID,
                Cell1 = cell[0],
                Cell2 = cell[1],
                Cell3 = cell[2],
                Cell4 = cell[3],
                Cell5 = cell[4],
                Cell6 = cell[5],
                Cell7 = cell[6],
                Cell8 = cell[7],
                Cell9 = cell[8]
            };
            return View(board);
        }

        public ActionResult PlayerMove(string buttonClick)
        {
            string cookieValue = Request.Cookies["User"].Value;
            var gameSession = Gameengine.ActiveGame.GetSession(cookieValue);
            try
            {
                gameSession.Turn(cookieValue, buttonClick);
            }
            catch { }
            return RedirectToAction("Game");
        }
        public ActionResult TableOfContent()
        {
         
            return View();
        }
    }
}