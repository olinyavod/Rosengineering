using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Rosengineering.DAL.Models;
using Rosengineering.Desktop.ViewModels;

namespace Rosengineering.Desktop.IoCModules
{
	public class Mapping:Profile
	{
		public Mapping()
		{
			CreateMap<UserEditorViewModel, User>()
				.ReverseMap();

			CreateMap<UserGroupEditorViewModel, UserGroup>()
				.ReverseMap();
		}
	}
}
