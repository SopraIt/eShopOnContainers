FROM node:10-alpine

ENV VS_ENV dev

WORKDIR /var/www
#COPY ./src/Web/vue-storefront .

COPY ./src/Web/vue-storefront/babel.config.js .
COPY ./src/Web/vue-storefront/config ./config
COPY ./src/Web/vue-storefront/core ./core
COPY ./src/Web/vue-storefront/ecosystem.json .
COPY ./src/Web/vue-storefront/.eslintignore .
COPY ./src/Web/vue-storefront/.eslintrc.js .
COPY ./src/Web/vue-storefront/lerna.json .
COPY ./src/Web/vue-storefront/tsconfig.json .
COPY ./src/Web/vue-storefront/tsconfig-build.json .
COPY ./src/Web/vue-storefront/shims.d.ts .
COPY ./src/Web/vue-storefront/package.json .
COPY ./src/Web/vue-storefront/src ./src
#COPY ./src/Web/vue-storefront/var ./var
COPY ./src/Web/vue-storefront/docker/vue-storefront/vue-storefront.sh .

#COPY ./src/Web/vue-storefront/docker/vue-storefront/vue-storefront.sh /usr/local/bin

# WORKDIR /var/www


RUN apk add --no-cache --virtual .build-deps ca-certificates wget git 
RUN yarn install --no-cache 
RUN apk del .build-deps
RUN yarn build:client 
RUN yarn build:server 
RUN yarn build:sw



CMD ["sh","./vue-storefront.sh"]
#CMD ["vue-storefront.sh"]
