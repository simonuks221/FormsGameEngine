using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsGameEngine
{
    public class GameManager
    {
        public MainGameEnginePanel mainGameEnginePanel;
        public List<GameScene> gameScenes;
        public int currentActiveScene = 0;

        public GameManager(MainGameEnginePanel _mainGameEnginePanel)
        {
            mainGameEnginePanel = _mainGameEnginePanel;
            gameScenes = new List<GameScene>();
        }

        public void UpdateCurrentGamePanel()
        {
            for(int i = 0; i < gameScenes[currentActiveScene].gameObjects.Count; i++)
            {
                gameScenes[currentActiveScene].gameObjects[i].UpdateObject(mainGameEnginePanel);
            }
        }
    }
}
