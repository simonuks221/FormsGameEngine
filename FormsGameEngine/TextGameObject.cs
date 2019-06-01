using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace FormsGameEngine
{
    public class TextGameObject : GameObjectControl
    {
        public string text;

        public TextGameObject(GameManager _gameManager, Point _textLocation) : base(_gameManager,_textLocation)
        {
            text = "Null";
        }

        public override void UpdateObject(MainGameEnginePanel _mainGameEnginePanel)
        {
            Label label;
            if (gameObjectControl == null)
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
