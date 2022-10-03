import useAxiosCommand from '../useAxios/useAxiosCommand';
const useGetOrders = () => {

    return useAxiosCommand({
        method: 'post',
        url: `/users/orders`
    });
}

export default useGetOrders;