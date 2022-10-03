import { LOG_IN, LOG_OUT } from "../actions/accountActions";

export default function accountReducer(user, { type, payload }) {

    switch (type) {
        case LOG_IN:
            localStorage.setItem('user', JSON.stringify(payload));
            return payload;

        case LOG_OUT:
            localStorage.clear();
            return null;

        default:
            return JSON.parse(localStorage.getItem('user'));
    }
}
