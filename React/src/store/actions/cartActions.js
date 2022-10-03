export const ADD_TO_CART = 'ADD_TO_CART';
export const REMOVE_FROM_CART = 'REMOVE_FROM_CART';

export function addToCart(cartItem) {
    return {
        type: ADD_TO_CART,
        payload: cartItem
    }
}

export function removeFromCart(cartItem) {
    return {
        type: REMOVE_FROM_CART,
        payload: cartItem
    }
}