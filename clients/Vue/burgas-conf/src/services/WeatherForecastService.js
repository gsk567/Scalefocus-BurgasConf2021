// This code is generated for the purposes of BurgasConf 2021.

import axios from 'axios';
import { apiBaseUrl } from '../config';

export function get() {
	return axios.get(`${apiBaseUrl}/weather-forecast/get`)
}
