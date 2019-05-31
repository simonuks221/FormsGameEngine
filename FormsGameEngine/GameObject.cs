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

    public class TextGameObject : GameObjectControl
    {
        public string text;

        public TextGameObject(Point _textLocation) : base(_textLocation)
        {
            text = "Null";
        }

        public override void UpdateObject(MainGameEnginePanel _mainGameEnginePanel)
        {
            Label label;
            if(gameObjectControl == null)
            {
                label = new Label();
                gameObjectControl = label;
                _mainGameEnginePanel.Controls.Add(gameObjectControl);
            }
            else
            {
                label = (Label)gameObjectControl;
            }
            gameObjectControl.Location = gameObjectLocation;
            label.Text = text;
        }
    }
}
