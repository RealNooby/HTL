using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Bullet
{
    public float bombRadius;
	public override void HitEnemy(Enemy _enemy,Vector2 collPoint)
    {
        Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, bombRadius);
        for (int i = 0; i < colls.Length; i++)
        {
            Destroy(_enemy.gameObject);
        }
    }
}
