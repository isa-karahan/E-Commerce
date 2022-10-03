import useAxiosQuery from '../useAxios/useAxiosQuery';

const useGetProduct = () => {

    return useAxiosQuery(`/users/orders`);
}

export default useGetProduct;