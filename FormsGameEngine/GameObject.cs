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

        public GameObject()
        {
            
        }

        public abstract void UpdateObject(MainGameEnginePanel _mainGameEnginePanel);
    }

    public abstract class GameObject2D : GameObject
    {
        public Control gameObjectControl;
        public Point gameObjectLocation;

        public GameObject2D(Point _gameObject2DLocation)
        {
            gameObjectLocation = _gameObject2DLocation;
        }
    }


    public class CubeGameObject : GameObject2D
    {
        public Size cubeSize;
        public Color cubeColor;

        public CubeGameObject(Point _cubeLocation, Size _cubeSize, Color _cubeColor) : base(_cubeLocation)
        {
            cubeSize = _cubeSize;
            cubeColor = _cubeColor;
        }

        public override void  UpdateObject(MainGameEnginePanel _mainGameEnginePanel)
        { 
            if(gameObjectControl != null)
            {
                gameObjectControl.Dispose();
            }

            Panel newPanel = new Panel();
            newPanel.Size = cubeSize;
            newPanel.BackColor = cubeColor;

            gameObjectControl = newPanel;
            gameObjectControl.Location = gameObjectLocation;

            _mainGameEnginePanel.Controls.Add(gameObjectControl);
        }
    }
}
