export const CHANGE_SERVER = 'CHANGE_SERVER';

export function changeServer(url) {
    return {
        type: CHANGE_SERVER,
        url: url
    }
}