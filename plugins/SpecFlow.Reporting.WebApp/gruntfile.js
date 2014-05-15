module.exports = function (grunt) {
	grunt.loadNpmTasks('grunt-contrib-uglify');
	grunt.loadNpmTasks('grunt-contrib-copy');
	grunt.loadNpmTasks('grunt-contrib-watch');
	grunt.loadNpmTasks('grunt-angular-templates');
	grunt.loadNpmTasks('grunt-contrib-less');

	grunt.initConfig({
		pkg: grunt.file.readJSON('package.json'),

		uglify: {
			build: {
				options: {
					compress: false,
					mangle: false,
					banner: '<%= pkg.banner %>'
				},

				files: {
					'dist/scripts.js': [
                        'bower_components/jquery/dist/jquery.js',
                        'bower_components/lodash/dist/lodash.js',
                        'bower_components/angular/angular.js',
                        'bower_components/angular-route/angular-route.js',
                        'bower_components/uri.js/src/URI.js',
                        'bower_components/bootstrap/js/collapse.js',
                        'bower_components/bootstrap/js/dropdown.js',
                        'bower_components/angular-ui-bootstrap-bower/ui-bootstrap-tpls.js',
                        'src/app.js',
                        'src/app.routes.js',
                        'src/reportService.js',
                        'src/directives/result-badge.js',
                        'src/views/dashboard.controller.js',
                        'src/views/scenario.controller.js',
                        'src/templates.js'
					]
				}
			}
		},
		ngtemplates: {
			options: {
				module: 'app',
				htmlmin: {
					collapseWhitespace: true,
					collapseBooleanAttributes: true
				},
				url: function (url) {
					return url.replace('src/', '');
				}
			},
			build: {
				src: 'src/**/*.tpl.html',
				dest: 'src/templates.js'
			}
		},
		copy: {
			build: {
				files: [
                    { src: ['src/index.html'], dest: 'dist/index.html' },
                    { expand: true, cwd: 'bower_components/bootstrap/', src: ['fonts/**'], dest: 'dist/' }
				]
			}
		},
		watch: {
			scripts: {
				files: ['src/**/*.*'],
				tasks: ['build'],
				options: {
					spawn: false,
				},
			},
		},
		less: {
			build: {
				options: {
					compress: true
				},
				files: {
					"dist/styles.css": [
                        'bower_components/bootstrap/less/bootstrap.less',
                        'bower_components/bootstrap/less/theme.less',
                        'src/styles/styles.less'
					]
				}
			}
		},
	});

	grunt.registerTask('build', ['ngtemplates', 'uglify', 'copy', 'less']);
};