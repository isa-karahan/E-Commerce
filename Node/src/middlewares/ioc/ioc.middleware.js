const iocMiddleware = service => {
    return (req, res, next) => {

        req.service = new service();
        next();
    }
}

module.exports = iocMiddleware;