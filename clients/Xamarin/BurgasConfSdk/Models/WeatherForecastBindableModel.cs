// This code is generated for the purposes of BurgasConf 2021.

using System;
using System.Collections.ObjectModel;
using Prism.Mvvm;

namespace BurgasConfSdk.Models
{
	public class WeatherForecastBindableModel : BindableBase
	{
		private DateTime date;
		private Int32 temperatureC;
		private Int32 temperatureF;
		private String summary;

		public DateTime Date
		{
			get
			{
				return this.date;
			}
			set
			{
				SetProperty(ref this.date, value);
			}
		}

		public Int32 TemperatureC
		{
			get
			{
				return this.temperatureC;
			}
			set
			{
				SetProperty(ref this.temperatureC, value);
			}
		}

		public Int32 TemperatureF
		{
			get
			{
				return this.temperatureF;
			}
			set
			{
				SetProperty(ref this.temperatureF, value);
			}
		}

		public String Summary
		{
			get
			{
				return this.summary;
			}
			set
			{
				SetProperty(ref this.summary, value);
			}
		}
	}
}