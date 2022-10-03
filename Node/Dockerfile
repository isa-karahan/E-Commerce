FROM node:alpine

COPY package*.json ./

RUN npm install

COPY . .

EXPOSE 3200

CMD [ "node", "app.js" ]