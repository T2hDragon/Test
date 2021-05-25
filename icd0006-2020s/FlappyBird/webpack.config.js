const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');

const subDir ='/flappy-ts/';

module.exports = {
    mode: 'production',
    entry: "./src/index.js",
    output: {
        path: path.resolve(__dirname, 'dist'),
        publicPath: subDir,
        filename: "[name].js"
    },
    plugins: [
        new HtmlWebpackPlugin({
            base: subDir,
            template: "./src/index.html",
            inject: "body",
            minify: false,
        })
    ]
}
