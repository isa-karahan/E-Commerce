const mongoose = require('mongoose');

const productSchema = new mongoose.Schema({
    name: {
        type: String,
        required: true
    },
    unitPrice: {
        type: Number,
        required: true
    },
    unitsInStock: {
        type: Number,
        required: true
    },
    description: {
        type: String,
        required: true
    },
    category: {
        type: String,
        required: true,
        ref:'Category'
    }
});

const Product = mongoose.model('Product', productSchema);

module.exports = Product;