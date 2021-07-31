<template>
  <div id="app">
    <b-container>
      <weather-forecast-table :items="weatherItems" :formatters="formatters" />
    </b-container>
  </div>
</template>

<script>
import WeatherForecastTable from './components/tables/WeatherForecastTable.vue';
import { get } from './services/WeatherForecastService';
export default {
  name: 'App',
  components: {
    WeatherForecastTable
  },
  data() {
    return {
      weatherItems: [],
      formatters: {
        date: (value) => (new Date(value)).toLocaleString(),
        temperatureC: (value) => `${value} °C`,
        temperatureF: (value) => `${value} °F`
      }
    }
  },
  mounted() {
    get()
      .then(response => {
        this.weatherItems = response.data;
      });
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
