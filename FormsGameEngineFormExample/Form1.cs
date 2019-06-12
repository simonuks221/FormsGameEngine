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
        Box2dGameObject leftHandle;
        Box2dGameObject rightHandle;
        Box2dGameObject ballObject;
        TextGameObject rightScoreText;
        TextGameObject leftScoreText;
        TextGameObject victoryText;
        TextGameObject gameTimeText;

        int handleSpeed = 2;
        int ballSpeed = 2;

        int leftScore = 0;
        int rightScore = 0;

        bool aiOponent = true;
        int victoryScoreAmount = 3;

        List<Point> ballVelocities;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MainGameEnginePanel mainGameEnginePanel = new MainGameEnginePanel(this, new Size(400, 200), new Point(0, 0));
            gameManager = new GameManager(this, mainGameEnginePanel);

            Box2dGameObject leftTrigger = new Box2dGameObject(gameManager, new Point(-1, 0), new Size(1, 200));
            leftTrigger.objectTag = "leftTrigger";
            leftTrigger.colliding = true;
            Box2dGameObject rightTrigger = new Box2dGameObject(gameManager, new Point(400, 0), new Size(1, 200));
            rightTrigger.objectTag = "rightTrigger";
            rightTrigger.colliding = true;

            Box2dGameObject topSide = new Box2dGameObject(gameManager, new Point(0, -1), new Size(400, 1));
            topSide.objectTag = "side";
            topSide.colliding = true;
            Box2dGameObject bottomSide = new Box2dGameObject(gameManager, new Point(0, 200), new Size(400, 1));
            bottomSide.objectTag = "side";
            bottomSide.colliding = true;

            leftHandle = new Box2dGameObject(gameManager, new Point(25, 100), new Size(10, 50));
            leftHandle.cubeColor = Color.Green;
            leftHandle.colliding = true;
            leftHandle.objectTag = "handle";
            rightHandle = new Box2dGameObject(gameManager, new Point(375, 100), new Size(10, 50));
            rightHandle.cubeColor = Color.Green;
            rightHandle.colliding = true;
            rightHandle.objectTag = "handle";

            ballObject = new Box2dGameObject(gameManager, new Point(200, 100), new Size(10, 10));
            ballObject.cubeColor = Color.Black;
            Random r = new Random();
            ballVelocities = new List<Point>() { new Point(ballSpeed, ballSpeed), new Point(-ballSpeed, -ballSpeed), new Point(-ballSpeed, ballSpeed), new Point(ballSpeed, -ballSpeed) };
            ballObject.objectVelocity = ballVelocities[r.Next(0, ballVelocities.Count())];
            ballObject.objectTag = "ball";
            ballObject.colliding = true;
            ballObject.OnCollision += BallCollision;

            rightScoreText = new TextGameObject(gameManager, new Point(350, 0));
            rightScoreText.text = rightScore.ToString();
            leftScoreText = new TextGameObject(gameManager, new Point(20, 0));
            leftScoreText.text = leftScore.ToString();

            gameTimeText = new TextGameObject(gameManager, new Point(200, 0));
            gameTimeText.text = "0";

            List<GameObject> scene1GameObjects = new List<GameObject>() {leftHandle, rightHandle, ballObject, topSide, bottomSide, leftTrigger, rightTrigger};
            GameScene gameScene = new GameScene(scene1GameObjects);
            gameManager.AddScene(gameScene);

            UiManager gameUi = new UiManager();
            gameUi.AddWidget(gameTimeText);
            gameManager.AddUi(gameUi);

            victoryText = new TextGameObject(gameManager, new Point(200, 100));
            GameScene victoryScene = new GameScene();
            gameManager.AddScene(victoryScene);

            UiManager victoryUi = new UiManager();
            victoryUi.AddWidget(victoryText);
            gameManager.AddUi(victoryUi);

            VictoryScoreAmountTextBox.Text = victoryScoreAmount.ToString();
            BallSpeedTextBox.Text = ballSpeed.ToString();

            gameManager.Tick += GameManager_Tick;
        }

        private void GameManager_Tick() //Game loop
        {
            SetupPlayerMovement();
            if (aiOponent)
            {
                AiOponentMovement();
            }

            gameTimeText.text = (gameManager.gameTime).ToString();
        }

        void SetupPlayerMovement() //Both handle movement
        {
            if (gameManager.keysDown.Contains(Keys.W) && leftHandle.gameObjectLocation.Y + leftHandle.boundingBox.min.Y > 0)
            {
                leftHandle.objectVelocity.Y = -handleSpeed;
            }
            else if (gameManager.keysDown.Contains(Keys.S) && leftHandle.gameObjectLocation.Y + leftHandle.boundingBox.max.Y < gameManager.mainGameEnginePanel.Size.Height)
            {
                leftHandle.objectVelocity.Y = handleSpeed;
            }
            else
            {
                leftHandle.objectVelocity.Y = 0;
            }

            if (!aiOponent)
            {

                if (gameManager.keysDown.Contains(Keys.Up) && rightHandle.gameObjectLocation.Y + rightHandle.boundingBox.min.Y > 0)
                {
                    rightHandle.objectVelocity.Y = -handleSpeed;
                }
                else if (gameManager.keysDown.Contains(Keys.Down) && rightHandle.gameObjectLocation.Y + rightHandle.boundingBox.max.Y < gameManager.mainGameEnginePanel.Size.Height)
                {
                    rightHandle.objectVelocity.Y = handleSpeed;
                }
                else
                {
                    rightHandle.objectVelocity.Y = 0;
                }
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
            else if (_other.objectTag.Contains("Trigger")) //Hit left or right trigger
            {
                if (_other.objectTag == "leftTrigger") //Add points to right
                {
                    rightScore++;
                    rightScoreText.text = rightScore.ToString();
                }
                else //Add points to left
                {
                    leftScore++;
                    leftScoreText.text = leftScore.ToString();
                }

                if (leftScore >= victoryScoreAmount || rightScore >= victoryScoreAmount) //Winning conditions
                {
                    gameManager.ChangeScene(1);
                    gameManager.ChangeUi(1);

                    if(leftScore >= victoryScoreAmount) //Left won
                    {
                        victoryText.text = "Left";
                    }
                    else //Right won
                    {
                        victoryText.text = "Right";
                    }
                    victoryText.text += " won";
                }
                else //No winners yet, continue the game
                {
                    ballObject.gameObjectLocation = new Point(200, 100);

                    Random r = new Random();
                    ballObject.objectVelocity = ballVelocities[r.Next(0, ballVelocities.Count)];
                }
            }
            else
            {
                throw new Exception(_other.objectTag + " ball hit something bad");
            }
        }

        void AiOponentMovement()
        {
            if(ballObject.gameObjectLocation.Y > rightHandle.gameObjectLocation.Y + rightHandle.cubeSize.Height / 2)
            {
                rightHandle.objectVelocity = new Point(0, 1);
            }
            else if (ballObject.gameObjectLocation.Y < rightHandle.gameObjectLocation.Y + rightHandle.cubeSize.Height / 2)
            {
                rightHandle.objectVelocity = new Point(0, -1);
            }
        }

        private void EnemyAiCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            aiOponent = EnemyAiCheckBox.Checked;
        }

        private void BallSpeedTextBox_TextChanged(object sender, EventArgs e)
        {
            int newBallSpeed = 0;
            if (Int32.TryParse(BallSpeedTextBox.Text, out newBallSpeed)) //user inputed a valid speed
            {
                ballSpeed = newBallSpeed;
                ballVelocities = new List<Point>() { new Point(ballSpeed, ballSpeed), new Point(-ballSpeed, -ballSpeed), new Point(-ballSpeed, ballSpeed), new Point(ballSpeed, -ballSpeed) };

                if (ballObject.objectVelocity.X != 0)
                {
                    if(ballObject.objectVelocity.X > 0)
                    {
                        ballObject.objectVelocity.X = ballSpeed;
                    }
                    else
                    {
                        ballObject.objectVelocity.X = -ballSpeed;
                    }
                }
                if (ballObject.objectVelocity.Y != 0)
                {
                    if(ballObject.objectVelocity.Y > 0)
                    {
                        ballObject.objectVelocity.Y = ballSpeed;
                    }
                    else
                    {
                        ballObject.objectVelocity.Y = -ballSpeed;
                    }
                }
            }
        }

        private void VictoryScoreAmountTextBox_TextChanged(object sender, EventArgs e)
        {
            int newVictoryScore = 0;
            if (Int32.TryParse(VictoryScoreAmountTextBox.Text, out newVictoryScore)) //user inputed a valid speed
            {
                victoryScoreAmount = newVictoryScore;
            }
        }
    }
}
