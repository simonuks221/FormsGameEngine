﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace FormsGameEngine
{
    public class Box2dGameObject : GameObject2D
    {
        public Size cubeSize;
        public Color cubeColor;

        public Box2dGameObject(Point _cubeLocation, Size _cubeSize, Color _cubeColor) : base(_cubeLocation)
        {
            cubeSize = _cubeSize;
            cubeColor = _cubeColor;

            Point min = new Point(0, 0);
            Point max = new Point(cubeSize.Width, cubeSize.Height);
            boundingBox = new BoundingBox(min, max);
        }

        public override void UpdateObject(MainGameEnginePanel _mainGameEnginePanel)
        {
            Panel panel;
            if (gameObjectControl == null)
            {
                panel = new Panel();
                panel.Size = cubeSize;
                panel.BackColor = cubeColor;
                gameObjectControl = panel;
                _mainGameEnginePanel.Controls.Add(gameObjectControl);
            }
            else
            {
                panel = (Panel)gameObjectControl;
            }
            gameObjectControl.Location = gameObjectLocation;
            gameObjectControl.BringToFront();
        }
    }
}