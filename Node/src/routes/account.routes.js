const express = require('express');
const { register, login, updatePassword, deleteAccount, logout, getNewToken } = require('../controllers/accounts.controller');

const { authorize, validate, validationSchemas } = require('../middlewares/index');

const accountRoutes = express.Router();

accountRoutes.post('/register', validate(validationSchemas.registrationSchema), async (req, res) => {
    await register(req, res);
});

accountRoutes.post('/login', validate(validationSchemas.loginSchema), async (req, res) => {
    await login(req, res);
});

accountRoutes.put('/', authorize, validate(validationSchemas.updatePasswordSchema), async (req, res) => {
    await updatePassword(req, res);
});

accountRoutes.delete('/', authorize, async (req, res) => {
    await deleteAccount(req, res);
});

accountRoutes.post('/logout', authorize, async (req, res) => {
    await logout(req, res);
});

accountRoutes.post('/token', async (req, res) => {
    await getNewToken(req, res);
});

module.exports = accountRoutes;