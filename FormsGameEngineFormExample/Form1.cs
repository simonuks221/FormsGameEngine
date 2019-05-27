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
        GameObject2D leftHadle;
        GameObject2D rightHandle;
        GameObject2D ballObject;
        TextGameObject rightScoreText;
        TextGameObject leftScoreText;

        int leftScore = 0;
        int rightScore = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MainGameEnginePanel mainGameEnginePanel = new MainGameEnginePanel(new Size(400, 200));
            Controls.Add(mainGameEnginePanel);
            mainGameEnginePanel.Location = new Point(0, 0);

            gameManager = new GameManager(this, mainGameEnginePanel);

            CubeGameObject leftTrigger = new CubeGameObject(new Point(-1, 0), new Size(1, 200), Color.Red);
            leftTrigger.objectTag = "leftTrigger";
            leftTrigger.solid = true;
            CubeGameObject rightTrigger = new CubeGameObject(new Point(400, 0), new Size(1, 200), Color.Red);
            rightTrigger.objectTag = "rightTrigger";
            rightTrigger.solid = true;

            CubeGameObject topSide = new CubeGameObject(new Point(0, -1), new Size(400, 1), Color.Red);
            topSide.objectTag = "side";
            topSide.solid = true;
            CubeGameObject bottomSide = new CubeGameObject(new Point(0, 200), new Size(400, 1), Color.Red);
            bottomSide.objectTag = "side";
            bottomSide.solid = true;

            leftHadle = new CubeGameObject(new Point(25, 100), new Size(10, 50), Color.Green);
            leftHadle.solid = true;
            leftHadle.objectTag = "handle";
            rightHandle = new CubeGameObject(new Point(375, 100), new Size(10, 50), Color.Green);
            rightHandle.solid = true;
            rightHandle.objectTag = "handle";

            ballObject = new CubeGameObject(new Point(200, 100), new Size(10, 10), Color.Black);
            Random r = new Random();
            ballObject.objectVelocity = new Point(1, 1);
            ballObject.objectTag = "ball";
            ballObject.solid = true;
            ballObject.OnCollision += BallCollision;

            rightScoreText = new TextGameObject(new Point(350, 0));
            rightScoreText.text = rightScore.ToString();
            leftScoreText = new TextGameObject(new Point(20, 0));
            leftScoreText.text = leftScore.ToString();

            List<GameObject> scene1GameObjects = new List<GameObject>() { leftHadle, rightHandle, leftScoreText, rightScoreText, ballObject, topSide, bottomSide, leftTrigger, rightTrigger};
            GameScene scene1 = new GameScene(scene1GameObjects);
            gameManager.gameScenes.Add(scene1);

            gameManager.Tick += GameManager_Tick;
        }

        private void GameManager_Tick() //Game loop
        {
            SetupPlayerMovement();
        }

        void SetupPlayerMovement() //Both handle movement
        {
            if (gameManager.keysDown.Contains(Keys.W) && leftHadle.gameObjectLocation.Y + leftHadle.boundingBox.min.Y > 0)
            {
                leftHadle.objectVelocity.Y = -1;
            }
            else if (gameManager.keysDown.Contains(Keys.S) && leftHadle.gameObjectLocation.Y + leftHadle.boundingBox.max.Y < gameManager.mainGameEnginePanel.Size.Height)
            {
                leftHadle.objectVelocity.Y = 1;
            }
            else
            {
                leftHadle.objectVelocity.Y = 0;
            }

            if (gameManager.keysDown.Contains(Keys.Up) && rightHandle.gameObjectLocation.Y + rightHandle.boundingBox.min.Y > 0)
            {
                rightHandle.objectVelocity.Y = -1;
            }
            else if (gameManager.keysDown.Contains(Keys.Down) && rightHandle.gameObjectLocation.Y + rightHandle.boundingBox.max.Y < gameManager.mainGameEnginePanel.Size.Height)
            {
                rightHandle.objectVelocity.Y = 1;
            }
            else
            {
                rightHandle.objectVelocity.Y = 0;
            }
        }

        void BallCollision(GameObject2D _sender, GameObject2D _other)
        {
            if (_other.objectTag == "handle") //Hit one of the handles
            {
                ballObject.objectVelocity = new Point(ballObject.objectVelocity.X * -1, ballObject.objectVelocity.Y);
            }
            else if (_other.objectTag == "side") //Hit top / bottom of screen
            {
                ballObject.objectVelocity = new Point(ballObject.objectVelocity.X, ballObject.objectVelocity.Y * -1);
            }
            else if (_other.objectTag == "leftTrigger") //Add point to right side
            {
                rightScore++;
                rightScoreText.text = rightScore.ToString();
                ballObject.gameObjectLocation = new Point(200, 100);
            }
            else if (_other.objectTag == "rightTrigger") //Add point to left side
            {
                leftScore++;
                leftScoreText.text = leftScore.ToString();
                ballObject.gameObjectLocation = new Point(200, 100);
            }
            else
            {
                throw new Exception(_other.objectTag);
            }
        }
    }
}
