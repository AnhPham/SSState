using UnityEngine;
using System.Collections;

public class SSInputIgnoreMask : MonoBehaviour 
{
	struct DepthEntry
	{
		public int depth;
		public RaycastHit hit;
	}

	public delegate void OnActionDelegate(GameObject go);
	public delegate void OnPressDelegate(GameObject go, bool isPressed);
	public static OnActionDelegate onClick;
	public static OnActionDelegate onTouch;
	public static OnPressDelegate onPress;

	private static Vector3 		m_LastTouch;
	private static RaycastHit 	m_Hit;
	private static bool			m_Pressed;

	private static DepthEntry mHit = new DepthEntry();
	private static BetterList<DepthEntry> mHits = new BetterList<DepthEntry>();

	private RaycastHit m_HitEmpty = new RaycastHit();

	public static bool IsRun { get; protected set; }

	private void Awake()
	{
		IsRun = true;
	}

	private void Update() 
	{		
		if (!IsRun)
			return;

		if ( Input.GetMouseButton(0) ) 
		{
			Press ();
		}
		else if(Input.GetMouseButtonUp(0))
		{
			Release ();
		}
	}

	private void Press()
	{
		if (Raycast(Input.mousePosition, out m_Hit))
		{
			Vector3 delta = Vector3.zero;
			if (m_LastTouch != Vector3.zero) 
			{
				delta = Input.mousePosition - m_LastTouch;
			}

			GameObject go = m_Hit.collider.gameObject;
			OnTouch(go, delta);

			if (!m_Pressed)
			{
				OnPress (go, true);
			}
		}

		m_Pressed = true;
		m_LastTouch = Input.mousePosition;
	}

	private void Release()
	{
		if (m_Pressed)
		{
			RaycastHit hit;
			if (Raycast (Input.mousePosition, out hit))
			{
				GameObject go = hit.collider.gameObject;
				if (m_Hit.collider != null && m_Hit.collider.gameObject == go)
				{
					OnPress (go, false);
					OnClick (go);
				}
			}
		}

		m_Pressed = false;
		m_LastTouch = Vector3.zero;
	}

	private bool Raycast(Vector3 inPos, out RaycastHit hit)
	{
		for (int i = 0; i < UICamera.list.size; ++i)
		{
			UICamera cam = UICamera.list.buffer[i];

			// Skip inactive scripts
			if (!cam.enabled || !NGUITools.GetActive(cam.gameObject)) continue;

			// Convert to view space
			Camera currentCamera = cam.cachedCamera;
			Vector3 pos = currentCamera.ScreenToViewportPoint(inPos);
			if (float.IsNaN(pos.x) || float.IsNaN(pos.y)) continue;

			// If it's outside the camera's viewport, do nothing
			if (pos.x < 0f || pos.x > 1f || pos.y < 0f || pos.y > 1f) continue;

			// Cast a ray into the screen
			Ray ray = currentCamera.ScreenPointToRay(inPos);

			// Raycast into the screen
			RaycastHit[] hits = Physics.RaycastAll(ray);

			if (hits.Length > 1)
			{
				for (int b = 0; b < hits.Length; ++b)
				{
					GameObject go = hits[b].collider.gameObject;
					mHit.depth = NGUITools.CalculateRaycastDepth(go);

					if (mHit.depth != int.MaxValue)
					{
						mHit.hit = hits[b];
						mHits.Add(mHit);
					}
				}

				mHits.Sort(delegate(DepthEntry r1, DepthEntry r2) { return r2.depth.CompareTo(r1.depth); });

				for (int b = 0; b < mHits.size; ++b)
				{
					#if UNITY_FLASH
					if (IsVisible(mHits.buffer[b]))
					#else
					if (IsVisible(ref mHits.buffer[b]))
					#endif
					{
						hit = mHits[b].hit;
						mHits.Clear();
						return true;
					}
				}
				mHits.Clear();
			}
			else if (hits.Length == 1 && IsVisible(ref hits[0]))
			{
				hit = hits[0];
				return true;
			}
			continue;
		}

		hit = m_HitEmpty;
		return false;
	}

	static bool IsVisible (ref RaycastHit hit)
	{
		UIPanel panel = NGUITools.FindInParents<UIPanel>(hit.collider.gameObject);

		if (panel == null || panel.IsVisible(hit.point))
		{
			return true;
		}
		return false;
	}

	#if UNITY_FLASH
	static bool IsVisible (DepthEntry de)
	#else
	static bool IsVisible (ref DepthEntry de)
	#endif
	{
		UIPanel panel = NGUITools.FindInParents<UIPanel>(de.hit.collider.gameObject);
		return (panel == null || panel.IsVisible(de.hit.point));
	}

	private static void OnTouch(GameObject go, Vector3 delta)
	{
		if (go != null) 
		{
			if (onTouch != null)
			{
				onTouch (go);
			}
		}
	}

	private static void OnClick(GameObject go)
	{
		if (go != null) 
		{
			if (onClick != null)
			{
				onClick (go);
			}
		}
	}

	private static void OnPress(GameObject go, bool isPressed)
	{
		if (go != null) 
		{
			if (onPress != null)
			{
				onPress (go, isPressed);
			}
		}
	}
}
