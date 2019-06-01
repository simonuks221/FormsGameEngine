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
        Form thisForm;

        public MainGameEnginePanel(Form _thisForm, Size _panelSize, Point _panelLocation)
        {
            thisForm = _thisForm;
            thisForm.Controls.Add(this);
            this.Size = _panelSize;
            this.BackColor = Color.LightGray;
            this.Location = _panelLocation;
        }
    }
}
