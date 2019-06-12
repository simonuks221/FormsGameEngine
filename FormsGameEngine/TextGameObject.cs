using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace FormsGameEngine
{
    public class TextGameObject : BaseWidget
    {
        public static List<WidgetControlInfo> widgetControlsInfo = new List<WidgetControlInfo>() { new WidgetControlInfo(typeof(TransparentLabel), new Size(7, 11), new Point(0, 0))};


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
                TransparentLabel label = this.widgetControls[0] as TransparentLabel;
                if(label != null)
                {
                    label.Text = textGameObjectText;
                }
            }
        }

        public TextGameObject(GameManager _gameManager, Point _textLocation) : base(_gameManager, _textLocation)
        {
            //text = "Null";
        }
    }
}
