import axios from "axios";
import history from '../components/history/history';

const apiClient = axios.create({
	headers: {
		"Content-Type": "application/json",
	},
	withCredentials: true
});

apiClient.interceptors.request.use(
	request => {
		return request
	},
	error => {
		return error.response;
	}
)

apiClient.interceptors.response.use(
	response => {
		return response;
	},
	async error => {
		const originalRequest = error.config;

		if (error.response.status === 401 && !originalRequest._retry) {
			originalRequest._retry = true;

			try {
				const tempAxios = axios.create({ withCredentials: true });

				const res = await tempAxios.post(`${originalRequest.baseURL}/accounts/token`);

				if (res.status === 200) {
					return await apiClient(originalRequest);;
				}
			}
			catch (e) {
				localStorage.clear();
				history.push('/login');
			}
		}
		return error.response;
	}
)

export default apiClient;