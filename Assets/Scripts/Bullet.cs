using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += transform.right * speed * Time.deltaTime;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy _enemy = collision.gameObject.GetComponent<Enemy>();
            HitEnemy(_enemy, collision.GetContact(0).point);
        }

        if (collision.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == "EnemyRemover")
        {
            Destroy(this.gameObject);
        }
    }
    public virtual void HitEnemy(Enemy _enemy, Vector2 collPoint)
    {
        _enemy.Stun(((Vector2)_enemy.transform.position - collPoint.normalized));
        Destroy(gameObject);
    }
}
