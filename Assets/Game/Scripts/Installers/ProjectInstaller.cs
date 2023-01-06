using Game.Managers.AsyncManager;
using Game.Systems.ApplicationHandler;

using Zenject;

namespace Game.Installers
{
	public class ProjectInstaller : MonoInstaller<ProjectInstaller>
	{
		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelfTo<AsyncManager>().AsSingle();

			SignalBusInstaller.Install(Container);
			ApplicationHandlerInstaller.Install(Container);
		}
	}
}