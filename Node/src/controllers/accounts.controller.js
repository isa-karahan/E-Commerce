module.exports.register = async (req, res) => {
    res.send(await req.service.register(req));
}

module.exports.login = async (req, res) => {
    res.send(await req.service.login(req, res));
};

module.exports.logout = async (req, res) => {
    res.send(await req.service.logout(req, res));
};

module.exports.getNewToken = async (req, res) => {
    res.send(await req.service.getNewToken(req, res));
}

module.exports.updatePassword = async (req, res) => {
    res.send(await req.service.updatePassword(req));
}

module.exports.deleteAccount = async (req, res) => {
    res.send(await req.service.deleteAccount(req));
}