using UnityEngine;
using System.Collections;

public class SceneB : MonoBehaviour 
{
	[SerializeField]
	UILabel m_Label;

	private void Start()
	{
		SSTutorialManager.Instance.onFinishAllStates += OnFinishAllStates;

		SSTutorialManager.Instance.NextState ();
	}

	private void OnDestroy()
	{
		SSTutorialManager.Instance.onFinishAllStates -= OnFinishAllStates;
	}

	private void OnFinishAllStates(string groupStateName)
	{
		m_Label.text = "Finished!";
	}

	public void OnETap()
	{
		m_Label.text = "E Tap";
	}
}
