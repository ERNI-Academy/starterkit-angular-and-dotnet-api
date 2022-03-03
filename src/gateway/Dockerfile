FROM nginx

COPY ./src/gateway/nginx.dev.conf /tmp/nginx.conf.template

# RUN echo $(printenv)

EXPOSE 80

# default value in case it's not defined
ENV WEB_APP_PORT=80

# IMPORTANT: only substitute specified variables (ex: $WEB_APP_PORT). Otherwise other variables inside the file (ex: $request_method, $uri, $http_upgrade)
# would be replaced by "" (EMPTY) and nginx would crash giving "unrelated errors"
CMD /bin/bash -c "envsubst '\$WEB_APP_PORT \$VAR2' < /tmp/nginx.conf.template > /etc/nginx/nginx.conf  && nginx -g 'daemon off;'" 

# NOTE: previous CMD command cannot be executed as part of "build time" (RUN ...) as environment variables are not accessible during BUILD.
# during BUILD "args" should be used instead
# RUN /bin/bash -c "envsubst '\$WEB_APP_PORT \$VAR2' < /tmp/nginx.conf.template > /etc/nginx/nginx.conf" 
