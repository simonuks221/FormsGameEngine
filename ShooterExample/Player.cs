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

        float lastShootTime = -10000f;
        float shootDelay = 0.3f;

        int projectileMaxVelocity = 3;

        public Player(GameManager _gameManager, Point _location) : base(_gameManager, _location, new Size(7, 7))
        {
            this.boxColor = Color.Green;
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

            if (gameManager.keysDown.Contains(Keys.Space))
            {
                if (gameManager.gameTime - lastShootTime > shootDelay)
                {
                    Console.Out.WriteLine(gameManager.mouseLocation + " " + this.gameObjectLocation);

                    Point locationDifference = PointHelper.Subtract( gameManager.mouseLocation, this.gameObjectLocation);
                    Point projectileVelocity = new Point(0, 0);
                    if (locationDifference.X > locationDifference.Y)
                    {
                        int yVelocity = locationDifference.Y * 100 / locationDifference.X;
                        projectileVelocity = new Point(projectileMaxVelocity, yVelocity * projectileMaxVelocity / 100);
                    }
                    else //Y bigger
                    {
                        int xVelocity = locationDifference.X * 100 / locationDifference.Y;
                        projectileVelocity = new Point(xVelocity * projectileMaxVelocity / 100, projectileMaxVelocity);
                    }

                    Console.Out.WriteLine(projectileVelocity);

                    Projectile newProjectile = new Projectile(gameManager, this.gameObjectLocation, projectileVelocity);
                    gameManager.AddGameObjectToScene(newProjectile, 0);

                    lastShootTime = gameManager.gameTime;
                }
            }

            base.UpdateObject(_mainGameEnginePanel);
        }
    }
}
