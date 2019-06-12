using System;
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
        public List<UiManager> gameUis;
        public int currentActiveScene = 0;
        public int currentActiveUi = 0;

        public List<Keys> keysDown = new List<Keys>();

        public float gameTime = 0; //Game time in seconds

        public Point mouseLocation = new Point(0, 0);

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
                c.MouseMove += MouseMoveOnGamePanel;
            }

            mainGameEnginePanel = _mainGameEnginePanel;
            gameScenes = new List<GameScene>();
            gameUis = new List<UiManager>();

            form.Select();
            form.Focus();
            form.Focus();

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
            if (delay > 0) //Delay is valid
            {
                Timer timer = new Timer();
                timer.Interval = delay;
                timer.Tick += (s, e) =>
                {
                    action();
                    timer.Stop();
                };
                timer.Start();
            }
            else //Delay is 0 or negative
            {
                action();
            }
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

                        if (obj.colliding)
                        {
                            int biggerVelocity = Math.Abs(obj.objectVelocity.X); //Choose bigger velocity
                            if (Math.Abs(obj.objectVelocity.Y) > Math.Abs(obj.objectVelocity.X))
                            {
                                biggerVelocity = Math.Abs(obj.objectVelocity.Y);
                            }

                            int startingX = obj.gameObjectLocation.X;
                            int startingY = obj.gameObjectLocation.Y;

                            for (float i = 0.1f; i <= 1; i += 0.1f)
                            {
                                int velocityX = (int)Math.Round(startingX + obj.objectVelocity.X * i);
                                int velocityY = (int)Math.Round(startingY + obj.objectVelocity.Y * i);

                                if (!IsLocationBlocked(obj, new Point(obj.boundingBox.max.X + velocityX, obj.boundingBox.max.Y + velocityY), collidingObjects, out other)
                               && !IsLocationBlocked(obj, new Point(obj.boundingBox.min.X + velocityX, obj.boundingBox.min.Y + velocityY), collidingObjects, out other)
                               && !IsLocationBlocked(obj, new Point(obj.boundingBox.min.X + velocityX, obj.boundingBox.max.Y + velocityY), collidingObjects, out other)
                               && !IsLocationBlocked(obj, new Point(obj.boundingBox.max.X + velocityX, obj.boundingBox.min.Y + velocityY), collidingObjects, out other)
                                )
                                { ///If not colliding move up
                                    newLocX = velocityX;
                                    newLocY = velocityY;
                                }
                                else //Colliding
                                { 
                                    if (!obj.ignoreCollisionTags.Contains(other.objectTag) && !other.ignoreCollisionTags.Contains(obj.objectTag))
                                    {
                                        obj.Collision(other);
                                        other.Collision(obj);
                                        break; //Break loop if theres and obstacle in the way of objects path depending on velocity
                                    }
                                    else //Dont mind collision, objects are in  ignore list
                                    {
                                        newLocX = velocityX;
                                        newLocY = velocityY;
                                    }
                               }
                            }

                            obj.gameObjectLocation.X = newLocX;
                            obj.gameObjectLocation.Y = newLocY;
                        }
                        /*
                        if (other == null) //If no collision then move up
                        {
                            obj.gameObjectLocation.X = newLocX;
                            obj.gameObjectLocation.Y = newLocY;
                        }
                        */
                        
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
                    if (_collidingObjects[y].colliding)
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

            if(gameUis.Count > 0)
            {
                for(int i = 0; i < gameUis[currentActiveUi].widgets.Count; i++)
                {
                    gameUis[currentActiveUi].widgets[i].UpdateWidget(mainGameEnginePanel);
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

        public void MouseMoveOnGamePanel(object sender, MouseEventArgs e)
        {
            Control senderControl = sender as Control;
            if (senderControl != null)
            {
                if(senderControl == mainGameEnginePanel)
                {
                    mouseLocation = e.Location;
                }
                else
                {
                    Point newMouseLocation = new Point(senderControl.Location.X + e.Location.X, senderControl.Location.Y + e.Location.Y);
                    mouseLocation = newMouseLocation;
                }
            }
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

        public void ChangeUi(int _newUiIndex) //Not finished
        {
            foreach (BaseWidget w in gameUis[currentActiveUi].widgets) //Clear leftover Controls
            {
                for(int i = 0; i < w.widgetControls.Count; i++)
                {
                    w.widgetControls[i].Dispose();
                }
                w.widgetControls.Clear();
            }
            for (int i = 0; i < mainGameEnginePanel.Controls.Count; i++)
            {
                mainGameEnginePanel.Controls[i].Dispose();
            }

            currentActiveUi = _newUiIndex;
        }

        public void AddUi(UiManager _newUi)
        {
            gameUis.Add(_newUi);
        }

        public void AddWidgetToUi(BaseWidget _widgetToAdd, int _uiIndex)
        {
            if(_widgetToAdd != null && _uiIndex >= 0 && _uiIndex < gameUis.Count)
            {
                gameUis[_uiIndex].AddWidget(_widgetToAdd);
            }
        }
    }
}
