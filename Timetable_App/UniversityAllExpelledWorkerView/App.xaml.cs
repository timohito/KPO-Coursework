using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Unity;
using Unity.Lifetime;
using TimetableBusinessLogic.BusinessLogics;
using TimetableBusinessLogic.Interfaces;
using TimetableDatabaseImplement.Implements;

namespace UniversityAllExpelledWorkerView
{
	/// <summary>
	/// Логика взаимодействия для App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			IUnityContainer currentContainer = BuildUnityContainer();

			var enterWindow = currentContainer.Resolve<EnterWindow>();
			enterWindow.Show();
		}

		private static IUnityContainer BuildUnityContainer()
		{
			var currentContainer = new UnityContainer();
			currentContainer.RegisterType<IGroupStorage, GroupStorage>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<ITimetableStorage, TimetableStorage>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<IDenearyStorage, DenearyStorage>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<ILectorStorage, LectorStorage>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<ISubjectStorage, SubjectStorage>(new HierarchicalLifetimeManager());	
			//currentContainer.RegisterType<IReportStorage, ReportStorage>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<GroupLogic>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<TimetableLogic>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<DenearyLogic>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<LectorLogic>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<SubjectLogic>(new HierarchicalLifetimeManager());

			return currentContainer;
		}
	}
}
