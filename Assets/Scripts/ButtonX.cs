using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ButtonX : Button
{
	public float cooldown;
	public Image cooldownClock;
	public override void OnPointerExit(PointerEventData eventData)
	{
		if (Input.GetMouseButton(0) && interactable)
		{
			OnDragOff();
		}
		base.OnPointerExit(eventData);
	}
	public void ResetCooldown()
	{
		cooldown = 10;
		cooldownClock.fillAmount = 1;
		interactable = false;
	}
	private void Update()
	{
		cooldownClock.fillAmount = cooldown/10;
		if(cooldown > 0)
		{
			cooldown -= Time.deltaTime;
		}
		else
		{
			cooldown = 0;
			interactable = true;
		}
	}
	public void OnDragOff()
	{
		
		GameManager.instance.player.bombState = Player.BombState.DRAGGING;
	}
}