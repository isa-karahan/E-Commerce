import { Card, Image, Loader } from 'semantic-ui-react'
import { Link } from 'react-router-dom';
import AddToCart from '../cart/AddToCart';
import AddToFavorites from '../favorites/AddToFavorites';
import { useSelector } from 'react-redux';
import useGetProducts from '../../hooks/productService/useGetProducts';

export default function Products() {

    const { user } = useSelector(state => state);

    const [response, error, loading] = useGetProducts();

    return (
        <div>
            <Loader active={loading} size='big' className="mt-5" />

            {
                response && !error && <Card.Group itemsPerRow={5}>
                    {
                        response.map(product =>
                            <Card color='green' key={product.id}>
                                <Card.Content>
                                    <Link to={`/products/${product.id}`}>
                                        <Image
                                            src='https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSRsyL8RHI76mQheyv4TViIng30EF0UfNAc4A&usqp=CAU'
                                        />
                                        <Card.Header>{product.name}</Card.Header>
                                        <Card.Description>
                                            <strong>{product.unitPrice} $</strong>
                                        </Card.Description>
                                    </Link>
                                </Card.Content>
                                {
                                    user &&
                                    <Card.Content extra>
                                        <div className='ui two buttons'>

                                            <AddToFavorites product={product.id}></AddToFavorites>
                                            <AddToCart product={product} quantity={1} ></AddToCart>
                                        </div>
                                    </Card.Content>
                                }
                            </Card>
                        )
                    }
                </Card.Group>
            }

        </div>
    )
}
