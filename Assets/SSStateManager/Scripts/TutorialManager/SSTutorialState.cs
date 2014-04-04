using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SSTutorialState : SSState 
{
	static List<UIPanel> m_UIPanelList = new List<UIPanel>();

	public virtual void OnClick(GameObject go)
	{
	}

	public virtual void OnTouch(GameObject go)
	{
	}

	public virtual void OnPress(GameObject go, bool isPressed)
	{
	}

	protected void SetAllPanelsAlpha(float alpha)
	{
		if (alpha == 1)
		{
			SetAllPanelsAlpha1 ();
			return;
		}

		foreach (UIPanel panel in UIPanel.list) 
		{
			if (!m_UIPanelList.Contains (panel))
			{
				m_UIPanelList.Add (panel);
			}
			panel.alpha = alpha;
		}	
	}

	protected void SetAllPanelsAlpha1()
	{
		foreach (UIPanel panel in m_UIPanelList)
		{
			panel.alpha = 1;
		}

		m_UIPanelList.Clear ();
	}
}
