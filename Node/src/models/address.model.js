const mongoose = require('mongoose');
var mongoose_delete = require('mongoose-delete');

const addressSchema = new mongoose.Schema({
    user: {
        type: String,
        required: true
    },
    name: {
        type: String,
        required: true
    },
    country: {
        type: String,
        required: true
    },
    city: {
        type: String,
        required: true
    },
    state: {
        type: String,
        required: true
    },
    street: {
        type: String,
        required: true
    },
    postCode: {
        type: String,
        required: true
    }
});

addressSchema.plugin(mongoose_delete, { overrideMethods: 'all' });

module.exports = mongoose.model('Address', addressSchema);