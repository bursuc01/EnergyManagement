# Use an official Node.js image as the base
FROM node:latest as builder

# Set the working directory
WORKDIR /app

# Copy dependency files and package.json
COPY package*.json ./

# Install dependencies
RUN npm install

# Copy all other files
COPY . .
RUN chmod -R 777 /app
# Build the Angular application
RUN npm run build

# Use an official Nginx image as the base for the final image
FROM nginx:alpine
RUN apk --no-cache add curl
EXPOSE 8082
RUN curl -L https://github.com/a8m/envsubst/releases/download/v1.2.0/envsubst-`uname -s`-`uname -m` -o envsubst && \
    chmod +x envsubst && \
    mv envsubst /usr/local/bin
	

COPY --from=builder /app/nginx.conf /etc/nginx/nginx.template
CMD ["/bin/sh", "-c", "envsubst < /etc/nginx/nginx.template > /etc/nginx/conf.d/default.conf && nginx -g 'daemon off;'"]
COPY --from=builder /app/dist/energy-manegement/ /usr/share/nginx/html