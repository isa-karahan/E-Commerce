require('dotenv/config');
const mongoose = require('mongoose');
const app = require('./server');

mongoose.connect(process.env.DB_CONNECTION, () => {
    console.log('Connected to Database.');

    app.listen(3200, () => {
        console.log("Server has started!")
    });
})
    .catch((error) => {
        console.log("Database connection failed. exiting now...");
        console.error(error);
        process.exit(1);
    });