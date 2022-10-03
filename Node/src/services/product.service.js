const User = require('../models/user.model');
const Comment = require('../models/comment.model');
const Product = require('../models/product.model');
const Category = require('../models/category.model');
const { Result, DataResult } = require('../utils/result');

module.exports = class PRoductService {
    async getProducts() {
        const products = await Product.find().lean();
        const mappedProducts = products.map(product => {
            let res = {
                ...product,
                id: product._id,
                categoryId: product.category
            };
            delete res._id;
            delete res.category;

            return res;
        })
        return DataResult.Success(mappedProducts);
    }

    async getProduct(productId) {
        const product = await Product.findById(productId).populate('category');

        if (!product) {
            throw new Error('Product not found!');
        }

        const comments = await Comment.find({ product: productId }).populate('user', ['firstName', 'lastName']);

        const mappedComments = comments.map(
            comment => {
                const { firstName, lastName } = comment.user;
                return {
                    id: comment._id,
                    userName: `${firstName} ${lastName}`,
                    text: comment.text,
                    added: comment.added
                };
            }
        )

        const data = {
            id: product._id,
            name: product.name,
            comments: mappedComments,
            unitPrice: product.unitPrice,
            category: product.category.name,
            description: product.description,
            bonusPercentage: product.category.bonusPercentage
        };

        return DataResult.Success(data);
    }

    async getCategories() {
        const categories = await Category.find().lean();
        const mappedCategories = categories.map(category => {
            let res = {
                ...category,
                id: category._id
            };
            delete res._id;
            return res;
        })
        return DataResult.Success(mappedCategories);
    }

    async getCategory(categoryId) {

        const products = await this.getProducts();

        return DataResult.Success(products.data.filter(p => p.categoryId === categoryId));
    }

    async addComment(req) {
        const { productId, text } = req.body;

        const product = await Product.findById(productId);
        const user = await User.findById(req.user._id);

        if (!(product && user)) {
            throw new Error('Invalid id input!');
        }

        const comment = new Comment({
            product: productId,
            text: text,
            user: req.user._id
        });

        await comment.save();

        return Result.Success('Comment successfully added.');
    }
}