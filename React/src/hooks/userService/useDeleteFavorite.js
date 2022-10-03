import useAxiosCommand from "../useAxios/useAxiosCommand";

const useDeleteFavorite = () => {

    return useAxiosCommand({
        method: 'delete',
        url: `/users/favorites`
    });
}

export default useDeleteFavorite;