import useAxiosCommand from "../useAxios/useAxiosCommand";

const useAddFavorite = () => {

    return useAxiosCommand({
        method: 'post',
        url: `/users/favorites`
    });
}

export default useAddFavorite;