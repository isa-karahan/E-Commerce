import { Button } from "semantic-ui-react"
import useAddFavorite from '../../hooks/userService/useAddFavorite';

export default function AddToFavorites(props) {

    const { product } = props;

    const addFavorite = useAddFavorite();

    return (
        <Button color='red'
            floated="left"
            icon='heart'
            onClick={() => addFavorite({ productId: product })}
        ></Button>
    )
}
