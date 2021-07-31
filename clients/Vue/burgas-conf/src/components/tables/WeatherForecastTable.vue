<template>
	<div>
		<!-- This code is generated for the purposes of BurgasConf 2021. -->
		<b-table striped hover :items="items" :fields="tableFields"></b-table>
	</div>
</template>

<script>
// This code is generated for the purposes of BurgasConf 2021.
	export default {
		data() {
			return {
				fields: [
					{ key: 'date', label: 'Date', formatter: this.formatters.date },
					{ key: 'temperatureC', label: 'TemperatureC', formatter: this.formatters.temperatureC },
					{ key: 'temperatureF', label: 'TemperatureF', formatter: this.formatters.temperatureF },
					{ key: 'summary', label: 'Summary', formatter: this.formatters.summary },
				]
			}
		},
		props: {
			excludedFields: {
				type: Array,
				default() {
					return [];
				}
			},
			items: {
				type: Array,
				required: true
			},
			formatters: {
				type: Object,
				default() {
					return {}
				}
			}
		},
		computed: {
			tableFields() {
				return this.fields.filter(x => !this.excludedFields.includes(x.key));
			}
		}
	}
</script>


