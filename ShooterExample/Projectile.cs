using FormsGameEngine;
using System.Drawing;

namespace ShooterExample
{
    class Projectile : Box2dGameObject
    {
        public Projectile(GameManager _gameManager, Point _location, PointF _velocity) : base(_gameManager, _location, new Size(3, 3))
        {
            this.colliding = true;
            this.boxColor = Color.Yellow;
            this.objectVelocity = _velocity;

            Destroy(5000);
        }
    }
}
