const mongoose = require('mongoose');
var mongoose_delete = require('mongoose-delete');

const userSchema = new mongoose.Schema({
    firstName: {
        type: String,
        required: true
    },
    lastName: {
        type: String,
        required: true
    },
    phoneNumber: {
        type: String,
        required: true
    },
    added: {
        type: String,
        default: new Date().toISOString().replace(/T/, ' ').replace(/\..+/, '')
    }
});

userSchema.plugin(mongoose_delete, { overrideMethods: 'all' });

module.exports = mongoose.model('User', userSchema);