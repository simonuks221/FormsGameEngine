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
        public GameManager gameManager;

        public GameObject(GameManager _gameManager)
        {
            gameManager = _gameManager;
        }
        public abstract void UpdateObject(MainGameEnginePanel _mainGameEnginePanel);

        public virtual void UpdateObjectOverride()
        {

        }

        public virtual void Destroy(int _delay = 0)
        {
            gameManager.Delayed(_delay, () => gameManager.gameScenes[0].gameObjects.Remove(this));
        }
    }

    public abstract class GameObjectControl : GameObject
    {
        public Control gameObjectControl;
        public Point gameObjectLocation;

        public GameObjectControl(GameManager _gameManager,Point _gameObjectLocation) : base(_gameManager)
        {
            gameObjectLocation = _gameObjectLocation;
        }

        public override void UpdateObject(MainGameEnginePanel _mainGameEnginePanel)
        {
            this.gameObjectControl.MouseMove += gameManager.MouseMoveOnGamePanel;
            //this.gameObjectControl.Location = PointHelper.Add(this.gameObjectControl.Location, gameManager.cameraLocation);
        }

        public override void Destroy(int _delay = 0)
        {
            gameManager.Delayed(_delay, () => DestroyThis());
        }

        private void DestroyThis()
        {
            if (gameObjectControl != null)
            {
                gameObjectControl.Dispose();
            }
            gameManager.gameScenes[0].gameObjects.Remove(this);
        }
    }
}
