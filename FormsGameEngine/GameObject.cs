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
        public Control gameObjectControl;
        public Point gameObjectLocation;

        public GameObject(Point _gameObjectLocation)
        {
            gameObjectLocation = _gameObjectLocation;
        }

        public abstract void UpdateDisplay(MainGameEnginePanel _mainGameEnginePanel);
    }

    public class CubeGameObject : GameObject
    {
        public Size cubeSize;
        public Color cubeColor;

        public CubeGameObject(Point _cubeLocation, Size _cubeSize, Color _cubeColor) : base(_cubeLocation)
        {
            cubeSize = _cubeSize;
            cubeColor = _cubeColor;
        }

        public override void  UpdateDisplay(MainGameEnginePanel _mainGameEnginePanel)
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
