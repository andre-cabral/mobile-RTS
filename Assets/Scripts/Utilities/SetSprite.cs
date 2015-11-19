using UnityEngine;
using System.Collections;

public class SetSprite : MonoBehaviour {
	public Sprite spriteToUse;
	SpriteRenderer spriteRenderer;

	public void Awake(){
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	public void SetTheSprite(){
		spriteRenderer.sprite = spriteToUse;
	}
}
