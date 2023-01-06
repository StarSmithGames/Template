using UnityEngine;

using Zenject;

namespace Game.Systems.AnalyticsSystem
{
	[CreateAssetMenu(fileName = "AnalyticsSystemInstaller", menuName = "Installers/AnalyticsSystemInstaller")]
	public class AnalyticsSystemInstaller : ScriptableObjectInstaller<AnalyticsSystemInstaller>
	{
		public AnalysticsSettigns settigns;

		public override void InstallBindings()
		{
			Container.BindInstance(settigns.amplitudeApiKey).WhenInjectedInto<AmplitudeAnalyticsGroup>();
			Container.BindInterfacesAndSelfTo<AmplitudeAnalyticsGroup>().AsSingle().NonLazy();
			Container.BindInterfacesAndSelfTo<FirebaseAnalyticsGroup>().AsSingle().NonLazy();
			Container.BindInterfacesAndSelfTo<UnityAnalyticsGroup>().AsSingle().NonLazy();
			Container.BindInterfacesAndSelfTo<AnalyticsSystem>().AsSingle().NonLazy();
		}
	}

	[System.Serializable]
	public class AnalysticsSettigns
	{
		public string amplitudeApiKey;
	}
}