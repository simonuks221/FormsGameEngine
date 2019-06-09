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
        string textGameObjectText;

        public string text
        {
            get
            {
                return textGameObjectText;
            }
            set
            {
                textGameObjectText = value;
                TransparentLabel label = this.gameObjectControl as TransparentLabel;
                if(label != null)
                {
                    label.Text = textGameObjectText;
                }
            }
        }

        public TextGameObject(GameManager _gameManager, Point _textLocation) : base(_gameManager,_textLocation)
        {
            textGameObjectText = "Null";
        }

        public override void UpdateObject(MainGameEnginePanel _mainGameEnginePanel)
        {
            TransparentLabel transparentLabel;
            if (gameObjectControl == null)
            {
                transparentLabel = new TransparentLabel();
                transparentLabel.Location = gameObjectLocation;
                transparentLabel.Text = textGameObjectText;
                transparentLabel.Size = new Size(7, 11); //1 letter requires size of (7, 11)
                gameObjectControl = transparentLabel;
                _mainGameEnginePanel.Controls.Add(gameObjectControl);
            }
            else
            {
                transparentLabel = (TransparentLabel)gameObjectControl;
            }
        }
    }
}
