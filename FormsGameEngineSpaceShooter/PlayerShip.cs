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
        float fireRate = 0.3f;
        float lastFire = -1000;

        int playerSpeed = 2;

        public PlayerShip(GameManager _gameManager, Point _playerLocation) : base(_gameManager,_playerLocation, new Size(20, 20))
        {
            this.cubeColor = Color.Green;
            this.colliding = true;
            this.objectTag = "player";
            this.ignoreCollisionTags = new List<string>() {"enemy"};
        }

        public override void UpdateObjectOverride()
        {
            //Handle player movement
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

            if (gameManager.gameTime - lastFire >= fireRate)
            {
                if (gameManager.keysDown.Contains(Keys.Space))
                {
                    PlayerProjectile newProjectile = new PlayerProjectile(gameManager, new Point(this.gameObjectLocation.X + 7, this.gameObjectLocation.Y - 10));
                    newProjectile.colliding = true;
                    gameManager.AddGameObjectToScene(newProjectile, gameManager.currentActiveScene);
                    lastFire = gameManager.gameTime;
                }
            }
        }
    }
}
