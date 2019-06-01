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

        float spawnFrequency = 2f;
        float lastSpawnTime = -100;

        PlayerShip playerShip;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mainGamePanel = new MainGameEnginePanel(this, new Size(200, 400), new Point(0, 0));
            gameManager = new GameManager(this, mainGamePanel);

            playerShip = new PlayerShip(gameManager, new Point(100, 350));

            GameScene gameScene = new GameScene(new List<GameObject>() { playerShip});
            gameManager.AddScene(gameScene);

            gameManager.Tick += GameManager_Tick;
        }

        private void GameManager_Tick()
        {
            if (gameManager.gameTime - lastSpawnTime >= spawnFrequency)
            {
                SpawnNewEnemyShip();
                lastSpawnTime = gameManager.gameTime;
            }
        }

        void SpawnNewEnemyShip()
        {
            Random r = new Random();
            Point newEnemyLocation = new Point(r.Next(0, 180), 0);
            EnemyShip newEnemyShip = new EnemyShip(gameManager, newEnemyLocation, new Size(10, 10));
            newEnemyShip.solid = true;
            newEnemyShip.objectVelocity = new Point(0, r.Next(1, 2));
            gameManager.AddGameObjectToScene(newEnemyShip, gameManager.currentActiveScene);
        }
    }
}
