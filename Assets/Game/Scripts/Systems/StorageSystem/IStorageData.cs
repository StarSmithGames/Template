using UnityEngine.Assertions;
using UnityEngine.Events;

namespace Game.Managers.StorageManager
{
	public interface IStorageData<T>
	{
		event UnityAction onChanged;

		T GetData();
		void SetData(T data);
	}

	public class StorageData<T> : IStorageData<T>
	{
		public event UnityAction onChanged;

		private Database database;
		private string key;
		private T defaultValue;

		public StorageData(Database database, string key, T defaultValue = default)
		{
			this.database = database;
			this.key = key;
			this.defaultValue = defaultValue;

			SetData(database.Get(GetDataKey(), defaultValue));
		}

		public string GetDataKey()
		{
			return key + "_key";
		}
		public T GetData()
		{
			return database.Get<T>(GetDataKey(), defaultValue);
		}
		public void SetData(T data)
		{
			database.Set(GetDataKey(), data);

			onChanged?.Invoke();
		}
	}

	public class Storage
	{
		public Database Database { get; private set; }

		public Profile Profile { get; private set; }

		public IStorageData<bool> IsWasHere{ get; private set; }
		public IStorageData<bool> IsFirstTime { get; private set; }


		/// <summary>
		/// Default Data
		/// </summary>
		public Storage()
		{
			Profile = new Profile();
			Database = new Database();

			Initialization();
		}

		/// <summary>
		/// Json Data
		/// </summary>
		public Storage(string profile, string data)
		{
			Profile = new Profile(profile);
			Database = new Database();
			Database.LoadJson(data);

			Initialization();
		}

		public void Clear()
		{
			Database.Drop();
			Initialization();
		}

		private void Initialization()
		{
			IsWasHere = new StorageData<bool>(Database, "is_was_here", false);
			IsFirstTime = new StorageData<bool>(Database, "is_first_time", true);
		}

		[System.Serializable]
		public class Reference
		{
			public string displayName;
			public string fileName;
		}
	}

	public class Profile
	{
		public Data data;

		public Profile()
		{

		}

		public Profile(string json)
		{
			data = JsonSerializator.ConvertFromUnityJson<Data>(json);
		}

		public string GetJson()
		{
			return JsonSerializator.ConvertToUnityJson(data);
		}

		public Data GetData()
		{
			if(data == null)
			{
				data = new Data();
			}

			return data;
		}

		[System.Serializable]
		public class Data
		{
	
		}
	}
}