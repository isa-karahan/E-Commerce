module.exports.getProducts = async (req, res) => {
    res.send(await req.service.getProducts());
}

module.exports.getProduct = async (req, res) => {
    res.send(await req.service.getProduct(req.params.id));
}

module.exports.getCategories = async (req, res) => {
    res.send(await req.service.getCategories());
}

module.exports.getCategory = async (req, res) => {
    res.send(await req.service.getCategory(req.params.categoryId));
}

module.exports.addComment = async (req, res) => {
    res.send(await req.service.addComment(req));
}