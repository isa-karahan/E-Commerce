const mongoose = require('mongoose');
var mongoose_delete = require('mongoose-delete');

const orderDetailSchema = new mongoose.Schema({
    user: {
        type: String,
        required: true
    },
    address: {
        type: String,
        required: true,
        ref: 'Address'
    },
    totalPrice: {
        type: Number,
        required: true
    },
    orderDate: {
        type: String,
        required: true
    }
});

orderDetailSchema.plugin(mongoose_delete, { overrideMethods: 'all' });

module.exports = mongoose.model('OrderDetail', orderDetailSchema);;