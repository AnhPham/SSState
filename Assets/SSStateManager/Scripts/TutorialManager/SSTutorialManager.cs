using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SSInputIgnoreMask))]
public class SSTutorialManager : SSStateManager<SSTutorialState>
{
	[SerializeField]
	GameObject m_ArrowPrefab;

	[SerializeField]
	ArrowDirection m_ArrowDirection = ArrowDirection.DOWN;

	GuideArrow m_Arrow;

	new public static SSTutorialManager Instance
	{
		get { return (SSTutorialManager)m_Instance; }
	}

	protected override void Awake()
	{
		base.Awake ();

		SSInputIgnoreMask.onClick += OnClick;
		SSInputIgnoreMask.onTouch += OnTouch;
		SSInputIgnoreMask.onPress += OnPress;
	}

	private void OnDestroy()
	{
		SSInputIgnoreMask.onClick -= OnClick;
		SSInputIgnoreMask.onTouch -= OnTouch;
		SSInputIgnoreMask.onPress -= OnPress;
	}

	public override void StartState(string stateGroupName)
	{
		base.StartState (stateGroupName);

		if (m_States.ContainsKey (stateGroupName)) 
		{
			SetAllCam (false);
		}
	}

	public override void NextState()
	{
		HideArrow ();
		base.NextState ();
	}

	protected override void OnFinishedAllState(string stateGroupName)
	{
		DestroyArrow ();
	}

	protected void OnClick(GameObject go)
	{
		if (CurrentState != null)
		{
			CurrentState.OnClick (go);
		}
	}

	protected void OnTouch(GameObject go)
	{
		if (CurrentState != null)
		{
			CurrentState.OnTouch (go);
		}
	}

	protected void OnPress(GameObject go, bool isPressed)
	{
		if (CurrentState != null)
		{
			CurrentState.OnPress (go, isPressed);
		}
	}

	public void ShowArrow(Transform parent, Vector3 pos)
	{
		InitArrow ();

		m_Arrow.gameObject.SetActive (true);
		m_Arrow.SetArrow (parent, pos, m_ArrowDirection);
	}

	public void ShowArrow(Transform parent)
	{
		InitArrow ();

		m_Arrow.gameObject.SetActive (true);
		m_Arrow.SetArrow (parent, m_ArrowDirection);
	}

	public void HideArrow()
	{
		if (m_Arrow != null)
		{
			m_Arrow.gameObject.SetActive (false);
		}
	}

	private void InitArrow()
	{
		if (m_Arrow == null)
		{
			GameObject go = Instantiate (m_ArrowPrefab) as GameObject;
			m_Arrow = go.GetComponent<GuideArrow> ();
		}
	}

	private void DestroyArrow()
	{
		if (m_Arrow != null)
		{
			Destroy (m_Arrow.gameObject);
			m_Arrow = null;
		}
	}

	private void SetAllCam(bool isActive)
	{
		foreach (UICamera cam in UICamera.list) 
		{
			if (isActive)
			{
				cam.eventReceiverMask = (1 << LayerMask.NameToLayer ("Default"));
			} 
			else
			{
				cam.eventReceiverMask = 0 << 0;
			}
		}	
	}
}
