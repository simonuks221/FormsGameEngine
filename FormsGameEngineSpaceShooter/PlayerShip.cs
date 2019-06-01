using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormsGameEngine;
using System.Drawing;
using System.Windows.Forms;

namespace FormsGameEngineSpaceShooter
{
    class PlayerShip : Box2dGameObject
    {
        float fireRate = 1;
        float lastFire = -1000;

        public PlayerShip(GameManager _gameManager, Point _playerLocation) : base(_gameManager,_playerLocation, new Size(20, 20), Color.Green)
        {

        }

        public override void UpdateObjectOverride()
        {
            //Handle player movement
            if (gameManager.keysDown.Contains(Keys.D))
            {
                this.objectVelocity = new Point(1, this.objectVelocity.Y);
            }
            else if (gameManager.keysDown.Contains(Keys.A))
            {
                this.objectVelocity = new Point(-1, this.objectVelocity.Y);
            }
            else
            {
                this.objectVelocity = new Point(0, this.objectVelocity.Y);
            }

            if (gameManager.keysDown.Contains(Keys.W))
            {
                this.objectVelocity = new Point(this.objectVelocity.X, -1);
            }
            else if (gameManager.keysDown.Contains(Keys.S))
            {
                this.objectVelocity = new Point(this.objectVelocity.X, 1);
            }
            else
            {
                this.objectVelocity = new Point(this.objectVelocity.X, 0);
            }

            if (gameManager.gameTime - lastFire >= fireRate)
            {
                if (gameManager.keysDown.Contains(Keys.Space))
                {
                    PlayerProjectile newProjectile = new PlayerProjectile(gameManager, new Point(this.gameObjectLocation.X, this.gameObjectLocation.Y - 10));
                    newProjectile.solid = true;
                    gameManager.AddGameObjectToScene(newProjectile, gameManager.currentActiveScene);
                    lastFire = gameManager.gameTime;
                }
            }
        }
    }
}
