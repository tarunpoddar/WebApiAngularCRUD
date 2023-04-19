const path = require('.');

module.exports = {
  entry: './src/main.ts', // entry point of your Angular application
  output: {
    path: path.resolve(__dirname, 'dist'), // output directory for the bundled files
    filename: 'bundle.js' // name of the bundled file
  },
  resolve: {
    extensions: ['.ts', '.js'] // file extensions to resolve
  },
  module: {
    rules: [
      {
        test: /\.ts$/, // use ts-loader to transpile TypeScript files
        loader: 'ts-loader',
        exclude: /node_modules/
      },
      {
        test: /\.css$/, // use style-loader and css-loader to handle CSS files
        loaders: ['style-loader', 'css-loader']
      },
      {
        test: /\.html$/, // use html-loader to handle HTML files
        loader: 'html-loader'
      }
    ]
  }
};
