class Result {

    constructor(isSuccess, message) {
        this.isSuccess = isSuccess;
        this.message = message;
    }

    static Success(message = '') {
        return new Result(true, message);
    }

    static Error(message) {
        return new Result(false, message);
    }
}

class DataResult {

    constructor(isSuccess, message, data) {
        this.isSuccess = isSuccess;
        this.message = message;
        this.data = data;
    }

    static Success(data, message = '') {
        return new DataResult(true, message, data);
    }

    static Error(data, message) {
        return new DataResult(false, message, data);
    }
}

module.exports = { Result, DataResult };