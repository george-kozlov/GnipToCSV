//
// Gnip.Ruler, Gnip.Streamer
// Copyright (C) 2013 George Kozlov
// These programs are free software: you can redistribute them and/or modify them under the terms of the GNU General Public License as published by the Free Software Foundation. either version 3 of the License, or any later version.
// These programs are distributed in the hope that they will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
// See the GNU General Public License for more details. You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/.
// For further questions or inquiries, please contact semantapi (at) gmail (dot) com
//

using System;

namespace Gnip.Data.Common
{
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
	public class CSVContractAttribute : Attribute
	{
		public CSVContractAttribute()
		{
		}

		public string Name
		{
			get;
			set;
		}

		public string Description
		{
			get;
			set;
		}
	}

	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
	public class CSVMemberAttribute : Attribute
	{
		public CSVMemberAttribute()
		{
			Default = false;
		}

		public string Name
		{
			get;
			set;
		}

		public string Description
		{
			get;
			set;
		}

		public string ValuePath
		{
			get;
			set;
		}

		public bool Default
		{
			get;
			set;
		}
	}
}
