using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsGameEngine
{
    public class UiManager
    {
        public List<BaseWidget> widgets = new List<BaseWidget>() { };

        public UiManager(List<BaseWidget> _widgets = null)
        {
            if(_widgets != null)
            {
                widgets = _widgets;
            }
        }

        public void AddWidget(BaseWidget _widgetToAdd)
        {
            widgets.Add(_widgetToAdd);
        }
    }

}
