using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FormsGameEngine;

namespace FormsGameEngineFormExample
{
    public partial class Form1 : Form
    {
        GameManager gameManager;
        GameObject2D playerGameObject;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MainGameEnginePanel mainGameEnginePanel = new MainGameEnginePanel(new Size(700, 400));
            Controls.Add(mainGameEnginePanel);
            mainGameEnginePanel.Location = new Point(0, 0);

            gameManager = new GameManager(this, mainGameEnginePanel);

            playerGameObject = new CubeGameObject(new Point(30, 30), new Size(10, 10), Color.Green);
            playerGameObject.OnCollision += PlayerHit;

            CubeGameObject cube1 = new CubeGameObject(new Point(0, 0), new Size(20, 20), Color.Red);
            cube1.solid = true;
            CubeGameObject cube2 = new CubeGameObject(new Point(50, 50), new Size(20, 20), Color.Red);
            cube2.solid = true;

            TextGameObject text1 = new TextGameObject(new Point(100, 100));

            List<GameObject> scene1GameObjects = new List<GameObject>() {playerGameObject, cube1, cube2, text1};
            
            GameScene scene1 = new GameScene(scene1GameObjects);
            
            gameManager.gameScenes.Add(scene1);


            gameManager.Tick += GameManager_Tick;
        }

        private void GameManager_Tick()
        {
            SetupPlayerMovement();
        }

        void SetupPlayerMovement()
        {
            int speedMultiplier = 1; //For greater speed when Shift is pressed
            if (gameManager.keysDown.Contains(Keys.ShiftKey))
            {
                speedMultiplier = 3;
            }
            else
            {
                speedMultiplier = 1;
            }
            
            if (gameManager.keysDown.Contains(Keys.D) && playerGameObject.gameObjectLocation.X + playerGameObject.boundingBox.max.X < gameManager.mainGameEnginePanel.Size.Width)
            {
                playerGameObject.objectVelocity.X = speedMultiplier;
            }
            else if (gameManager.keysDown.Contains(Keys.A) && playerGameObject.gameObjectLocation.X + playerGameObject.boundingBox.min.X > 0)
            {
                playerGameObject.objectVelocity.X = -speedMultiplier;
            }
            else
            {
                playerGameObject.objectVelocity.X = 0;
            }

            if (gameManager.keysDown.Contains(Keys.W) && playerGameObject.gameObjectLocation.Y + playerGameObject.boundingBox.min.Y > 0)
            {
                playerGameObject.objectVelocity.Y = -speedMultiplier;
            }
            else if (gameManager.keysDown.Contains(Keys.S) && playerGameObject.gameObjectLocation.Y + playerGameObject.boundingBox.max.Y < gameManager.mainGameEnginePanel.Size.Height)
            {
                playerGameObject.objectVelocity.Y = speedMultiplier;
            }
            else
            {
                playerGameObject.objectVelocity.Y = 0;
            }
        }

        void PlayerHit( GameObject2D _sender, GameObject2D _other)
        {
            Console.Out.WriteLine("Ahh, i hit " + _other);
        }
    }
}
