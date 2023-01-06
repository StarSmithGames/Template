using System.Collections.Generic;

namespace Game.Systems.AnalyticsSystem
{
	public class AnalyticsSystem
	{
		private AmplitudeAnalyticsGroup amplitudeAnalyticsGroup;
		private FirebaseAnalyticsGroup firebaseAnalyticsGroup;
		private UnityAnalyticsGroup unityAnalyticsGroup;

		public AnalyticsSystem(
			AmplitudeAnalyticsGroup amplitudeAnalyticsGroup,
			FirebaseAnalyticsGroup firebaseAnalyticsGroup,
			UnityAnalyticsGroup unityAnalyticsGroup)
		{
			this.amplitudeAnalyticsGroup = amplitudeAnalyticsGroup;
			this.firebaseAnalyticsGroup = firebaseAnalyticsGroup;
			this.unityAnalyticsGroup = unityAnalyticsGroup;
		}

		public void LogEvent(string id, Dictionary<string, object> parameters = null)
		{
			//id = saveLoad.GetStorage().IsPayUser.GetData() ? $"WHALE_{id}" : id;

			if (parameters == null)
			{
				amplitudeAnalyticsGroup.LogEvent(id);
				firebaseAnalyticsGroup.LogEvent(id);
				unityAnalyticsGroup.LogEvent(id);
			}
			else
			{
				amplitudeAnalyticsGroup.LogEvent(id, parameters);
				firebaseAnalyticsGroup.LogEvent(id, parameters);
				unityAnalyticsGroup.LogEvent(id, parameters);
			}
		}
	}
}