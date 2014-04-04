using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using StateDict = System.Collections.Generic.Dictionary<string, SSStateData[]>;

public class SSStateManager<T> : MonoBehaviour where T : SSState
{
	public delegate void OnFinishAllStatesDelegate (string stateGroupName);
	public OnFinishAllStatesDelegate onFinishAllStates;

	protected StateDict 	m_States;
	protected int 			m_StateDataPointer;
	protected string 		m_StateGroupName;
	protected SSStateData	m_StateWait;

	public T CurrentState { get; protected set; }

	protected static SSStateManager<T> m_Instance;
	public static SSStateManager<T> Instance
	{
		get { return m_Instance; }
	}

	protected virtual void Awake()
	{
		m_Instance = this;
		DontDestroyOnLoad (gameObject);
	}

	private void Start()
	{
		Reset ();
	}

	public void Reset()
	{
		m_StateGroupName = string.Empty;
		m_StateDataPointer = 0;
	}

	public void SetStateData(StateDict states)
	{
		m_States = states;

		Reset ();
	}

	public virtual void NextState()
	{
		if (m_States == null)
		{
			return;
		}

		if (!m_States.ContainsKey (m_StateGroupName))
		{
			return;
		}

		// Destroy current state
		DestroyCurrentState ();

		// Next data
		if (m_StateDataPointer >= m_States[m_StateGroupName].Length) 
		{
			string curState = m_StateGroupName;

			Reset ();

			OnFinishedAllState (curState);

			if (onFinishAllStates != null)
			{
				onFinishAllStates (curState);
			}
			return;
		}

		// Get data
		SSStateData stateData = m_States [m_StateGroupName][m_StateDataPointer];

		// Next pointer
		m_StateDataPointer++;

		// New state
		CreateState (stateData);
	}

	protected virtual void OnFinishedAllState(string stateGroupName)
	{
	}

	public virtual void StartState(string stateGroupName)
	{
		if (!m_States.ContainsKey (stateGroupName)) 
		{
			return;
		}

		Reset ();

		m_StateGroupName = stateGroupName;

		NextState ();
	}

	private void DestroyCurrentState()
	{
		if (CurrentState != null) 
		{
			CurrentState.onNextState -= NextState;
			Object.Destroy (CurrentState);

			CurrentState = null;
		}
	}

	private void CreateState(SSStateData stateData)
	{
		CurrentState = m_Instance.gameObject.AddComponent (stateData.Type) as T;
		CurrentState.onNextState += NextState;
		CurrentState.Set (m_Instance.gameObject, stateData.Data);
		CurrentState.Run ();
	}

	private SSStateData GetNextStateData()
	{
		if (!m_States.ContainsKey (m_StateGroupName))
		{
			return null;
		}

		if (m_StateDataPointer > m_States[m_StateGroupName].Length - 1) 
		{
			return null;
		}

		SSStateData nextStateData = m_States [m_StateGroupName][m_StateDataPointer];
		return nextStateData;
	}
}
