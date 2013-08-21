
// <author>George Kozlov (george.kozlov@outlook.com)</author>
// <date>07/05/2013</date>
// <summary>ActivityBase class</summary>

using System;
using System.Runtime.Serialization;

using Gnip.Data.Common;

namespace Gnip.Data
{
	[DataContract(Name = "activity")]
	public abstract class ActivityBase
	{
		[DataMember(Name = "id")]
		public string Id
		{
			get;
			set;
		}

		[DataMember(Name = "objectType")]
		public string ObjectType
		{
			get;
			set;
		}

		[DataMember(Name = "verb")]
		public string Verb
		{
			get;
			set;
		}

		[DataMember(Name = "postedTime")]
		private string _postedTimeString;

		[CSVMember(Name = "Activity posted", ValuePath = "PostedTime", Default = true)]
		public DateTime PostedTime
		{
			get
			{
				DateTime time = DateTime.Now;
				DateTime.TryParse(_postedTimeString, out time);
				return time;
			}
		}

		[CSVMember(Name = "Data source")]
		[DataMember(Name = "generator")]
		public Generator Generator
		{
			get;
			set;
		}

		[CSVMember(Name = "Data provider")]
		[DataMember(Name = "provider")]
		public Provider Provider
		{
			get;
			set;
		}

		[CSVMember(Name = "GNIP extensions")]
		[DataMember(Name = "gnip")]
		public GnipExtensions Gnip
		{
			get;
			set;
		}
	}
}
