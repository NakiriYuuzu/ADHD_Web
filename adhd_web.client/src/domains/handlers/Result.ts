export type Result<D, E> = 
    { type: ResultType.SUCCESS; data: D } | 
    { type: ResultType.FAILURE; error: E } | 
    { type: ResultType.LOADING } |
    { type: ResultType.IDLE }

export enum ResultType {
    SUCCESS,
    FAILURE,
    LOADING,
    IDLE
}

// Sample usage:
export enum PasswordError {
    NONE = "NONE",
    TOO_SHORT = "TOO_SHORT",
    NO_UPPERCASE = "NO_UPPERCASE",
    NO_DIGIT = "NO_DIGIT",
}

// PasswordValidator.ts
export function validatePassword(password: string): Result<void, PasswordError> {
    if (password.length < 9) {
        return {type: ResultType.FAILURE, error: PasswordError.TOO_SHORT};
    }

    if (!password.match(/[A-Z]/)) {
        return {type: ResultType.FAILURE, error: PasswordError.NO_UPPERCASE};
    }

    if (!password.match(/[0-9]/)) {
        return {type: ResultType.FAILURE, error: PasswordError.NO_DIGIT};
    }

    return {type: ResultType.SUCCESS, data: undefined};
}

// UserRegistration.ts
function registerUser(username: string, password: string): void {
    const passwordValidation = validatePassword(password);
    if (passwordValidation.type === ResultType.FAILURE) {
        switch (passwordValidation.error) {
            case PasswordError.TOO_SHORT:
                console.error('Password is too short');
                break;
            case PasswordError.NO_UPPERCASE:
                console.error('Password must contain at least one uppercase character');
                break;
            case PasswordError.NO_DIGIT:
                console.error('Password must contain at least one digit');
                break;
        }
    }
}
