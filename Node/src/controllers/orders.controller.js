module.exports.getAll = async (req, res) => {
    res.send(await req.service.getAllOrders(req.user._id));
}

module.exports.createOrder = async (req, res) => {
    res.send(await req.service.createOrder(req));
}

module.exports.cancelOrder = async (req, res) => {
    res.send(await req.service.cancelOrder(req));
}