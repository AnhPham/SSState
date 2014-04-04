using UnityEngine;
using System.Collections;

using StateDict = System.Collections.Generic.Dictionary<string, SSStateData[]>;

public class SceneA : MonoBehaviour 
{
	[SerializeField]
	UILabel m_Label;

	private void Start()
	{
		StateDict stateDict = new StateDict ();

		SSStateData[] chapter1 = new SSStateData[] 
		{
			Wait(typeof(WaitA)),
			Wait(typeof(WaitB)),
			Wait(typeof(WaitC)),
			Wait(typeof(WaitD)),
			Wait(typeof(WaitE)),
		};

		stateDict.Add ("chapter1", chapter1);

		SSTutorialManager.Instance.SetStateData (stateDict);
	}

	private SSStateData Wait(System.Type type)
	{
		return new SSStateData (type);
	}

	private void OnGUI()
	{
		GUILayout.BeginHorizontal ();

		if (GUILayout.Button ("Start Tut")) 
		{
			SSTutorialManager.Instance.StartState ("chapter1");
		}

		GUILayout.EndHorizontal ();
	}

	public void OnATap()
	{
		m_Label.text = "A Tap";
	}

	public void OnBTap()
	{
		m_Label.text = "B Tap";
	}

	public void OnCTap()
	{
		m_Label.text = "C Tap";
	}

	public void OnDTap()
	{
		Application.LoadLevel ("SceneB");
	}
}
