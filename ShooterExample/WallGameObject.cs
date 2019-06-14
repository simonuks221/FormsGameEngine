using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormsGameEngine;
using System.Drawing;

namespace ShooterExample
{
    class WallGameObject : Box2dGameObject
    {
        public WallGameObject(GameManager _gameManager, Point _location) : base(_gameManager, _location, new Size(10, 10))
        {
            this.boxColor = Color.Black;
            this.colliding = true;
        }
    }
}
