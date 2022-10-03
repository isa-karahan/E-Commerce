const cors = require('cors');
require('express-async-errors');
const express = require('express');
const cookieParser = require("cookie-parser");

const { authorize, errorMiddleware, ioc } = require('./src/middlewares/index');

const { AccountService, OrderService, ProductService, UserService } = require('./src/services/index');

const { accountRoutes, orderRoutes, productRoutes, userRoutes } = require('./src/routes/index');

const app = express();

app.use(cookieParser());

app.use(cors({
    credentials: true,
    allowedHeaders: ['Content-Type', 'Authorization', 'Access-Control-Allow-Origin'],
    origin: ['http://localhost:3000']
}));

app.use(express.json());
app.use(express.urlencoded({ extended: true }));

app.use('/api/accounts', ioc(AccountService), accountRoutes);
app.use('/api/products', ioc(ProductService), productRoutes);
app.use('/api/users/', authorize, ioc(UserService), userRoutes);
app.use('/api/users/', authorize, ioc(OrderService), orderRoutes);

app.use(errorMiddleware);

module.exports = app;