module.exports.errorMiddleware = require('./error/error.middleware');

module.exports.authorize = require('./auth/authorize');

module.exports.validate = require('./validation/validation.middleware');
module.exports.validationSchemas = require('./validation/validation_schemas');

module.exports.ioc = require('./ioc/ioc.middleware');