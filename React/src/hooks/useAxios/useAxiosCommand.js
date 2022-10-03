import { toast } from 'react-toastify';
import apiClient from '../../apiClient/apiClient';
import { useSelector } from 'react-redux';

const useAxiosCommand = (configObj) => {
    const {
        method,
        url,
    } = configObj;

    const baseURL = useSelector(state => state.server);

    const command = async (data = {}) => {

        try {
            const res = await apiClient.request({
                baseURL: baseURL,
                data: data,
                method,
                url
            });

            const result = res.data;

            if (result.isSuccess) {
                toast.success(result.message);
            }
            else {
                toast.error(result.message);
            }

            return result;
        }
        catch (err) {
            toast.error(err.message);
            return { isSuccess: false };
        }
    }

    return command;
}

export default useAxiosCommand;