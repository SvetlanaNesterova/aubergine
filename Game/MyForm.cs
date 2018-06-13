using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Game
{
    class MyForm : Form
    {
        private static string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "image");
        private static Image playerLImg = Image.FromFile(Path.Combine(path, "birdL.png"));
        private static Image playerRImg = Image.FromFile(Path.Combine(path, "birdR.png"));
        private static Image playerStaticImg = Image.FromFile(Path.Combine(path, "bird.png"));
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
            BackColor = Color.SkyBlue;
            //BackgroundImage = Image.FromFile(Path.Combine(path, "water.png"));

            var game = new Game();

            PaintEventHandler drawingField = (sender, args) =>
            {
                foreach (var obj in game.objects)
                {
                    var imgPath = defaultImg;

                    if (obj is Player)
                        imgPath = playerImg;
                    if (obj is Worm)
                        imgPath = wormImg;

                    args.Graphics.DrawImage(imgPath, obj.Position.Coords);
                }
            };

            Paint += drawingField;

            KeyDown += MyForm_KeyDown;
            KeyUp += MyForm_KeyUp;

            var time = 0;
            var timer = new Timer {Interval = 5};
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
            //var player = (Player) game.objects[0];
            //if (isUp) player.GoUp();
            //if (isDown) player.GoDown();
            //if (isL) player.GoL();
            //if (isR) player.GoR();
            if (isUp) game.moveObjects(Direction.Up);
            if (isDown) game.moveObjects(Direction.Down);
            playerImg = playerStaticImg;
            if (isL)
            {
                game.moveObjects(Direction.Left);
                playerImg = playerLImg;
            }

            if (isR)
            {
                game.moveObjects(Direction.Right);
                playerImg = playerRImg;
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
    enum Direction
    {
        Up,
        Down,
        Right,
        Left
    }
}
