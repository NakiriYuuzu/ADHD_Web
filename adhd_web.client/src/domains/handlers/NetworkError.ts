export enum NetworkError {
    BAD_REQUEST,
    NOT_FOUND,
    INTERNAL_SERVER_ERROR,
    NO_INTERNET,
    UNKNOWN_ERROR
}

function getNetworkErrorFromStatusCode(statusCode: number): NetworkError {
    switch (statusCode) {
        case 400:
            return NetworkError.BAD_REQUEST
        case 404:
            return NetworkError.NOT_FOUND
        case 500:
            return NetworkError.INTERNAL_SERVER_ERROR
        default:
            return NetworkError.UNKNOWN_ERROR
    }
}