const mongoose = require('mongoose');
var mongoose_delete = require('mongoose-delete');

const favoritesSchema = new mongoose.Schema({
    user: {
        type: String,
        required: true
    },
    product: {
        type: String,
        required: true,
        ref: 'Product'
    },
    added: {
        type: String,
        default: new Date().toISOString().replace(/T/, ' ').replace(/\..+/, '')
    }
});

favoritesSchema.plugin(mongoose_delete, { overrideMethods: 'all' });

module.exports = mongoose.model('Favorites', favoritesSchema);