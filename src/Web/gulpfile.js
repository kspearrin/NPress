/// <binding BeforeBuild='less, lib' Clean='clean' ProjectOpened='less, lib' />

var gulp = require("gulp"),
    rimraf = require("rimraf"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify"),
    project = require("./project.json"),
    less = require("gulp-less");

var paths = {
    webroot: "./" + project.webroot + "/"
};

paths.js = paths.webroot + "js/**/*.js";
paths.minJs = paths.webroot + "js/**/*.min.js";
paths.concatJsDest = paths.webroot + "js/blog.min.js";
paths.libDir = paths.webroot + "lib/";
paths.bowerDir = "./bower_components/";
paths.lessDir = "./Less/";
paths.cssDir = paths.webroot + "css/";
paths.jsDir = paths.webroot + "js/";

gulp.task("clean:js", function (cb) {
    rimraf(paths.concatJsDest, cb);
});

gulp.task("clean:css", function (cb) {
    rimraf(paths.cssDir, cb);
});

gulp.task("clean:lib", function (cb) {
    rimraf(paths.libDir, cb);
});

gulp.task("clean", ["clean:js", "clean:css", "clean:lib"]);

gulp.task("min:js", function () {
    gulp.src([paths.js, "!" + paths.minJs], { base: "." })
        .pipe(concat(paths.concatJsDest))
        .pipe(uglify())
        .pipe(gulp.dest("."));
});

gulp.task("min", ["min:js"]);

gulp.task("lib", function () {
    // bootstrap
    gulp.src(paths.bowerDir + "bootstrap/dist/**/*")
        .pipe(gulp.dest(paths.libDir + "bootstrap"));
    // font-awesome
    gulp.src(paths.bowerDir + "font-awesome/css/*")
        .pipe(gulp.dest(paths.libDir + "font-awesome/css"));
    gulp.src(paths.bowerDir + "font-awesome/fonts/*")
        .pipe(gulp.dest(paths.libDir + "font-awesome/fonts"));
    // jquery
    gulp.src(paths.bowerDir + "jquery/dist/*.js")
        .pipe(gulp.dest(paths.libDir + "jquery"));
    // jquery-validation
    gulp.src(paths.bowerDir + "jquery-validation/dist/*.js")
        .pipe(gulp.dest(paths.libDir + "jquery-validation"));
    // jquery-validation-unobtrusive
    gulp.src(paths.bowerDir + "jquery-validation-unobtrusive/*.js")
        .pipe(gulp.dest(paths.libDir + "jquery-validation-unobtrusive"));
});

gulp.task("less", function () {
    gulp.src(paths.lessDir + 'admin.less')
        .pipe(less())
        .pipe(gulp.dest(paths.cssDir));

    gulp.src(paths.lessDir + 'admin.less')
        .pipe(less())
        .pipe(concat(paths.cssDir + "admin.min.css"))
        .pipe(cssmin())
        .pipe(gulp.dest("."));

    gulp.src(paths.lessDir + 'blog.less')
        .pipe(less())
        .pipe(gulp.dest(paths.cssDir));

    gulp.src(paths.lessDir + 'blog.less')
        .pipe(less())
        .pipe(concat(paths.cssDir + "blog.min.css"))
        .pipe(cssmin())
        .pipe(gulp.dest("."));
});
