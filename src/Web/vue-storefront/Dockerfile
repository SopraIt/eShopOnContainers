FROM node:10-alpine

ENV VS_ENV dev

#COPY . /var/www

COPY ./babel.config.js /var/www
COPY ./config /var/www/config
COPY ./core /var/www/core
COPY ./ecosystem.json /var/www
COPY ./.eslintignore /var/www
COPY ./.eslintrc.js /var/www
COPY ./lerna.json /var/www
COPY ./tsconfig.json /var/www
COPY ./tsconfig-build.json /var/www
COPY ./shims.d.ts /var/www
COPY ./package.json /var/www
COPY ./src /var/www/src
COPY ./var /var/www/var

COPY ./docker/vue-storefront/vue-storefront.sh /usr/local/bin

WORKDIR /var/www


RUN apk add --no-cache --virtual .build-deps ca-certificates wget git 
RUN yarn install --no-cache 
#RUN yarn dev
RUN apk del .build-deps



CMD ["vue-storefront.sh"]
