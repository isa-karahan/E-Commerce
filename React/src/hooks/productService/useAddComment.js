import useAxiosCommand from "../useAxios/useAxiosCommand";

const useAddComment = () => {

    return useAxiosCommand({
        method: 'post',
        url: '/products/comment'
    });
}

export default useAddComment;