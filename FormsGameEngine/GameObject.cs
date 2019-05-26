using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace FormsGameEngine
{
    public abstract class GameObject
    {
        public string objectTag;

        public GameObject()
        {
            
        }
        public abstract void UpdateObject(MainGameEnginePanel _mainGameEnginePanel);
    }

    public abstract class GameObjectControl : GameObject
    {
        public Control gameObjectControl;
        public Point gameObjectLocation;

        public GameObjectControl(Point _gameObjectLocation)
        {
            gameObjectLocation = _gameObjectLocation;
        }
    }

    public class TextGameObject : GameObjectControl
    {
        public string text;

        public TextGameObject(Point _textLocation) : base(_textLocation)
        {
            text = "Null";
        }

        public override void UpdateObject(MainGameEnginePanel _mainGameEnginePanel)
        {
            if (gameObjectControl != null)
            {
                gameObjectControl.Dispose();
            }
            
            Label newLabel = new Label();
            newLabel.Text = text;

            gameObjectControl = newLabel;
            gameObjectControl.Location = gameObjectLocation;

            _mainGameEnginePanel.Controls.Add(gameObjectControl);
        }
    }

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

        public GameObject2D(Point _gameObject2DLocation) : base(_gameObject2DLocation)
        {
            
        }

        public void Collision( GameObject2D Collider)
        {
            OnCollision?.Invoke(this, Collider);
        }
    }


    public class CubeGameObject : GameObject2D
    {
        public Size cubeSize;
        public Color cubeColor;

        public CubeGameObject(Point _cubeLocation, Size _cubeSize, Color _cubeColor) : base(_cubeLocation)
        {
            cubeSize = _cubeSize;
            cubeColor = _cubeColor;

            Point min = new Point(0, 0);
            Point max = new Point(cubeSize.Width, cubeSize.Height);
            boundingBox = new BoundingBox(min, max);
        }

        public override void  UpdateObject(MainGameEnginePanel _mainGameEnginePanel)
        { 
            if(gameObjectControl != null)
            {
                gameObjectControl.Dispose();
            }

            Panel newPanel = new Panel();
            newPanel.Size = cubeSize;
            newPanel.BackColor = cubeColor;

            gameObjectControl = newPanel;
            gameObjectControl.Location = gameObjectLocation;

            _mainGameEnginePanel.Controls.Add(gameObjectControl);
        }
    }
}
