const bcrypt = require('bcryptjs');

const verifyPassword = (password, hash) => {
    return bcrypt.compareSync(password, hash);
};

const generateHash = (password) => {
    return bcrypt.hashSync(password, bcrypt.genSaltSync(8), null);
};;

module.exports = { verifyPassword, generateHash };