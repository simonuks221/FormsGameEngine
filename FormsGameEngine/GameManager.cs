using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                for (int y = 0; y < collidingObjects.Count; y++) //Check collisions agains other objects
                {
                    if(y != i)
                    {
                        if(collidingObjects[i].gameObjectLocation == collidingObjects[y].gameObjectLocation)
                        {
                            collidingObjects[i].Collision(collidingObjects[y]);
                            break;
                        }
                    }
                }
            }
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
