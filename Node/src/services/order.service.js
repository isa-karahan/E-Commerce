const User = require('../models/user.model');
const Wallet = require('../models/wallet.model');
const Address = require('../models/address.model');
const Product = require('../models/product.model');
const OrderItem = require('../models/order_item.model');
const { Result, DataResult } = require('../utils/result');
const OrderDetail = require('../models/order_detail.model');

module.exports = class OrderService {

    async getAllOrders(userId) {

        const result = [];

        const orders = await OrderDetail.find({ user: userId }).populate('address');

        for await (let order of orders) {
            const orderItems = await OrderItem.find({ orderDetail: order._id })
                .populate('product', ['name', 'unitPrice']);

            const { name, country, city, state, street, postCode } = order.address;

            result.push({
                orderDetailId: order._id,
                orderAddress: `${name}: ${street} ${postCode} - ${state}/${city}/${country}`,
                totalPrice: order.totalPrice,
                orderDate: order.orderDate,
                orderItems: orderItems.map(
                    item => {
                        return {
                            quantity: item.quantity,
                            productId: item.product._id,
                            productName: item.product.name,
                            unitPrice: item.product.unitPrice
                        };
                    }
                )
            });
        }

        return DataResult.Success(result);
    }

    async createOrder(req) {

        const { addressId, orderItems } = req.body;

        const user = await User.findById(req.user._id);

        if (!user) {
            throw new Error('User not found!');
        }

        const address = await Address.findById(addressId);

        if (!address) {
            throw new Error('User address not found!');
        }

        const totalPrice = await this.calculateTotalPrice(orderItems);

        await this.addTransaction(user._id, -totalPrice, 'Payment');

        const newOrder = new OrderDetail({
            user: user._id,
            address: addressId,
            totalPrice: totalPrice,
            orderDate: new Date().toISOString().replace(/T/, ' ').replace(/\..+/, '')
        });

        await newOrder.save();

        for await (let item of orderItems) {

            const newItem = new OrderItem({
                orderDetail: newOrder._id,
                product: item.product,
                quantity: item.quantity
            });
            await newItem.save();
        }

        await this.addBonuses(orderItems, user._id, false);

        return Result.Success('Order created successfully.');
    }

    async cancelOrder(req) {

        const { orderId } = req.body;

        const orderToDelete = await OrderDetail.findById(orderId);

        if (!orderToDelete) {
            throw new Error('Order not Found!');
        }
        await OrderDetail.deleteById(orderId);

        const result = await OrderItem.find({ orderDetail: orderId });

        await this.addTransaction(orderToDelete.user, orderToDelete.totalPrice, 'Refund');

        await this.addBonuses(result, orderToDelete.user, true);

        await OrderItem.delete({ orderDetail: orderId })

        return Result.Success('Order deleted successfully.');
    }

    async addBonuses(orderItems, userId, isDelete) {

        let bonus = 0;

        for await (const item of orderItems) {

            const result = await Product.findOne({ _id: item.product }).populate('category');

            bonus += result.unitPrice * item.quantity * (result.category.bonusPercentage / 100.0);
        };

        if (isDelete) {
            await this.addTransaction(userId, -bonus, 'Cashback Return');
        }
        else {
            await this.addTransaction(userId, bonus, 'Cashback');
        }
    }

    async calculateTotalPrice(orderItems) {
        let total = 0;

        for await (const item of orderItems) {

            const product = await Product.findById(item.product);

            if (!product) {
                throw new Error('Product not found!');
            }

            if (item.quantity > product.unitsInStock) {
                throw new Error(`${product.name} doesn't have enough stock!`);
            }

            total += product.unitPrice * item.quantity;
        }

        return total;
    }

    // incoming amount should be negative if it is a draw
    // positive if a cashback
    async addTransaction(userId, amount, type) {
        const wallet = await Wallet.findOne({ user: userId });

        if (wallet.balance < -amount) {
            throw new Error('insufficient balance!');
        }

        wallet.transactions.push({
            amount: amount,
            type: type,
            date: new Date().toISOString().replace(/T/, ' ').replace(/\..+/, '')
        });

        wallet.balance += amount;
        await wallet.save();
    }
} 