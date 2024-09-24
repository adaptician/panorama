# Stage 1: Build the Angular application
FROM node:20 AS build

# Set working directory
WORKDIR /app

# Install dependencies
COPY src/Panorama.Web.Host/package*.json ./
RUN yarn install

# Copy the rest of the application files
COPY src/Panorama.Web.Host .

# Build the Angular app
RUN yarn run ng build --configuration production

# Stage 2: Serve the Angular app with Nginx
FROM nginx:alpine

# Copy the built Angular app to the Nginx HTML folder
COPY --from=build /app/wwwroot/dist /usr/share/nginx/html

# Copy custom Nginx configuration file
COPY src/Panorama.Web.Host/nginx.conf /etc/nginx/nginx.conf
COPY src/Panorama.Web.Host/fast-nginx-default.conf /etc/nginx/fast-nginx-default.conf

# Expose service ports.
EXPOSE 80

# Start Nginx server
CMD ["nginx", "-g", "daemon off;"]