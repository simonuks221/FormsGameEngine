using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormsGameEngine;
using System.Drawing;
using System.Windows.Forms;

namespace ShooterExample
{
    class Player : Box2dGameObject
    {
        int playerSpeed = 1;

        public Player(GameManager _gameManager, Point _location) : base(_gameManager, _location, new Size(7, 7))
        {
            this.cubeColor = Color.Green;
            this.colliding = true;
        }

        public override void UpdateObject(MainGameEnginePanel _mainGameEnginePanel)
        {
            if (gameManager.keysDown.Contains(Keys.W))
            {
                this.objectVelocity = new Point(this.objectVelocity.X, -playerSpeed);
            }
            else if (gameManager.keysDown.Contains(Keys.S))
            {
                this.objectVelocity = new Point(this.objectVelocity.X, playerSpeed);
            }
            else
            {
                this.objectVelocity = new Point(this.objectVelocity.X, 0);
            }

            if (gameManager.keysDown.Contains(Keys.D))
            {
                this.objectVelocity = new Point(playerSpeed, this.objectVelocity.Y);
            }
            else if (gameManager.keysDown.Contains(Keys.A))
            {
                this.objectVelocity = new Point(-playerSpeed, this.objectVelocity.Y);
            }
            else
            {
                this.objectVelocity = new Point(0, this.objectVelocity.Y);
            }

            base.UpdateObject(_mainGameEnginePanel);
        }
    }
}
