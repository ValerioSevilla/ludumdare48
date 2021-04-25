using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassesItemController : MonoBehaviour
{
	private static readonly int DISAPPEAR_TRIGGER = Animator.StringToHash("Disappear");
	
	[SerializeField] private GameObject nextLevelObject;

	public Sprite GetSprite()
	{
		return transform.Find("Transformer/Sprite").GetComponent<SpriteRenderer>().sprite;
	}

	public void Pick()
	{
		GetComponent<Collider2D>().enabled = false;
		GetComponent<Animator>().SetTrigger(DISAPPEAR_TRIGGER);

		if(nextLevelObject != null)
			nextLevelObject.SetActive(true);
	}

	public void DestroyItem()
	{
		Destroy(gameObject);
	}
}
