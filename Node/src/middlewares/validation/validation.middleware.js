const Joi = require('joi');

const validationMiddleware = schema => {
    return (req, res, next) => {
        const { error } = schema.validate(req.body);
        const isValid = error == null;

        if (isValid) {
            next();
        } else {
            const { details } = error;
            let message = details.map(i => i.message).join(',').replace(/\"/g, '');
            
            throw new Error(message);
        }
    }
}
module.exports = validationMiddleware;