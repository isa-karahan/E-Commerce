import useAxiosQuery from '../useAxios/useAxiosQuery';

const useGetFavorites = () => {

    return useAxiosQuery(`/users/favorites`);
}

export default useGetFavorites;