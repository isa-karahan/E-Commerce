import useAxiosCommand from "../useAxios/useAxiosCommand";

const useRegister = () => {

    return useAxiosCommand({
        method: 'post',
        url: `/accounts/register`
    });
}

export default useRegister;