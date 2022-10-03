import useAxiosQuery from '../useAxios/useAxiosQuery';
import { useParams } from 'react-router-dom';

const useGetProducts = () => {

    let { categoryId } = useParams();
    let url = categoryId ? `/products/categories/${categoryId}` : '/products';

    return useAxiosQuery(url);
}

export default useGetProducts;