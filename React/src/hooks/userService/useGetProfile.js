import useAxiosQuery from '../useAxios/useAxiosQuery';

const useGetProfile = () => {

    return useAxiosQuery(`/users`);
}

export default useGetProfile;