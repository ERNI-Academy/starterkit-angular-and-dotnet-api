# Appui

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 10.1.2.

## Development server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The app will automatically reload if you change any of the source files.

## Code scaffolding

Run `ng generate component component-name` to generate a new component. You can also use `ng generate directive|pipe|service|class|guard|interface|enum|module`.

## Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory. Use the `--prod` flag for a production build.

## Running unit tests

Run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io).

## Running end-to-end tests

Run `ng e2e` to execute the end-to-end tests via [Protractor](http://www.protractortest.org/).

## Docker

We have 2 diferent docker files:

- Dockerfile: this is the file used to create the image for production purposes
- Dockerfile.dev: this is the file used to create the image for developer purpose

In development we don't use the same approach as in "production Dockerfile" (using the 'env.template.js' file) for 2 reasons:

1. 'envsubst' command does not exists in this image. (this might be workaround by changing the image, installing it or using other npm command for the same purpose)
2. We are working over the source code (through a volume; instead of a COPY). This means that when we write on a file like 'env.js' we are modifying the source code and it would appear in our "git changes" (this might be avoided putting the original 'env.js' in git ignore)

## Further help

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI README](https://github.com/angular/angular-cli/blob/master/README.md).
