using UnityEngine;
using System.Collections;

public enum ArrowDirection
{
	UP,
	DOWN
}
	
public class GuideArrow : MonoBehaviour 
{
	[SerializeField]
	private Animation anim;
	
	[SerializeField]
	private UISprite sprite;

	public void SetArrow(Transform parent, Vector3 pos, ArrowDirection direction = ArrowDirection.DOWN)
	{
		transform.parent = parent;
		transform.localScale = Vector3.one;
		transform.localPosition = pos;

		switch (direction) 
		{
			case ArrowDirection.DOWN:
				sprite.transform.localEulerAngles = Vector3.zero;
				anim.Play("ArrowDown");
				break;
			case ArrowDirection.UP:
				sprite.transform.localEulerAngles = new Vector3(180, 0, 0);
				anim.Play("ArrowUp");
				break;
		}

		sprite.ParentHasChanged ();
	}
	
	public void SetArrow(Transform parent, ArrowDirection direction = ArrowDirection.DOWN)
	{
		UIWidget widget = parent.gameObject.GetComponentInChildren<UIWidget> ();

		transform.parent = parent;
		transform.localScale = Vector3.one;
		transform.localPosition = Vector3.zero;

		if (widget != null)
		{
			switch (direction) 
			{
				case ArrowDirection.DOWN:
					sprite.transform.localEulerAngles = Vector3.zero;
					transform.localPosition += new Vector3(0f, (widget.height / 2 + sprite.height / 2) ,0f);
					anim.Play("ArrowDown");
					break;
				case ArrowDirection.UP:
					sprite.transform.localEulerAngles = new Vector3(180, 0, 0);
					transform.localPosition += new Vector3(0f, - (widget.height / 2 + sprite.height / 2) ,0f);
					anim.Play("ArrowUp");
					break;
			}
		}

		sprite.ParentHasChanged ();
	}
}
