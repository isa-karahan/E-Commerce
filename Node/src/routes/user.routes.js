const express = require('express');
const { getAddresses, addAddress, deleteAddress, depositMoney,
    getProfile, getWallet, updateProfile, getFavoritesList,
    addToFavorites, removeFromFavorites } = require('../controllers/users.controller');

const { validate, validationSchemas } = require('../middlewares/index');

const userRoutes = express.Router();

userRoutes.post('/address', validate(validationSchemas.addressSchema), async (req, res) => {
    await addAddress(req, res);
});

userRoutes.get('/address', async (req, res) => {
    await getAddresses(req, res);
});

userRoutes.delete('/address', async (req, res) => {
    await deleteAddress(req, res);
});

userRoutes.get('/wallet', async (req, res) => {
    await getWallet(req, res);
});

userRoutes.put('/wallet', async (req, res) => {
    await depositMoney(req, res);
});

userRoutes.put('/', validate(validationSchemas.updateProfileSchema), async (req, res) => {
    await updateProfile(req, res);
});

userRoutes.get('/', async (req, res) => {
    await getProfile(req, res);
});

userRoutes.post('/favorites', async (req, res) => {
    await addToFavorites(req, res);
});

userRoutes.get('/favorites', async (req, res) => {
    await getFavoritesList(req, res);
});

userRoutes.delete('/favorites', async (req, res) => {
    await removeFromFavorites(req, res);
});

module.exports = userRoutes;