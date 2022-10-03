const mongoose = require('mongoose');
var mongoose_delete = require('mongoose-delete');

const orderItemSchema = new mongoose.Schema({
    orderDetail: {
        type: String,
        required: true,
        ref: 'OrderDetail'
    },
    product: {
        type: String,
        required: true,
        ref: 'Product'
    },
    quantity: {
        type: Number,
        required: true
    }
});

orderItemSchema.plugin(mongoose_delete, { overrideMethods: 'all' });

module.exports = mongoose.model('OrderItem', orderItemSchema);