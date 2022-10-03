import useAxiosQuery from '../useAxios/useAxiosQuery';

const useGetCategories = () => {

    return useAxiosQuery('/products/categories');
}

export default useGetCategories;