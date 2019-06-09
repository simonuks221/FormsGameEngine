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
        Box2dGameObject scoreBox;
        TextGameObject playerLifeText;

        float spawnFrequency = 2f;
        float lastSpawnTime = -100;

        PlayerShip playerShip;

        int playerLife = 10;

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

            scoreBox = new Box2dGameObject(gameManager, new Point(0, 400), new Size(200, 1));
            scoreBox.colliding = true;
            scoreBox.OnCollision += ScoreBox_OnCollision;
            gameManager.AddGameObjectToScene(scoreBox, 0);

            Box2dGameObject topBlock = new Box2dGameObject(gameManager, new Point(0, -1), new Size(200, 1));
            topBlock.colliding = true;
            gameManager.AddGameObjectToScene(topBlock, 0);

            Box2dGameObject bottomBlock = new Box2dGameObject(gameManager, new Point(0, 401), new Size(200, 1));
            bottomBlock.colliding = true;
            gameManager.AddGameObjectToScene(bottomBlock, 0);

            Box2dGameObject rightBlock = new Box2dGameObject(gameManager, new Point(201, 0), new Size(1, 400));
            rightBlock.colliding = true;
            gameManager.AddGameObjectToScene(rightBlock, 0);

            Box2dGameObject leftBlock = new Box2dGameObject(gameManager, new Point(-1, 0), new Size(1, 400));
            leftBlock.colliding = true;
            gameManager.AddGameObjectToScene(leftBlock, 0);

            playerLifeText = new TextGameObject(gameManager, new Point(0, 0));
            gameManager.AddGameObjectToScene(playerLifeText, 0);
            playerLifeText.text = playerLife.ToString();

            TextGameObject gameOverText = new TextGameObject(gameManager, new Point(100, 200));
            gameOverText.text = "Game over";
            GameScene gameOverScene = new GameScene(new List<GameObject>() { gameOverText});
            gameManager.AddScene(gameOverScene);
        }

        private void ScoreBox_OnCollision(GameObject2D _sender, GameObject2D _other)
        {
            EnemyShip enemy = _other as EnemyShip;
            if(enemy != null)
            {
                playerLife--;
                playerLifeText.text = playerLife.ToString();
                _other.Destroy();

                if(playerLife <= 0)
                {
                    gameManager.ChangeScene(1);
                }
            }
        }

        private void GameManager_Tick()
        {
            if (gameManager.gameTime - lastSpawnTime >= spawnFrequency)
            {
                SpawnNewEnemyShip();
                lastSpawnTime = gameManager.gameTime;
            }

            if (gameManager.keysDown.Contains(Keys.E))
            {
                PlayerShip ship = new PlayerShip(gameManager, gameManager.mouseLocation);
                gameManager.AddGameObjectToScene(ship, 0);
            }
        }

        void SpawnNewEnemyShip()
        {
            Random r = new Random();
            Point newEnemyLocation = new Point(r.Next(0, 180), 0);
            EnemyShip newEnemyShip = new EnemyShip(gameManager, newEnemyLocation, new Size(10, 10));
            newEnemyShip.colliding = true;
            newEnemyShip.objectVelocity = new Point(0, r.Next(1, 2));
            gameManager.AddGameObjectToScene(newEnemyShip, 0);
        }
    }
}
