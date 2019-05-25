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

            List<GameObject> scene1GameObjects = new List<GameObject>() {playerGameObject, new CubeGameObject(new Point(0, 0), new Size(20, 20), Color.Red), new CubeGameObject(new Point(50, 50), new Size(5, 5), Color.Red)};
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
            
            if (gameManager.keysDown.Contains(Keys.D) && playerGameObject.gameObjectLocation.X < gameManager.mainGameEnginePanel.Size.Width - 10)
            {
                playerGameObject.objectVelocity.X = speedMultiplier;
            }
            else if (gameManager.keysDown.Contains(Keys.A) && playerGameObject.gameObjectLocation.X > 0)
            {
                playerGameObject.objectVelocity.X = -speedMultiplier;
            }
            else
            {
                playerGameObject.objectVelocity.X = 0;
            }
            if (gameManager.keysDown.Contains(Keys.W) && playerGameObject.gameObjectLocation.Y > 0)
            {
                playerGameObject.objectVelocity.Y = -speedMultiplier;
            }
            else if (gameManager.keysDown.Contains(Keys.S) && playerGameObject.gameObjectLocation.Y < gameManager.mainGameEnginePanel.Size.Height - 10)
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
