using Zenject;

namespace Game.Managers.StorageManager
{
	public class SaveHandler : IInitializable
	{
		private ISaveLoad saveLoad;
		private SignalBus signalBus;

		public SaveHandler(ISaveLoad saveLoad,
			SignalBus signalBus)
		{
			this.saveLoad = saveLoad;
			this.signalBus = signalBus;
		}

		public void Initialize()
		{
			signalBus?.Subscribe<SignalSave>(OnSaveRequired);
		}

		private void OnSaveRequired()
		{
			var profile = saveLoad.GetStorage().Profile;

			//profile.GetData().waveRoadData = waveRoad.GetData();
			//profile.GetData().playerData = player.GetData();

			saveLoad.GetStorage().IsWasHere.SetData(true);

			saveLoad.Save();
		}
	}
}