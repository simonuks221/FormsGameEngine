﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace FormsGameEngine
{
    public class GameManager
    {
        public delegate void TickHandler();
        public event TickHandler Tick;

        public Form form;
        public MainGameEnginePanel mainGameEnginePanel;
        public List<GameScene> gameScenes;
        public int currentActiveScene = 0;

        public List<Keys> keysDown = new List<Keys>();

        public float gameTime = 0; //Game time in seconds

        public GameManager(Form _form, MainGameEnginePanel _mainGameEnginePanel)
        {
            form = _form;

            form.KeyDown += Form_KeyDown;
            form.KeyUp += Form_KeyUp;
            form.Click += Form_Click;
            foreach(Control c in form.Controls)
            {
                c.KeyDown += Form_KeyDown;
                c.KeyUp += Form_KeyUp;
            }

            mainGameEnginePanel = _mainGameEnginePanel;
            gameScenes = new List<GameScene>();

            form.Select();
            form.Focus();
            form.Focus();
            Console.Out.WriteLine(form.Focused);

            GameCycle();
        }

        #region GameLoop

        void GameCycle()
        {
            Delayed(1, () => UpdateScreen());
            gameTime += 1f / 60f; //Adds all game time
        }

        void UpdateScreen()
        {
            ApplyCollisionWithVelocity();
            if(Tick != null)
            {
                Tick.Invoke();
            }
            UpdateCurrentGamePanel();

            GameCycle(); //Continue the cycle
        }

        public void Delayed(int delay, Action action)
        {
            Timer timer = new Timer();
            timer.Interval = delay;
            timer.Tick += (s, e) => {
                action();
                timer.Stop();
            };
            timer.Start();
        }

        public void ApplyCollisionWithVelocity()
        {
            if (gameScenes.Count > 0)
            {
                List<GameObject2D> collidingObjects = new List<GameObject2D>();
                foreach (GameObject obj in gameScenes[currentActiveScene].gameObjects) //Populate colliding objects list
                {
                    GameObject2D ob = obj as GameObject2D;
                    if (ob != null)
                    {
                        collidingObjects.Add(ob);
                    }
                }


                foreach (GameObject2D obj in collidingObjects) //Aplly velocity
                {
                    if (obj.objectVelocity != new Point(0, 0))
                    {
                        GameObject2D other = null;

                        int newLocX = obj.gameObjectLocation.X + obj.objectVelocity.X;
                        int newLocY = obj.gameObjectLocation.Y + obj.objectVelocity.Y;

                        if (obj.solid)
                        {
                            int biggerVelocity = Math.Abs(obj.objectVelocity.X); //Choose bigger velocity
                            if (Math.Abs(obj.objectVelocity.Y) > Math.Abs(obj.objectVelocity.X))
                            {
                                biggerVelocity = Math.Abs(obj.objectVelocity.Y);
                            }

                            for (float i = 0.1f; i <= 1; i += 0.1f)
                            {
                                /*
                                int velocityX = (int)Math.Ceiling((float)obj.objectVelocity.X / i);
                                int velocityY = (int)Math.Ceiling((float)obj.objectVelocity.Y / i);

                                if (!IsLocationBlocked(obj, new Point(obj.boundingBox.max.X + obj.objectVelocity.X + obj.gameObjectLocation.X, obj.boundingBox.max.Y + obj.objectVelocity.Y + obj.gameObjectLocation.Y), collidingObjects, out other)
                                && !IsLocationBlocked(obj, new Point(obj.boundingBox.min.X + obj.objectVelocity.X + obj.gameObjectLocation.X, obj.boundingBox.min.Y + obj.objectVelocity.Y + obj.gameObjectLocation.Y), collidingObjects, out other)
                                && !IsLocationBlocked(obj, new Point(obj.boundingBox.min.X + obj.objectVelocity.X + obj.gameObjectLocation.X, obj.boundingBox.max.Y + obj.objectVelocity.Y + obj.gameObjectLocation.Y), collidingObjects, out other)
                                && !IsLocationBlocked(obj, new Point(obj.boundingBox.max.X + obj.objectVelocity.X + obj.gameObjectLocation.X, obj.boundingBox.min.Y + obj.objectVelocity.Y + obj.gameObjectLocation.Y), collidingObjects, out other)
                                ) //Old method for detecting collisions
                               */
                                int velocityX = (int)Math.Round(obj.gameObjectLocation.X + obj.objectVelocity.X * i);
                                int velocityY = (int)Math.Round(obj.gameObjectLocation.Y + obj.objectVelocity.Y * i);
                                //throw new Exception((obj.boundingBox.max.X + velocityX).ToString());
                                if (!IsLocationBlocked(obj, new Point(obj.boundingBox.max.X + velocityX, obj.boundingBox.max.Y + velocityY), collidingObjects, out other)
                               && !IsLocationBlocked(obj, new Point(obj.boundingBox.min.X + velocityX, obj.boundingBox.min.Y + velocityY), collidingObjects, out other)
                               && !IsLocationBlocked(obj, new Point(obj.boundingBox.min.X + velocityX, obj.boundingBox.max.Y + velocityY), collidingObjects, out other)
                               && !IsLocationBlocked(obj, new Point(obj.boundingBox.max.X + velocityX, obj.boundingBox.min.Y + velocityY), collidingObjects, out other)
                                )
                                { ///If not colliding move up
                                    newLocX = velocityX;
                                    newLocY = velocityY;
                                }
                                else
                                { //Colliding, invoke collision, dont move
                                    obj.Collision(other);
                                    other.Collision(obj);
                                    break; //Break loop if theres and obstacle in the way of objects path depending on velocity
                                }
                            }
                        }
                        if (other == null) //If no collision then move up
                        {
                            obj.gameObjectLocation.X = newLocX;
                            obj.gameObjectLocation.Y = newLocY;
                        }
                    }
                }
            }
        }

        bool IsLocationBlocked(GameObject2D _object, Point _location, List<GameObject2D> _collidingObjects, out GameObject2D _other)
        {
            for (int y = 0; y < _collidingObjects.Count; y++) //Check collisions agains other objects
            {
                if (_collidingObjects[y] != _object)
                {
                    if (_collidingObjects[y].solid)
                    {
                        if ((_location.X >= _collidingObjects[y].boundingBox.min.X + _collidingObjects[y].gameObjectLocation.X && _location.Y >= _collidingObjects[y].boundingBox.min.Y + _collidingObjects[y].gameObjectLocation.Y) && (_location.X <= _collidingObjects[y].boundingBox.max.X + _collidingObjects[y].gameObjectLocation.X && _location.Y <= _collidingObjects[y].boundingBox.max.Y + _collidingObjects[y].gameObjectLocation.Y))
                        {
                            _other = _collidingObjects[y];
                            return true;
                        }
                    }
                }
            }
            _other = null;
            return false;
        }

        public void UpdateCurrentGamePanel()
        {
            if (gameScenes.Count > 0)
            {
                for (int i = 0; i < gameScenes[currentActiveScene].gameObjects.Count; i++)
                {
                    gameScenes[currentActiveScene].gameObjects[i].UpdateObject(mainGameEnginePanel);
                }
            }
        }
        #endregion

        #region KeyHandling
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox isTextBox = sender as TextBox;
            if (isTextBox == null)
            {
                if (!keysDown.Contains(e.KeyCode))
                {
                    keysDown.Add(e.KeyCode);
                }
            }
        }

        private void Form_KeyUp(object sender, KeyEventArgs e)
        {
            keysDown.Remove(e.KeyCode);
        }
        #endregion

        private void Form_Click(object sender, EventArgs e) //Set focus to null if form is clicked
        {
            form.ActiveControl = null;
        }

        public void AddGameObjectToScene(GameObject _gameObject, int _sceneIndex)
        {
            if (_gameObject != null && _sceneIndex >= 0 && _sceneIndex < gameScenes.Count)
            {
                gameScenes[_sceneIndex].gameObjects.Add(_gameObject);
            }

        }

        public void AddScene(GameScene _gameScene)
        {
            gameScenes.Add(_gameScene);
        }

        public void ChangeScene(int _newSceneIndex)
        {
            foreach (GameObject g in gameScenes[currentActiveScene].gameObjects) //Clear leftover Controls
            {
                GameObjectControl c = g as GameObjectControl;
                if (c != null)
                {
                    c.gameObjectControl.Dispose();
                }
            }
            for (int i = 0; i < mainGameEnginePanel.Controls.Count; i++)
            {
                mainGameEnginePanel.Controls[i].Dispose();
            }

            currentActiveScene = _newSceneIndex;
        }
    }
}
