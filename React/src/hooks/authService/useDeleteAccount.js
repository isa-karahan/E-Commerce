import useAxiosCommand from "../useAxios/useAxiosCommand";

const useDeleteAccount = () => {

    return useAxiosCommand({
        method: 'delete',
        url: `/accounts`
    });
}

export default useDeleteAccount;