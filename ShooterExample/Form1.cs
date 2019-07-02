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


namespace ShooterExample
{
    public partial class Form1 : Form
    {
        GameManager gameManager;
        MainGameEnginePanel mainGameEnginePanel;

        Player player;

        Size gamePanelSize = new Size(300, 300);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mainGameEnginePanel = new MainGameEnginePanel(this, gamePanelSize, new Point(0, 0));
            gameManager = new GameManager(this, mainGameEnginePanel);

            WallGameObject wall1 = new WallGameObject(gameManager, new Point(0, 0));

            List<WallGameObject> walls = CreateStartupWalls();

            GameScene gameScene1 = new GameScene(walls.ToList<GameObject>());
            gameManager.AddScene(gameScene1);

            player = new Player(gameManager, new Point(100, 100));
            gameManager.AddGameObjectToScene(player, 0);

            UiManager gameUi1 = new UiManager();
            gameManager.AddUi(gameUi1);

            gameManager.Tick += GameManager_Tick;

            ConnectionStateLabel.Text = "Not connected";
        }

        private void GameManager_Tick()
        {

        }

        private List<WallGameObject> CreateStartupWalls()
        {
            List<WallGameObject> walls = new List<WallGameObject>();

            for(int x = 0; x < gamePanelSize.Width; x += 10)
            {
                for (int y = 0; y < gamePanelSize.Height; y += 10)
                {
                    if ((x == 0 || x == gamePanelSize.Width - 10) || (y == 0 || y == gamePanelSize.Width - 10))
                    {
                        WallGameObject newWall = new WallGameObject(gameManager, new Point(x, y));
                        walls.Add(newWall);
                    }
                }
            }
            return walls;
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {

        }
    }
}