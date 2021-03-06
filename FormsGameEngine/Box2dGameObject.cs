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
        public Color boxColor;
        public Image cubeImage;

        public Box2dGameObject(GameManager _gameManager ,Point _cubeLocation, Size _cubeSize) : base(_gameManager,_cubeLocation)
        {
            cubeSize = _cubeSize;

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
                if (boxColor != null)
                {
                    panel.BackColor = boxColor;
                }
                if (cubeImage != null)
                {
                    panel.BackgroundImage = cubeImage;
                }
                gameObjectControl = panel;
                _mainGameEnginePanel.Controls.Add(gameObjectControl);
            }
            else
            {
                panel = (Panel)gameObjectControl;
            }
            this.gameObjectControl.Location = PointHelper.Subtract(this.gameObjectLocation, gameManager.cameraLocation);
            gameObjectControl.BringToFront();

            UpdateObjectOverride();
            base.UpdateObject(_mainGameEnginePanel);
        }
    }
}
