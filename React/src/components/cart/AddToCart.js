import { Button } from "semantic-ui-react";
import { useDispatch } from 'react-redux';
import { addToCart } from '../../store/actions/cartActions';
import { toast } from 'react-toastify';

export default function AddToCart(props) {

    const dispatch = useDispatch()

    const { quantity, product } = props;

    const handleAddToCart = async () => {
        dispatch(addToCart({
            product: product.id,
            quantity: quantity ? quantity : 1
        }));
        toast.success(`${product.name} added to cart.`);
    }

    return (
        <Button color='green' onClick={handleAddToCart}>Add to cart</Button>
    )
}
