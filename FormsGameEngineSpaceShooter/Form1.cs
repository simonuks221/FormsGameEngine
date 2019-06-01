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

namespace FormsGameEngineSpaceShooter
{
    public partial class Form1 : Form
    {
        GameManager gameManager;
        MainGameEnginePanel mainGamePanel;

        bool delayingToSpawn = false;

        PlayerShip playerShip;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mainGamePanel = new MainGameEnginePanel(this, new Size(200, 200), new Point(0, 0));
            gameManager = new GameManager(this, mainGamePanel);

            playerShip = new PlayerShip(gameManager, new Point(100, 100));

            GameScene gameScene = new GameScene(new List<GameObject>() { playerShip});
            gameManager.AddScene(gameScene);

            gameManager.Tick += GameManager_Tick;
        }

        private void GameManager_Tick()
        {
            if (!delayingToSpawn)
            {
                gameManager.Delayed(1000, () => SpawnNewEnemyShip());
                delayingToSpawn = true;
            }
        }

        void SpawnNewEnemyShip()
        {
            delayingToSpawn = false;
            Random r = new Random();
            Point newEnemyLocation = new Point(r.Next(0, 200), 0);
            EnemyShip newEnemyShip = new EnemyShip(gameManager, newEnemyLocation, new Size(10, 10));
            newEnemyShip.solid = true;
            gameManager.AddGameObjectToScene(newEnemyShip, gameManager.currentActiveScene);
        }
    }
}
