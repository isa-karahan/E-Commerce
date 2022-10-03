import useAxiosCommand from '../useAxios/useAxiosCommand';

const useUpdateProfile = () => {

    return useAxiosCommand({
        method: 'put',
        url: `/users`
    });
}

export default useUpdateProfile;