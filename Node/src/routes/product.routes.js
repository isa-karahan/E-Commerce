const express = require('express');
const { addComment, getCategories, getCategory, getProduct, getProducts } = require('../controllers/products.controller');

const { authorize, validate, validationSchemas } = require('../middlewares/index');

const productRoutes = express.Router();

productRoutes.get('/', async (req, res) => {
    await getProducts(req, res);
});

productRoutes.get('/categories', async (req, res) => {
    await getCategories(req, res);
});

productRoutes.post('/comment', authorize, validate(validationSchemas.commentSchema), async (req, res) => {
    await addComment(req, res);
});

productRoutes.get('/categories/:categoryId', async (req, res) => {
    await getCategory(req, res);
});

productRoutes.get('/:id', async (req, res) => {
    await getProduct(req, res);
});

module.exports = productRoutes;