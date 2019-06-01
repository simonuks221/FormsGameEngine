using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FormsGameEngine
{
    public struct BoundingBox
    {
        public Point min;
        public Point max;
        public BoundingBox(Point _min, Point _max)
        {
            min = _min;
            max = _max;
        }
    }

    public abstract class GameObject2D : GameObjectControl
    {
        public delegate void CollisionHandler(GameObject2D _sender, GameObject2D _other);
        public event CollisionHandler OnCollision;

        public bool solid = false;
        public BoundingBox boundingBox;
        public Point objectVelocity = new Point(0, 0);

        public GameObject2D(GameManager _gameManager, Point _gameObject2DLocation) : base(_gameManager, _gameObject2DLocation)
        {

        }

        public void Collision(GameObject2D Collider)
        {
            OnCollision?.Invoke(this, Collider);
        }
    }
}
