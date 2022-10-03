import useAxiosCommand from "../useAxios/useAxiosCommand";

const useLogin = () => {

    return useAxiosCommand({
        method: 'post',
        url: `/accounts/login`
    });
}

export default useLogin;