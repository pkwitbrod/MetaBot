'use strict';

var _restify = require('restify');

var _restify2 = _interopRequireDefault(_restify);

var _config = require('dotenv/config');

var _config2 = _interopRequireDefault(_config);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var builder = require('botbuilder');

// Setting up the server
var server = _restify2.default.createServer();
server.listen(3978, function () {
  console.log('%s listening to %s', server.name, server.url);
});

// Create chat bot
var connector = new builder.ChatConnector({
  appId: process.env.Bot_App_Id,
  appPassword: process.env.Bot_App_Pw
});

var bot = new builder.UniversalBot(connector);

var getRetrunMessage = function getRetrunMessage(sentMessage) {
  var messages = {
    "Hello": "Hello there!",
    "What did we do": "So far not much. You have created a bot with the template, installed required packages, and added this and other messages. Pretty simple though right?",
    "What do you do": "Right now I return a hard-coded message based on hard-coded questions you send. This messages comes from an object literal in app.js"
  };
  return messages[sentMessage] || 'The color is unknown.';
};

server.post('/api/messages', connector.listen());

bot.dialog('/', function (session) {
  var message = getRetrunMessage(session.message.text);
  session.send(message);
});