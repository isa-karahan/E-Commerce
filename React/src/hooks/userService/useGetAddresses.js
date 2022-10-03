import useAxiosQuery from '../useAxios/useAxiosQuery';

const useGetAddresses = () => {

    return useAxiosQuery(`/users/address`);
}

export default useGetAddresses;