const mongoose = require('mongoose');

const categorySchema = new mongoose.Schema({
    name: {
        type: String,
        required: true
    },
    bonusPercentage: {
        type: Number,
        required: true
    }
});

module.exports = mongoose.model('Category', categorySchema);