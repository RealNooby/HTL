using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public Bullet bulletPrefab;
    public Bomb bombPrefab;
	public Transform muzzleTransform;
    public float fireRate;
    private float Timer;

	public GameObject bombIndicator;

	public enum BombState
	{
		IDLE,
		DRAGGING
	}
	public BombState bombState;
	public ButtonX bombButton;
	// Use this for initialization
	void Start ()
    {
		//now to make a skill for removing dead enemies (AoE)
	}
	void RotationLoop()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //this is the direction
        Vector2 dir = (Vector2)transform.position - mousePos;
        float _angle = Vector2.SignedAngle(Vector2.left, dir.normalized);
        transform.rotation = Quaternion.Euler(0, 0, _angle);
    }
    void SpawnBullets(int numOfBullets = 1)
    {
        for(int i = 0; i < numOfBullets; i++)
        {
            Bullet _bullet = Instantiate(bulletPrefab, muzzleTransform.position, transform.rotation);
        }
    }
	// Update is called once per frame
	void Update ()
    {
        Timer += Time.deltaTime;
        RotationLoop();
		if (EventSystem.current.IsPointerOverGameObject(-1))    // is the touch on the GUI
		{
			return;
		}
		else if (Input.GetMouseButton(0))
        {
			if(bombState == BombState.DRAGGING)
			{
				bombIndicator.gameObject.SetActive(true);
				bombIndicator.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 10;
			}
			else
			{
				if (Timer >= fireRate)
				{
					SpawnBullets();
					Timer = 0;
				}
			}
        }
		else if(Input.GetMouseButtonUp(0))
		{
			if (bombState == BombState.DRAGGING)
			{
				bombIndicator.gameObject.SetActive(false);
				Instantiate(bombPrefab, bombIndicator.transform.position, Quaternion.identity);
				bombButton.ResetCooldown();
				bombState = BombState.IDLE;
			}
		}
	}
}
