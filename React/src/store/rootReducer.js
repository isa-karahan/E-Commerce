import { combineReducers } from 'redux';
import accountReducer from './reducers/accountReducers';
import cartReducer from './reducers/cartReducers';
import serverReducer from './reducers/serverReducers';

const rootReducer = combineReducers({
    cartItems: cartReducer,
    user: accountReducer,
    server: serverReducer
});

export default rootReducer;