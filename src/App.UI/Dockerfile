#################
# Build the app #
#################
FROM node:12-alpine as build
WORKDIR /app
COPY src/App.UI/package.json .
COPY src/App.UI/package-lock.json .
RUN npm install
# @angular/cli is already installed as a dependency
# RUN npm install -g @angular/cli@10.1.2
COPY src/App.UI/ .
RUN npm run build:prod

################
# Run in NGINX #
################
FROM nginx:alpine
EXPOSE 80
EXPOSE 443
COPY --from=build /app/dist /usr/share/nginx/html

# default value in case it's not defined
ENV API_URL=/

# When the container starts, replace the env.js with values from environment variables
CMD ["/bin/sh",  "-c",  "envsubst < /usr/share/nginx/html/assets/env.template.js > /usr/share/nginx/html/assets/env.js && exec nginx -g 'daemon off;'"]