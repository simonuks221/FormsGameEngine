using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace FormsGameEngine
{
    public struct WidgetControlInfo
    {
        public Type controlType;
        public Size controlSize;
        public Point controlReliativeLocation;

        public WidgetControlInfo(Type _controlType, Size _controlSize, Point _reliativeControlLocation)
        {
            controlType = _controlType;
            controlSize = _controlSize;
            controlReliativeLocation = _reliativeControlLocation;
        }
    }


    public class BaseWidget
    {
        GameManager gameManager;

        public Point widgetLocationOnScreen;

        protected List<WidgetControlInfo> widgetControlsInfo;
        public List<Control> widgetControls = new List<Control>();

        protected virtual void SetupWidgetControlsInfo()
        {
            widgetControlsInfo = null;
        }

        public BaseWidget(GameManager _gameManager, Point _WidgetLocation)
        {
            SetupWidgetControlsInfo();
            gameManager = _gameManager;
            widgetLocationOnScreen = _WidgetLocation;
        }

        public virtual void UpdateWidget(MainGameEnginePanel _uiPanel)
        {
            if(widgetControlsInfo != null)
            {
                for(int i = 0; i < widgetControlsInfo.Count; i++)
                {
                    if(widgetControls.Count > i) //Widget may exist
                    {
                        if(widgetControls[i] != null)
                        {
                            continue; //Widget control exists, everythings good
                        }
                        else
                        {
                            widgetControls.RemoveAt(i);
                        }
                    }
                    //Widget control doesnt exist, make a new one and add it to panel
                    Control newControl = (Control)Activator.CreateInstance(widgetControlsInfo[i].controlType);
                    newControl.Location = new Point(widgetLocationOnScreen.X + widgetControlsInfo[i].controlReliativeLocation.X, widgetLocationOnScreen.Y + widgetControlsInfo[i].controlReliativeLocation.Y);
                    newControl.Size = widgetControlsInfo[i].controlSize;
                    widgetControls.Add(newControl);

                    _uiPanel.Controls.Add(newControl);
                    newControl.BringToFront();
                }
            }
            else
            {
                Console.Out.WriteLine(widgetControlsInfo == null);
                throw new Exception("Widget controls not set, dont make empty widgets.");
            }
        }
    }
}
