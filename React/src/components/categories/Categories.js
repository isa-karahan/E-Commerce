import { Dropdown } from 'semantic-ui-react';
import { Link } from 'react-router-dom';
import useGetCategories from '../../hooks/productService/useGetCategories';
import { useEffect, useState } from 'react';

export default function Categories() {

    const [response] = useGetCategories();

    const [options, setOptions] = useState([]);

    useEffect(() => {

        setOptions(response.map(category => {
            return {
                key: category.id,
                text: category.name,
                as: Link,
                to: `/categories/${category.id}`
            }
        }))

    }, [response])

    return (
        <div>
            <Dropdown text='Categories' options={options} simple item />
        </div>
    )
}
