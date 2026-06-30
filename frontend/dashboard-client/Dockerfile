# Build
FROM node:22 AS build

WORKDIR /app

COPY package*.json ./

RUN npm install

COPY . .

RUN npm run build

# Runtime
FROM nginx:alpine

COPY --from=build /app/dist/dashboard-client/browser /usr/share/nginx/html

EXPOSE 80