const mongoose = require('mongoose');

const commentSchema = new mongoose.Schema({
    product: {
        type: String,
        required: true,
        ref: 'Product'
    },
    user: {
        type: String,
        required: true,
        ref: 'User'
    },
    text: {
        type: String,
        required: true
    },
    added: {
        type: String,
        default: new Date().toISOString().replace(/T/, ' ').replace(/\..+/, '')
    }
});

module.exports = mongoose.model('Comment', commentSchema);