const Joi = require('joi');

const schemas = {
    registrationSchema: Joi.object({
        firstName: Joi.string().alphanum().min(2).max(30).required(),
        lastName: Joi.string().alphanum().min(2).max(30).required(),
        email: Joi.string().email().required(),
        password: Joi.string().required().pattern(new RegExp('^[a-zA-Z0-9]{8,30}$'))
            .message('Password must be at least 8 characters length!'),
        phoneNumber: Joi.string().required().pattern(new RegExp('^[+0-9]{9,12}$'))
            .message('Phone number must be at least 9 characters length!')
    }),

    loginSchema: Joi.object({
        email: Joi.string().email().required(),
        password: Joi.string().required().pattern(new RegExp('^[a-zA-Z0-9]{8,30}$'))
            .message('Password must be at least 8 characters length!')
    }),

    updatePasswordSchema: Joi.object({
        currentPassword: Joi.string().required().pattern(new RegExp('^[a-zA-Z0-9]{8,30}$'))
            .message('Password must be at least 8 characters length!'),
        newPassword: Joi.string().required().pattern(new RegExp('^[a-zA-Z0-9]{8,30}$'))
            .message('Password must be at least 8 characters length!')
    }),

    orderSchema: Joi.object({
        addressId: Joi.string().hex().length(24).required(),
        orderItems: Joi.array().items(Joi.object({
            product: Joi.string().hex().length(24).required(),
            quantity: Joi.number().greater(0).required()
        }))
    }),

    commentSchema: Joi.object({
        productId: Joi.string().hex().length(24).required(),
        text: Joi.string().required()
    }),

    addressSchema: Joi.object({
        name: Joi.string().required(),
        country: Joi.string().required(),
        city: Joi.string().required(),
        state: Joi.string().required(),
        street: Joi.string().required(),
        postCode: Joi.string().required().min(5)
    }),

    updateProfileSchema: Joi.object({
        firstName: Joi.string().alphanum().min(2).max(30).required(),
        lastName: Joi.string().alphanum().min(2).max(30).required(),
        phoneNumber: Joi.string().required().pattern(new RegExp('^[+0-9]{9,12}$'))
            .message('Phone number must be at least 9 characters length!')
    })
};

module.exports = schemas;