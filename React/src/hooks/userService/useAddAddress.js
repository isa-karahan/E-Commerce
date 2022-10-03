import useAxiosCommand from "../useAxios/useAxiosCommand";

const useAddAddress = () => {

    return useAxiosCommand({
        method: 'post',
        url: `/users/address`
    });
}

export default useAddAddress;