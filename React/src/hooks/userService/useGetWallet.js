import useAxiosQuery from '../useAxios/useAxiosQuery';

const useGetWallet = () => {

    return useAxiosQuery('/users/wallet');
}

export default useGetWallet;