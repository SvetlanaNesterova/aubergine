﻿using System;
using Aubergine;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using PtichkaGame.Logic;

namespace PtichkaGame
{
    class BirdForm : Form
    {
        private static string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "image");
        private static Image playerLImg = Image.FromFile(Path.Combine(path, "birdL.png"));
        private static Image playerFaceImg = Image.FromFile(Path.Combine(path, "bird.png"));
        private static Image playerRImg = Image.FromFile(Path.Combine(path, "birdR.png"));
        private static Image bushImg = Image.FromFile(Path.Combine(path, "bush.png"));
        private static Image emptyBushImg = Image.FromFile(Path.Combine(path, "bushEmpty.png"));
        private static Image defaultImg = Image.FromFile(Path.Combine(path, "default.png"));

        private Image playerImg = playerFaceImg;

        private bool isUp;
        private bool isL;
        private bool isR;
        private bool isDown;

        public BirdForm()
        {
            DoubleBuffered = true;
            ClientSize = new Size(1000, 500);
            BackColor = Color.DarkOliveGreen;
            //BackgroundImage = Image.FromFile(Path.Combine(path, "water.png"));

            var game = new Game();

            PaintEventHandler drawingField = (sender, args) =>
            {
                var frameSize = 15;
                var player = game.GetPlayer();
                var playerCoordsOnScreen = new Point(
                    ClientSize.Width / 2 - player.Position.Size.Width / 2,
                    ClientSize.Height / 2 - player.Position.Size.Height / 2);

                args.Graphics.DrawImage(playerImg, playerCoordsOnScreen.X - frameSize, playerCoordsOnScreen.Y - frameSize,
                    player.Position.Size.Width + frameSize * 2, player.Position.Size.Height + frameSize * 2);

                var screenCoords = Point.Subtract(player.Position.Coords, new Size(playerCoordsOnScreen));
                var objInRegion = game.GetGameObjectsInRectangle(new Rectangle(screenCoords, ClientSize));

                foreach (var obj in objInRegion)
                {
                    var img = defaultImg;

                    if (obj is BirdPlayer) continue;

                    if (obj is Bush)
                    {
                        var bush = obj as Bush;
                        if (bush.BerriesCount == 0)
                            img = emptyBushImg; 
                        else
                            img = bushImg;
                    }

                    var objCoordsOnScreen = Point.Subtract(obj.Position.Coords, new Size(screenCoords));

                    args.Graphics.DrawImage(img,
                        objCoordsOnScreen.X - frameSize, objCoordsOnScreen.Y - frameSize,
                        obj.Position.Size.Width + frameSize * 2, obj.Position.Size.Height + frameSize * 2);
                }
            };

            Paint += drawingField;

            KeyDown += MyForm_KeyDown;
            KeyUp += MyForm_KeyUp;

            var time = 0;
            var timer = new Timer { Interval = 5 };
            timer.Tick += (sender, args) =>
            {
                time++;
                game.Tick();
                DoMoving(game);
                Invalidate();
            };
            timer.Start();
        }

        private void DoMoving(Game game)
        {
            var player = game.GetPlayer();
            var direction = Direction.None;
            var distance = 2;

            if (isUp)
            {
                direction = Direction.Down;
                player.MoveInDirection(direction, distance);
            }
            if (isDown)
            {
                direction = Direction.Up;
                player.MoveInDirection(direction, distance);
            }
            if (isL)
            {
                playerImg = playerLImg;
                direction = Direction.Left;
                player.MoveInDirection(direction, distance);
            }
            if (isR)
            {
                playerImg = playerRImg;
                direction = Direction.Right;
                player.MoveInDirection(direction, distance);
            }
        }

        private void MyForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
                isUp = false;
            if (e.KeyCode == Keys.A)
                isL = false;
            if (e.KeyCode == Keys.D)
                isR = false;
            if (e.KeyCode == Keys.S)
                isDown = false;
            playerImg = playerFaceImg;

        }

        private void MyForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
                isUp = true;
            if (e.KeyCode == Keys.A)
                isL = true;
            if (e.KeyCode == Keys.D)
                isR = true;
            if (e.KeyCode == Keys.S)
                isDown = true;
        }
    }
}
