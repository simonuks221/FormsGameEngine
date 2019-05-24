using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace FormsGameEngine
{
    public class MainGameEnginePanel : Panel
    {
        public MainGameEnginePanel(Size _panelSize)
        {
            this.Size = _panelSize;
            this.BackColor = Color.LightGray;
        }
    }
}
