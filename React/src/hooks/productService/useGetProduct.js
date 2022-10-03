import { useParams } from 'react-router-dom';
import useAxiosQuery from '../useAxios/useAxiosQuery';

const useGetProduct = () => {

    const { id } = useParams();

    return useAxiosQuery(`/products/${id}`);
}

export default useGetProduct;