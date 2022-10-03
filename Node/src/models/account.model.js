const mongoose = require('mongoose');
var mongoose_delete = require('mongoose-delete');

const accountSchema = new mongoose.Schema({
    email: {
        type: String,
        required: true,
        unique: true
    },
    password: {
        type: String,
        required: true
    },
    user: {
        type: String,
        required: true,
        ref: 'User'
    },
    refreshToken: {
        type: String,
        default: null
    },
    refreshTokenExpiryTime: {
        type: Date,
        default: null
    }
});
accountSchema.plugin(mongoose_delete, { overrideMethods: 'all' });

module.exports = mongoose.model('Account', accountSchema);