// This code is generated for the purposes of BurgasConf 2021.

using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Prism.Mvvm;
using System.Net.Http;
using BurgasConfSdk.Models;
using Newtonsoft.Json;

namespace BurgasConfSdk.Services
{
	public interface IWeatherForecastService
	{
		Task<ObservableCollection<WeatherForecastBindableModel>> GetAsync();
	}

	public class WeatherForecastService : IWeatherForecastService
	{
		private readonly IConfiguration configuration;

		public WeatherForecastService(IConfiguration configuration)
		{
			this.configuration = configuration;
		}

		public async Task<ObservableCollection<WeatherForecastBindableModel>> GetAsync()
		{
			try
			{
				using (var httpClient = new HttpClient { BaseAddress = this.configuration.BaseUrl })
				{
					HttpResponseMessage response = await httpClient.GetAsync("/weather-forecast/get");

					string responseString = await response.Content.ReadAsStringAsync();
					var result = JsonConvert.DeserializeObject<ObservableCollection<WeatherForecastBindableModel>>(responseString);

					return result;
				}
			}
			catch (Exception)
			{
				return default;
			}
		}
	}
}