using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading;
using WMPLib;

namespace snakeTest
{
    /// <summary>
    /// Classe qui simule un jeu du snake en console
    /// </summary>
    public class Snake
    {
        #region attributs
        WindowsMediaPlayer mplayer;
        private ConsoleKeyInfo lastTouch; // Dernière touche qu'à touché l'utilisateur
        private const int ZONEMIN_X = 0; // Zone minimum de X
        private const int ZONEMIN_Y = 0; // Zone minimum de Y
        private const int ZONEMAX_X = 110; // Zone maximum de X
        private const int ZONEMAX_Y = 25; // Zone maximum de Y
        private int x = 0;
        private int y = 0;
        private string snake; // Allure du serpent
        private bool death = false;
        #endregion

        public Snake()
        {
            Console.Title = "Snake Game";
            Console.CursorVisible = false;
            // Ecrit le nom du jeu
            Console.SetCursorPosition(ZONEMAX_X / 2, ZONEMAX_Y + 3);
            Console.WriteLine("SNAKE GAME");
            Console.SetCursorPosition(ZONEMAX_X / 2 - 20, ZONEMAX_Y + 4);
            Console.WriteLine("Appyez sur une touche directionnelle pour commencer");
            this.PlayMusic("./sounds/snakeMusic.mp3");
        }


        /// <summary>
        /// Lance le jeu "Snake"
        /// </summary>
        public static void StartGame()
        {
            Snake snake = new Snake();
            Thread movePlayer = new Thread(new ThreadStart(snake.MovePlayer));
            Thread AutoMovePlayer = new Thread(new ThreadStart(snake.AutoMove));
            movePlayer.Start();
            AutoMovePlayer.Start();
        }

        /// <summary>
        /// Change la direction du joueur selon où appuie le joueur
        /// </summary>
        /// <param name="key"> Touche sur laquelle à appuyé le joueur</param>
        private void MovePlayer()
        {
            while (true)
            {
                this.lastTouch = Console.ReadKey(true);
                Console.SetCursorPosition(x, y);
                Console.WriteLine(snake);
            }
        }

        /// <summary>
        /// Fait bouger automatiquement le joueur
        /// </summary>
        private void AutoMove()
        {
            while (true)
            {
                BackGround();
                if (this.death == false) // VIVANT
                {
                    Thread.Sleep(1); // <-----------------------------------------------------
                    switch (lastTouch.Key.ToString())
                    {
                        case "UpArrow":
                            if (this.y != ZONEMIN_Y)
                            {
                                this.y -= 1;
                            }
                            else
                            {
                                death = true;
                            }
                            snake = ChangeSprite("haut");
                            break;
                        case "DownArrow":
                            if (this.y != ZONEMAX_Y)
                            {
                                this.y += 1;
                            }
                            else
                            {
                                death = true;
                            }
                            snake = ChangeSprite("bas");
                            break;
                        case "LeftArrow":
                            if (this.x != ZONEMIN_X)
                            {
                                this.x -= 2;
                            }
                            else
                            {
                                death = true;
                            }
                            snake = ChangeSprite("gauche");
                            break;
                        case "RightArrow":
                            if (this.x != ZONEMAX_X)
                            {
                                this.x += 2;
                            }
                            else
                            {
                                death = true;
                            }
                            snake = ChangeSprite("droite");
                            break;
                    }
                    this.ClearAt();
                    Console.SetCursorPosition(x, y);
                    Console.WriteLine(snake);
                }
                else if (this.death == true) // MORT
                {
                    Console.SetCursorPosition(ZONEMAX_X / 2, ZONEMAX_Y / 2);
                    Console.WriteLine("GAME OVER");
                    this.PlayMusic("./sounds/sadMusic.mp3");

                }
            }
        }

        /// <summary>
        /// Joue la musique du jeu
        /// </summary>
        private void PlayMusic(string lien)
        {
            mplayer = new WindowsMediaPlayer();
            mplayer.URL = lien;
            mplayer.controls.play();
        }

        /// <summary>
        /// Change le background où de la zone du joueur
        /// </summary>
        private void BackGround()
        {
            for (int i = 0; i < ZONEMAX_X + 1; i++)
            {
                for (int j = 0; j < ZONEMAX_Y + 1; j++)
                {
                    Console.SetCursorPosition(i, j);
                    Console.BackgroundColor = ConsoleColor.White;
                }
            }
        }

        /// <summary>
        /// Change l'allure du serpent
        /// </summary>
        /// <param name="direction"> direction appuyé </param>
        /// <returns></returns>
        private string ChangeSprite(string direction)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            string snake = "";
            switch (direction)
            {
                case "haut":
                    snake = "|";
                    break;

                case "bas":
                    snake = "|";
                    break;

                case "gauche":
                    snake = "(:===";
                    break;

                case "droite":
                    snake = "===:)";
                    break;
            }
            return snake;
        }

        /// <summary>
        /// Clear la console dans l'endroit du jeu
        /// </summary>
        private void ClearAt()
        {
            for (int i = 0; i < ZONEMAX_X + 1; i++)
            {
                for (int j = 0; j < ZONEMAX_Y + 1; j++)
                {
                    Console.SetCursorPosition(i, j);
                    Console.Write(" ");
                }
            }
        }
    }

    //  ##   ##    ##     ####     ######            #####    ##  ##            ####       ##     ##   ##   ####    ######   ##  ##             ####    ##  ##     ##     #####    #####    ######   ######
    //  ### ###   ####    ## ##    ##                ##  ##   ##  ##            ## ##     ####    ### ###    ##     ##       ### ##            ##  ##   ##  ##    ####    ##  ##   ##  ##   ##         ##
    //  #######  ##  ##   ##  ##   ##                ##  ##   ##  ##            ##  ##   ##  ##   #######    ##     ##       ######            ##       ##  ##   ##  ##   ##  ##   ##  ##   ##         ##
    //  ## # ##  ######   ##  ##   ####              #####     ####             ##  ##   ######   ## # ##    ##     ####     ######            ##       ######   ######   #####    #####    ####       ##
    //  ##   ##  ##  ##   ##  ##   ##                ##  ##     ##              ##  ##   ##  ##   ##   ##    ##     ##       ## ###            ##       ##  ##   ##  ##   ##  ##   ####     ##         ##
    //  ##   ##  ##  ##   ## ##    ##                ##  ##     ##              ## ##    ##  ##   ##   ##    ##     ##       ##  ##            ##  ##   ##  ##   ##  ##   ##  ##   ## ##    ##         ##
    //  ##   ##  ##  ##   ####     ######            #####      ##              ####     ##  ##   ##   ##   ####    ######   ##  ##             ####    ##  ##   ##  ##   #####    ##  ##   ######     ##
    //    _     _
    //   (c).-.(c)
    //    / ._. \
    //  __\( Y )/__
    // (_.-/'-'\-._)
    //    || M ||
    //  _.' `-' '._
    // (.-./`-'\.-.)
    //  `-'     `-'



}
