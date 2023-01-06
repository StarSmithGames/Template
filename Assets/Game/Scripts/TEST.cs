using Game.Systems.AnalyticsSystem;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Zenject;

public class TEST : MonoBehaviour
{
	private AnalyticsSystem analyticsSystem;

	[Inject]
	private void Construct(AnalyticsSystem analyticsSystem)
	{
		this.analyticsSystem = analyticsSystem;

		Debug.LogError("AnalyticsSystem " + (analyticsSystem != null));
	}

	private void Start()
	{
		GetComponent<Button>().onClick.AddListener(OnClick);
	}

	private void OnClick()
	{
		analyticsSystem.LogEvent("TEMPLETE CHECK");
	}
}