using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace FormsGameEngine
{
    public class GameObject
    {
        public Control gameObjectControl;
        public Point gameObjectLocation;

        public GameObject(Point _gameObjectLocation)
        {
            gameObjectLocation = _gameObjectLocation;
        }

        public void UpdateDisplay(MainGameEnginePanel _mainGameEnginePanel)
        {
            if(gameObjectControl != null)
            {
                gameObjectControl.Dispose();
            }

            Panel newPanel = new Panel();
            newPanel.Size = new Size(10, 10);
            newPanel.BackColor = Color.Red;

            gameObjectControl = newPanel;
            gameObjectControl.Location = gameObjectLocation;

            _mainGameEnginePanel.Controls.Add(gameObjectControl);
        }
    }
}
