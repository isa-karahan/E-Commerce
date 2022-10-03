const express = require('express');
const { cancelOrder, createOrder, getAll } = require('../controllers/orders.controller');

const { validate, validationSchemas } = require('../middlewares/index');

const orderRoutes = express.Router();

orderRoutes.delete('/orders', async (req, res) => {
    await cancelOrder(req, res);
});
orderRoutes.get('/orders', async (req, res) => {
    await getAll(req, res);
});
orderRoutes.post('/orders', validate(validationSchemas.orderSchema), async (req, res) => {
    await createOrder(req, res);
});

module.exports = orderRoutes;