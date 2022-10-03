import { configureStore } from '@reduxjs/toolkit';
import { composeWithDevTools } from 'redux-devtools-extension';
import rootReducer from './rootReducer';

export function getStore() {
    return configureStore({reducer:rootReducer, composeWithDevTools});
}