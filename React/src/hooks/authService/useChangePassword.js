import useAxiosCommand from "../useAxios/useAxiosCommand";

const useChangePassword = () => {

    return useAxiosCommand({
        method: 'put',
        url: `/accounts`
    });
}

export default useChangePassword;