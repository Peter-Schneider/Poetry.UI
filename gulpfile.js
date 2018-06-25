const del = require('del');
const gulp = require('gulp');
const sourcemaps = require('gulp-sourcemaps');
const sass = require('gulp-sass');

gulp.task('clean', () => {
  return del([
    './**/*.css',
  ]);
});

gulp.task('styles', () => {
  return gulp.src(['./Poetry.UI.*/**/*.scss'], { base: "./" })
    .pipe(sourcemaps.init())
    .pipe(sass({ outputStyle: 'compact' }).on('error', sass.logError))
    .pipe(sourcemaps.write())
    .pipe(gulp.dest('.'));
});

gulp.task('watch', () => {
  gulp.watch('./styles/**/*.scss', ['styles']);
});

gulp.task('build', ['clean', 'styles', 'watch']);
