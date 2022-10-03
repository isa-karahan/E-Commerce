import { ADD_TO_CART, REMOVE_FROM_CART } from "../actions/cartActions";

export default function cartReducer(cartItems, { type, payload }) {

    let newCart = null;

    switch (type) {
        case ADD_TO_CART:
            let existingCartItem = cartItems.find(cartItem => cartItem.product === payload.product);

            if (existingCartItem) {
                newCart = cartItems.map((cartItem) =>
                    cartItem.product === payload.product
                        ? { ...cartItem, quantity: cartItem.quantity + payload.quantity }
                        : cartItem
                );
            }
            else {
                newCart = [...cartItems, payload];
            }

            localStorage.setItem('cart', JSON.stringify(newCart));

            return newCart;

        case REMOVE_FROM_CART:
            newCart = cartItems.filter(cartItem => cartItem.product !== payload.product);
            localStorage.setItem('cart', JSON.stringify(newCart));
            return newCart;

        default:
            const cart = JSON.parse(localStorage.getItem('cart'));
            return cart ? cart : [];
    }
}
