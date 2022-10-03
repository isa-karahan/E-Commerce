import { useState, useEffect } from "react";
import { toast } from 'react-toastify';
import apiClient from '../../apiClient/apiClient';
import { useSelector } from "react-redux";

const useAxiosQuery = (url) => {

    const baseURL = useSelector(state => state.server);

    const [response, setResponse] = useState([]);
    const [error, setError] = useState('');
    const [loading, setLoading] = useState(true);
    const [reload, setReload] = useState(false);

    const refetch = () => setReload(!reload);

    useEffect(() => {

        const fetchData = async () => {
            try {
                const res = await apiClient.get(url, { baseURL: baseURL });

                const result = res.data;

                if (result.message) {
                    if (result.isSuccess)
                        toast.success(result.message);
                    else
                        toast.error(result.message);
                }

                setResponse(result.data);
            } catch (err) {
                console.log(err.message);
                setError(err.message);
            } finally {
                setLoading(false);
            }
        }

        // call the function
        fetchData();

        // eslint-disable-next-line
    }, [reload, url, baseURL]);

    // return object
    return [response, error, loading, refetch];
}

export default useAxiosQuery;