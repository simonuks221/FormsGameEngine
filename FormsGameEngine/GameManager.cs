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
        public int currentActiveScene = 0;

        public List<Keys> keysDown = new List<Keys>();

        public GameManager(Form _form, MainGameEnginePanel _mainGameEnginePanel)
        {
            form = _form;
            form.KeyDown += Form_KeyDown;
            form.KeyUp += Form_KeyUp;
            mainGameEnginePanel = _mainGameEnginePanel;
            gameScenes = new List<GameScene>();

            GameCycle();
        }

        #region GameCycle
        void GameCycle()
        {
            Delayed(1, () => UpdateScreen());
        }

        void UpdateScreen()
        {
            ApplyVelocity();
            CheckCollisions();
            Tick();
            UpdateCurrentGamePanel();
            GameCycle();
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

        public void ApplyVelocity()
        {
            List<GameObject2D> collidingObjects = new List<GameObject2D>();
            foreach (GameObject obj in gameScenes[currentActiveScene].gameObjects) //Populate colliding objects list
            {
                GameObject2D ob = (GameObject2D)obj;
                if (obj != null)
                {
                    collidingObjects.Add(ob);
                }
            }

            foreach (GameObject2D obj in collidingObjects) //Aplly velocity
            {
                if (obj.objectVelocity != new System.Drawing.Point(0, 0))
                {
                    GameObject2D other = null;
                    if (!IsLocationBlocked(obj, new Point(obj.boundingBox.max.X + obj.objectVelocity.X, obj.boundingBox.max.Y + obj.objectVelocity.Y), collidingObjects, out other)
                        && !IsLocationBlocked(obj, new Point(obj.boundingBox.min.X + obj.objectVelocity.X, obj.boundingBox.min.Y + obj.objectVelocity.Y), collidingObjects, out other)
                        )
                    {
                        obj.gameObjectLocation.X += obj.objectVelocity.X;
                        obj.gameObjectLocation.Y += obj.objectVelocity.Y;
                    }
                }
            }
        }

        public void CheckCollisions()
        {
            List<GameObject2D> collidingObjects = new List<GameObject2D>();
            foreach (GameObject obj in gameScenes[currentActiveScene].gameObjects) //Populate colliding objects list
            {
                GameObject2D ob = (GameObject2D)obj;
                if (obj != null)
                {
                    collidingObjects.Add(ob);
                }
            }

            for (int i = 0; i < collidingObjects.Count; i++) //Check collsisions
            {
                GameObject2D other = null;
                if(IsGameObjectColliding(collidingObjects[i], collidingObjects, out other))
                {
                    collidingObjects[i].Collision(other);
                }
            }

        }

        bool IsGameObjectColliding(GameObject2D _object, List<GameObject2D> _collidingObjects, out GameObject2D _other)
        {
            for (int y = 0; y < _collidingObjects.Count; y++) //Check collisions agains other objects
            {
                if (_collidingObjects[y] != _object)
                {
                    if (_object.gameObjectLocation == _collidingObjects[y].gameObjectLocation)
                    {
                        _other = _collidingObjects[y];
                        return true;
                    }
                }
            }
            _other = null;
            return false;
        }

        bool IsLocationBlocked(GameObject2D _object, Point _location, List<GameObject2D> _collidingObjects, out GameObject2D _other)
        {
            for (int y = 0; y < _collidingObjects.Count; y++) //Check collisions agains other objects
            {
                if (_collidingObjects[y] != _object)
                {
                    if ((_location.X > _collidingObjects[y].boundingBox.min.X && _location.Y > _collidingObjects[y].boundingBox.min.Y) && (_location.X < _collidingObjects[y].boundingBox.max.X && _location.Y < _collidingObjects[y].boundingBox.max.Y))
                    {
                        _other = _collidingObjects[y];
                        return true;
                    }
                }
            }
            _other = null;
            return false;
        }

        public void UpdateCurrentGamePanel()
        {
            for(int i = 0; i < gameScenes[currentActiveScene].gameObjects.Count; i++)
            {
                gameScenes[currentActiveScene].gameObjects[i].UpdateObject(mainGameEnginePanel);
            }
        }
        #endregion

        #region KeyHandling
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (!keysDown.Contains(e.KeyCode))
            {
                keysDown.Add(e.KeyCode);
            }
        }

        private void Form_KeyUp(object sender, KeyEventArgs e)
        {
            keysDown.Remove(e.KeyCode);
        }
        #endregion
    }
}
