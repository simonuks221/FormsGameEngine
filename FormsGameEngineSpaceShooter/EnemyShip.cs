using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormsGameEngine;
using System.Drawing;

namespace FormsGameEngineSpaceShooter
{
    class EnemyShip : Box2dGameObject
    {
        float health = 20;

        public EnemyShip(GameManager _gameManager, Point _enemyLocation, Size _enemySize) : base(_gameManager, _enemyLocation, _enemySize)
        {
            this.boxColor = Color.Red;
            this.colliding = true;
            this.objectTag = "enemy";
        }

        public void DoDamage(float amountOfDamage)
        {
            health -= amountOfDamage;
            if(health <= 0)
            {
                this.Destroy();
            }
        }

       
    }
}
