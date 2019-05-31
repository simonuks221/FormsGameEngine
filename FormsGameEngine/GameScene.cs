using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsGameEngine
{
    public class GameScene
    {
        public List<GameObject> gameObjects;

        public GameScene(List<GameObject> _gameObjects = null)
        {
            gameObjects = new List<GameObject>();
            if (_gameObjects != null)
            {
                gameObjects.AddRange(_gameObjects);
            }
        }

        public void AddgameObjectToScene(GameObject _gameObject)
        {
            if (_gameObject != null)
            {
                gameObjects.Add(_gameObject);
            }
        }
    }
}
