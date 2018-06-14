using Aubergine;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace Game
{
    class MyForm : Form
    {
        private static string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "image");
        private static Image playerLImg = Image.FromFile(Path.Combine(path, "playerL.png"));
        private static Image playerRImg = Image.FromFile(Path.Combine(path, "playerR.png"));
        private static Image wormImg = Image.FromFile(Path.Combine(path, "worm.png"));
        private static Image defaultImg = Image.FromFile(Path.Combine(path, "default.png"));

        private Image playerImg = playerLImg;

        private bool isUp;
        private bool isL;
        private bool isR;
        private bool isDown;

        private Direction playerDirection;

        public MyForm()
        {
            DoubleBuffered = true;
            ClientSize = new Size(1000, 500);
            BackColor = Color.Aqua;
            //BackgroundImage = Image.FromFile(Path.Combine(path, "water.png"));

            var game = new Game();

            PaintEventHandler drawingField = (sender, args) =>
            {
                foreach (var obj in game.Objects)
               {
                    var img = defaultImg;

                    if (obj is Hero)
                        img = playerImg;
                    if (obj is Worm)
                        img = wormImg;

                   args.Graphics.DrawImage(img, 
                       obj.Position.Coords.X - 30, obj.Position.Coords.Y - 30,
                       obj.Position.Size.Width + 30, obj.Position.Size.Height + 30); //obj.Position.Body);
               }
            };

            Paint += drawingField;

            KeyDown += MyForm_KeyDown;
            KeyUp += MyForm_KeyUp;

            var time = 0;
            var timer = new Timer {Interval = 5 };
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
            var distance = 5;
            
            if (isUp)
            {
                direction = Direction.Down;
                player.MoveInDirection(direction, distance);
                game.MoveCameraView(direction, -distance);
            }
            if (isDown)
            {
                direction = Direction.Up;
                player.MoveInDirection(direction, distance);
                game.MoveCameraView(direction, -distance);
            }
            if (isL)
            {
                playerImg = playerLImg;
                direction = Direction.Left;
                player.MoveInDirection(direction, distance);
                game.MoveCameraView(direction, -distance);
            }
            if (isR)
            {
                playerImg = playerRImg;
                direction = Direction.Right;
                player.MoveInDirection(direction, distance);
                game.MoveCameraView(direction, -distance);
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
