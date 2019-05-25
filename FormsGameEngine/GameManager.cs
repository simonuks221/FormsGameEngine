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
        public event EventHandler Tick;
        public delegate void TickHandler();

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

        void GameCycle()
        {
            Delayed(1, () => UpdateScreen());
        }

        void UpdateScreen()
        {
            Tick(this, new EventArgs());
            UpdateCurrentGamePanel();
            GameCycle();
        }

        public void UpdateCurrentGamePanel()
        {
            for(int i = 0; i < gameScenes[currentActiveScene].gameObjects.Count; i++)
            {
                gameScenes[currentActiveScene].gameObjects[i].UpdateObject(mainGameEnginePanel);
            }
        }

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
    }
}
