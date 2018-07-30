import { alertConstants } from '../_constants/AlertConstants';

// export const alertActions = {
//     success,
//     error,
//     clear
// };


// Create inteface for return typ
export const AlertActions = {
    success(message: string) {
        return { type: alertConstants.SUCCESS, message };
    },
    error(message: string) {
        return { type: alertConstants.ERROR, message };
    },
    clear() {
        return { type: alertConstants.CLEAR };
    }
}

// function success(message: string) {
//     return { type: alertConstants.SUCCESS, message };
// }

// function error(message: string) {
//     return { type: alertConstants.ERROR, message };
// }

// function clear() {
//     return { type: alertConstants.CLEAR };
// }