import restify from 'restify'
import env from 'dotenv/config'
const builder = require('botbuilder')

// Setting up the server
const server = restify.createServer()
server.listen(3978, () => {
  console.log('%s listening to %s', server.name, server.url);
})

// Create chat bot
const connector = new builder.ChatConnector({
  appId: process.env.Bot_App_Id,
  appPassword: process.env.Bot_App_Pw,
})

const bot = new builder.UniversalBot(connector);

let getRetrunMessage = (sentMessage) => {
  const messages = {
    "Hello":  "Hello there!",
    "What did we do":  "So far not much. You have created a bot with the template, installed required packages, and added this and other messages. Pretty simple though right?",
    "What do you do":  "Right now I return a hard-coded message based on hard-coded questions you send. This messages comes from an object literal in app.js"
  };
  return messages[sentMessage] ||  'The color is unknown.';
};

server.post('/api/messages', connector.listen())

bot.dialog('/', (session) => {
  const message = getRetrunMessage(session.message.text);
  session.send(message);
});
