import useAxiosQuery from '../useAxios/useAxiosQuery';

const useGetOrders = () => {

    return useAxiosQuery(`/users/orders`);
}

export default useGetOrders;