const mongoose = require('mongoose');
var mongoose_delete = require('mongoose-delete');

const transactionSchema = new mongoose.Schema({
    amount: {
        type: Number,
        required: true
    },
    date: {
        type: String,
        required: true
    },
    type: {
        type: String,
        required: true
    }
});

const walletSchema = new mongoose.Schema({
    user: {
        type: String,
        required: true
    },
    balance: {
        type: Number,
        default: 0
    },
    transactions: {
        type: [transactionSchema],
        default: []
    }
});

walletSchema.plugin(mongoose_delete, { overrideMethods: 'all' });

const Wallet = mongoose.model('Wallet', walletSchema);

module.exports = Wallet;