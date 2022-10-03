export const LOG_IN = 'LOG_IN';
export const LOG_OUT = 'LOG_OUT';

export function login(user) {
    return {
        type: LOG_IN,
        payload: user
    }
}

export function logout(user) {
    return {
        type: LOG_OUT,
        payload: user
    }
}