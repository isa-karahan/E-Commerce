module.exports.addAddress = async (req, res) => {
    res.send(await req.service.addAddress(req));
}

module.exports.deleteAddress = async (req, res) => {
    res.send(await req.service.deleteAddress(req));
}

module.exports.getAddresses = async (req, res) => {
    res.send(await req.service.getAddresses(req));
}

module.exports.getWallet = async (req, res) => {
    res.send(await req.service.getWallet(req));
}

module.exports.updateProfile = async (req, res) => {
    res.send(await req.service.updateProfile(req));
}

module.exports.getProfile = async (req, res) => {
    res.send(await req.service.getProfile(req));
}

module.exports.depositMoney = async (req, res) => {
    res.send(await req.service.depositMoney(req));
}

module.exports.addToFavorites = async (req, res) => {
    res.send(await req.service.addToFavorites(req));
}

module.exports.removeFromFavorites = async (req, res) => {
    res.send(await req.service.removeFromFavorites(req));
}

module.exports.getFavoritesList = async (req, res) => {
    res.send(await req.service.getFavorites(req));
}