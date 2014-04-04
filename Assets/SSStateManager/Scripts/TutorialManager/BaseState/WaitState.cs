using UnityEngine;
using System.Collections;

public class WaitState : SSTutorialState 
{
	protected GameObject m_CurObject;

	protected override IEnumerator IERun()
	{
		m_CurObject = GameObject.Find (ObjectName());

		if (m_CurObject != null)
		{
			SSTutorialManager.Instance.ShowArrow (m_CurObject.transform);

			SetAllPanelsAlpha (0.5f);
			m_CurObject.AddComponent<UIIndiePanel> ();
		}

		yield return 0;
	}

	protected override void NextState()
	{
		SetAllPanelsAlpha (1f);
		base.NextState ();
	}

	private void OnDestroy()
	{
		if (m_CurObject != null)
		{
			UIIndiePanel panel = m_CurObject.GetComponent<UIIndiePanel> ();
			if (panel != null)
			{
				Destroy (panel);
			}

			Rigidbody girid = m_CurObject.GetComponent<Rigidbody> ();
			if (girid != null)
			{
				Destroy (girid);
			}
		}
	}

	public override void OnClick(GameObject go)
	{
		if (IsOk(go))
		{
			go.SendMessage ("OnClick", SendMessageOptions.DontRequireReceiver);
			OnClick ();
		}
	}

	public override void OnTouch(GameObject go)
	{
		if (IsOk(go))
		{
			go.SendMessage ("OnTouch", SendMessageOptions.DontRequireReceiver);
			OnTouch ();
		}
	}

	public override void OnPress(GameObject go, bool isPressed)
	{
		if (IsOk(go))
		{
			go.SendMessage ("OnPress", isPressed, SendMessageOptions.DontRequireReceiver);
			OnPress (isPressed);
		}
	}

	protected virtual void OnClick()
	{
	}

	protected virtual void OnTouch()
	{
	}

	protected virtual void OnPress(bool isPressed)
	{
	}

	protected virtual bool IsOk(GameObject go)
	{
		return go.name.StartsWith (ObjectName());
	}

	protected virtual string ObjectName()
	{
		return string.Empty;
	}
}
