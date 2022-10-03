import useAxiosCommand from "../useAxios/useAxiosCommand";

const useLogout = () => {

    return useAxiosCommand({
        method: 'post',
        url: `/accounts/logout`
    });
}

export default useLogout;