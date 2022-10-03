import useAxiosCommand from '../useAxios/useAxiosCommand';

const useGetOrders = () => {

    return useAxiosCommand({
        method: 'put',
        url: `/users/wallet`
    });
}

export default useGetOrders;