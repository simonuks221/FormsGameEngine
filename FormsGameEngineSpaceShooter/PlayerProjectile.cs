using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using FormsGameEngine;

namespace FormsGameEngineSpaceShooter
{
    class PlayerProjectile : Box2dGameObject
    {
        float damage = 10;

        public PlayerProjectile(GameManager _gameManager, Point _projectileLocation) : base(_gameManager, _projectileLocation, new Size(5, 5), Color.Yellow)
        {
            this.objectVelocity = new Point(0, -3);
            OnCollision += PlayerProjectile_OnCollision;
        }

        private void PlayerProjectile_OnCollision(GameObject2D _sender, GameObject2D _other)
        {
            EnemyShip enemyShip = _other as EnemyShip;
            if(enemyShip != null)
            {
                enemyShip.DoDamage(damage);
                this.Destroy();
            }
        }
    }
}
