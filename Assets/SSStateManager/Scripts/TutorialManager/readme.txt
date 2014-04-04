SS-Tutorial-Manager 1.0

- How to play Demo:
1. Add "SceneA" & "SceneB" to Build Setting.
2. Play "SceneA".

- How to use:
1. Create a game object "Tutorial Manager" in BaseScene, and drag SSTutorialManager.cs to it.
2. Make your own "GuideArrow" prefab then drag it to SSTutorialManager of "Tutorial Manager".
3. Make some state scripts which inherit WaitState
4. Set up your tutorial:

	// Example:
	private void SetupTutorial()
	{
		StateDict stateDict = new StateDict ();

		SSStateData[] chapter1 = new SSStateData[] 
		{
			new SSStateData(typeof(WaitA)),
			new SSStateData(typeof(WaitB)),
			new SSStateData(typeof(WaitC)),
			new SSStateData(typeof(WaitD)),
			new SSStateData(typeof(WaitE)),
		};

		stateDict.Add ("chapter1", chapter1);

		SSTutorialManager.Instance.SetStateData (stateDict);
	}

5. Start tutorial:

	// Example:
	SSTutorialManager.Instance.StartState("chapter1");