require('dotenv/config');
const jwt = require('jsonwebtoken');

const { ACCESS_TOKEN_SECRET } = process.env;

module.exports = (req, res, next) => {
    const token = req.cookies.accessToken;

    jwt.verify(token, ACCESS_TOKEN_SECRET, (err, token) => {

        if (err) {
            throw new Error('Unauthorized');
        }

        req.user = token.user;
        next();
    })
}