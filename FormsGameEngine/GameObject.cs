using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace FormsGameEngine
{
    public abstract class GameObject
    {
        public string objectTag;

        public GameObject()
        {
            
        }
        public abstract void UpdateObject(MainGameEnginePanel _mainGameEnginePanel);
    }

    public abstract class GameObjectControl : GameObject
    {
        public Control gameObjectControl;
        public Point gameObjectLocation;

        public GameObjectControl(Point _gameObjectLocation)
        {
            gameObjectLocation = _gameObjectLocation;
        }
    }
}
