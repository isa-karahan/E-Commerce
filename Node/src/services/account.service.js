require('dotenv/config');
const crypto = require("crypto");
const jwt = require('jsonwebtoken');
const { Result, DataResult } = require('../utils/result');

const User = require('../models/user.model');
const Wallet = require('../models/wallet.model');
const Account = require('../models/account.model');
const Comment = require('../models/comment.model');
const Favorites = require('../models/favorites.model');
const OrderItem = require('../models/order_item.model');
const OrderDetail = require('../models/order_detail.model');

const { verifyPassword, generateHash } = require('../utils/hashing_helper');

const { ACCESS_TOKEN_SECRET } = process.env;

module.exports = class AccountService {

    async register(req) {
        const { firstName, lastName, email, password, phoneNumber } = req.body;

        const data = await Account.findOne({ email: email });

        if (data) {
            throw new Error("Email is already in use!");
        }

        const user = new User({
            firstName: firstName,
            lastName: lastName,
            phoneNumber: phoneNumber
        });

        await user.save();

        const wallet = new Wallet({
            user: user._id
        });

        await wallet.save();

        const account = new Account({
            email: email,
            password: generateHash(password),
            user: user._id
        });

        await account.save();

        return Result.Success('Account successfully created, please login.');
    }

    async login(req, res) {
        const { email, password } = req.body;

        const account = await Account.findOne({ email: email });

        if (!account) {
            throw new Error("Account not found!");
        }

        if (!verifyPassword(password, account.password)) {
            throw new Error("Wrong password!");
        }

        const user = await User.findById(account.user);

        const accessToken = this.generateAccessToken(user);
        const refreshToken = this.generateRefreshToken();

        let expire = new Date();
        expire.setDate(expire.getDate() + 7)

        account.refreshToken = refreshToken;
        account.refreshTokenExpiryTime = expire;

        await account.save();

        let data = {
            userName: `${user.firstName} ${user.lastName}`
        }

        res.cookie('accessToken', accessToken, { httpOnly: true, maxAge: 24 * 60 * 60 * 1000 });
        res.cookie('refreshToken', refreshToken, { httpOnly: true, maxAge: 7 * 24 * 60 * 60 * 1000 });

        return DataResult.Success(data, 'Logged in successfully.');
    }

    async updatePassword(req) {
        const { currentPassword, newPassword } = req.body;

        const account = await Account.findOne({ user: req.user._id });

        if (!account) {
            throw new Error("Account not found!");
        }

        if (!verifyPassword(currentPassword, account.password)) {
            throw new Error("Wrong password!");
        }

        account.password = generateHash(newPassword);

        await account.save();

        return Result.Success('Password successfully updated.');
    }

    async deleteAccount(req) {

        const accountToDelete = await Account.findOne({ user: req.user._id });

        if (!accountToDelete) {
            throw new Error('Account not found!');
        }

        accountToDelete.delete();

        await User.deleteById(req.user._id);
        await Wallet.delete({ user: req.user._id });

        const orders = await OrderDetail.find({ user: req.user._id });

        for await (let order of orders) {
            await OrderItem.delete({ orderDetail: order._id });
        }

        await OrderDetail.delete({ user: req.user._id });
        await Comment.deleteMany({ user: req.user._id });
        await Favorites.delete({ user: req.user._id });

        return Result.Success('Account successfully deleted.');
    }

    async getNewToken(req, res) {
        const { accessToken, refreshToken } = req.cookies;

        if (!accessToken || !refreshToken) {
            throw new Error('Unauthorized!');
        }

        const token = jwt.verify(accessToken, ACCESS_TOKEN_SECRET, { ignoreExpiration: true });

        if (!token) {
            throw new Error('Unauthorized!');
        }

        let account = await Account.findOne({ user: token.user._id });

        if (account.refreshToken !== refreshToken) {

            if (account.refreshTokenExpiryTime < Date.now()) {
                account.refreshToken = null;
                account.refreshTokenExpiryTime = null;
                await account.save();
            }

            throw new Error('Invalid client request!');
        }

        const newAccessToken = this.generateAccessToken(token.user);
        const newRefreshToken = this.generateRefreshToken();

        account.refreshToken = newRefreshToken;
        await account.save();

        res.cookie('accessToken', newAccessToken, { httpOnly: true, maxAge: 24 * 60 * 60 * 1000 });
        res.cookie('refreshToken', newRefreshToken, { httpOnly: true, maxAge: 7 * 24 * 60 * 60 * 1000 });

        return Result.Success();
    }

    async logout(req, res) {

        res.clearCookie('accessToken');
        res.clearCookie('refreshToken');

        let account = await Account.findOne({ user: req.user._id });
        account.refreshToken = null;
        account.refreshTokenExpiryTime = null;
        await account.save();

        return Result.Success('Logged out successfully.');
    }

    generateAccessToken(user) {
        return jwt.sign({ user }, ACCESS_TOKEN_SECRET, { expiresIn: '1d' });
    }

    generateRefreshToken() {
        return crypto.randomBytes(64).toString('hex');
    }

}
