#if UNITY_FLASH || UNITY_WP8 || UNITY_METRO
#define USE_SIMPLE_DICTIONARY
#endif

using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// UI Indie Panel inherit from UI Panel. Its final alpha is independent to its parent.
/// </summary>

[ExecuteInEditMode]
public class UIIndiePanel : UIPanel
{
	protected override void OnStart()
	{
		base.OnStart ();
		ChangeParent ();
		SetMaxDepth ();
	}

	private void ChangeParent()
	{
		UIWidget[] widgets = GetComponentsInChildren<UIWidget> ();
		foreach (var widget in widgets)
		{
			widget.ParentHasChanged ();
		}
	}

	private void SetMaxDepth()
	{
		depth = 99;
	}

	public override float CalculateFinalAlpha (int frameID)
	{
		finalAlpha = alpha;
		return finalAlpha;
	}
}
