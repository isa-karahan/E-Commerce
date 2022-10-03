import useAxiosCommand from "../useAxios/useAxiosCommand";

const useDeleteAddress = () => {

    return useAxiosCommand({
        method: 'delete',
        url: `/users/address`
    });
}

export default useDeleteAddress;