# Etapa de build
FROM node:20-alpine AS build
WORKDIR /app

# Copiamos package.json y package-lock.json
COPY package*.json ./
RUN npm install

# Copiamos el resto de la aplicación
COPY . .
RUN npm run build --prod

# Etapa de producción
FROM nginx:alpine
COPY --from=build /app/dist/FoodTruck.Web/browser /usr/share/nginx/html

EXPOSE 5000
