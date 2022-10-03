import useAxiosCommand from '../useAxios/useAxiosCommand';
const useCancelOrder = () => {

    return useAxiosCommand({
        method: 'delete',
        url: `/users/orders`
    });
}

export default useCancelOrder;