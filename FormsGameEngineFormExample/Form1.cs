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

            List<GameObject> scene1GameObjects = new List<GameObject>() {playerGameObject, new CubeGameObject(new Point(0, 0), new Size(20, 20), Color.Red)};
            GameScene scene1 = new GameScene(scene1GameObjects);
            
            gameManager.gameScenes.Add(scene1);

            gameManager.Tick += GameManager_Tick;
        }

        private void GameManager_Tick(object sender, EventArgs e)
        {
            SetupPlayerMovement();
        }

        void SetupPlayerMovement()
        {
            foreach (Keys k in gameManager.keysDown)
            {
                if (k == Keys.D)
                {
                    playerGameObject.gameObjectLocation.X += 1;
                }
                if (k == Keys.A)
                {
                    playerGameObject.gameObjectLocation.X -= 1;
                }
                if (k == Keys.W)
                {
                    playerGameObject.gameObjectLocation.Y -= 1;
                }
                if (k == Keys.S)
                {
                    playerGameObject.gameObjectLocation.Y += 1;
                }
            }
        }
    }
}
