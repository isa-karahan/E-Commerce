const User = require('../models/user.model');
const Wallet = require('../models/wallet.model');
const Account = require('../models/account.model');
const Address = require('../models/address.model');
const Product = require('../models/product.model');
const Favorites = require('../models/favorites.model');
const { Result, DataResult } = require('../utils/result');

module.exports = class UserService {

    async addAddress(req) {

        const { name, country, city, state, street, postCode } = req.body;

        const newAddress = new Address({
            user: req.user._id,
            name: name,
            country: country,
            city: city,
            state: state,
            street: street,
            postCode: postCode
        });

        await newAddress.save();

        return Result.Success('Address successfully added.');
    }

    async deleteAddress(req) {

        const { addressId } = req.body;

        const addressToDelete = await Address.findById(addressId);

        if (!addressToDelete) {
            throw new Error('Address not found!');
        }

        addressToDelete.delete();

        return Result.Success('Address successfully deleted.');
    }

    async getAddresses(req) {

        const addresses = await Address.find({ user: req.user._id }).lean();

        const mappedAddresses = addresses.map(
            address => {
                let res = {
                    ...address,
                    id: address._id
                };
                delete res._id;

                return res;
            }
        )

        return DataResult.Success(mappedAddresses);
    }

    async getWallet(req) {

        const wallet = await Wallet.findOne({ user: req.user._id }).lean();

        if (!wallet) {
            throw new Error('User not found!');
        }

        const mappedTransactions = wallet.transactions.map(
            tr => {
                let res = {
                    ...tr,
                    id: tr._id
                };
                delete tr._id;

                return res;
            }
        )

        return DataResult.Success({
            walletId: wallet._id,
            balance: wallet.balance,
            transactions: mappedTransactions
        });
    }

    async updateProfile(req) {
        const { firstName, lastName, phoneNumber } = req.body;

        const user = await User.findById(req.user._id);

        if (!user) {
            throw new Error('User not found!');
        }

        user.firstName = firstName;
        user.lastName = lastName;
        user.phoneNumber = phoneNumber;

        await user.save();

        return Result.Success('Profile successfully updated.');
    }

    async getProfile(req) {
        const user = await User.findById(req.user._id);
        const account = await Account.findOne({ user: req.user._id });

        if (!user) {
            throw new Error('User not found!');
        }

        const data = {
            firstName: user.firstName,
            lastName: user.lastName,
            phoneNumber: user.phoneNumber,
            email: account.email,
            added: user.added
        };

        return DataResult.Success(data);
    }

    async depositMoney(req) {

        const { amount } = req.body;

        if (amount < 0) {
            throw new Error('Deposit amount cannot be negative value!');
        }

        const wallet = await Wallet.findOne({ user: req.user._id });

        wallet.transactions.push({
            amount: amount,
            type: 'Deposit',
            date: new Date().toISOString().replace(/T/, ' ').replace(/\..+/, '')
        });

        wallet.balance += Number(amount);
        await wallet.save();

        return Result.Success('Deposit is successfull.');
    }

    async addToFavorites(req) {

        const { productId } = req.body;

        const check = await Favorites.findOne({ user: req.user._id, product: productId });

        if (check) {
            throw new Error('Product is already in the list!');
        }

        const product = await Product.findById(productId);

        if (!product) {
            throw new Error('Product not found!');
        }

        const newFav = new Favorites({
            product: productId,
            user: req.user._id
        });

        await newFav.save();

        return Result.Success(`${product.name} added to the favorites list.`);
    }

    async removeFromFavorites(req) {

        const { productId } = req.body;

        const favorite = await Favorites.findOne({ user: req.user._id, product: productId }).populate('product');

        if (!favorite) {
            throw new Error('Product is not in the list!');
        }

        favorite.delete();

        return Result.Success(`${favorite.product.name} removed from the favorites list.`);
    }

    async getFavorites(req) {

        const favorites = await Favorites.find({ user: req.user._id }).populate('product', ['name', 'unitPrice']);

        const mappedFavorites = favorites.map(
            fav => {
                const { name, unitPrice, _id } = fav.product;
                return {
                    id: fav._id,
                    productName: name,
                    productId: _id,
                    unitPrice: unitPrice,
                    added: fav.added
                }
            }
        );

        return DataResult.Success(mappedFavorites);
    }
}