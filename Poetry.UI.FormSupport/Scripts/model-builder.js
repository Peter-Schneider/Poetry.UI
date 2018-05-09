class ModelBuilder {
    build(instance) {
        var target = {};
        var properties = {};

        for (var key in instance) {
            if (!instance.hasOwnProperty(key)) {
                continue;
            }

            properties[key] = this.buildProperty(target, instance, key);
        }

        properties['_events'] = {
            value: {}
        };

        Object.defineProperties(target, properties);

        function onChange(key, callback) {
            if (!target._events[key]) {
                target._events[key] = [];
            }

            target._events[key].push(callback);
        }

        target._onChange = function (key, callback) {
            if (key.indexOf(' ') == -1) {
                onChange(key, callback);
            } else {
                var keys = key.split(' ');

                for (var i = 0; i < keys.length; i++) {
                    onChange(keys[i], callback);
                }
            }
        };

        return target;
    }

    buildProperty(target, instance, key) {
        return {
            enumerable: true,
            get: function () {
                return instance[key];
            },
            set: function (value) {
                instance[key] = value;

                if (target._events[key]) {
                    for (var i = 0; i < target._events[key].length; i++) {
                        var callback = target._events[key][i];

                        callback();
                    }
                }

                if (target._events['']) {
                    for (var i = 0; i < target._events[''].length; i++) {
                        var callback = target._events[''][i];

                        callback();
                    }
                }
            }
        };
    }
}