const { Result } = require('../../utils/result');

module.exports = (err, req, res, next) => {
    res.status(err.status || 500);
    console.log(req.url);
    console.log(err);

    let result = Result.Error(err.message);

    if (err.message.includes('ObjectId')) {
        result.message = 'Invalid id input!';
    }

    if (err.message.includes('Unauthorized')) {
        res.status(401);
    }

    res.send(result);
}