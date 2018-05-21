


/* CONTAINER */

class Container {
    constructor(parent) {
        this.parent = parent;
        this.map = [];
        this.ARGUMENT_NAMES = /([^\s,]+)/g;
    }

    inject(key, value) {
        this.map[key] = value;
    }

    getFunctionParameters(func) {
        var funcString = func.toString();
        var result = funcString.slice(funcString.indexOf('(') + 1, funcString.indexOf(')')).match(this.ARGUMENT_NAMES);

        return result;
    }

    resolve(arg, overrides) {
        if (typeof arg == 'string') {
            return this.resolveParameter(arg, overrides);
        }

        if (typeof arg == 'function') {
            return this.resolveClass(arg, overrides);
        }
    }

    resolveParameter(name, overrides) {
        if (overrides && overrides[name]) {
            return overrides[name];
        }

        var container = this;

        while (container != null) {
            if (container.map[name]) {
                return container.map[name];
            }

            container = container.parent;
        }

        return; // returns undefined
    }

    resolveClass(_class, overrides) {
        var parameters = this.getFunctionParameters(_class);

        if (parameters) {
            var container = this;

            parameters = parameters.map(p => container.resolve(p, overrides));

            if (parameters.length > 10) {
                throw new 'Resolving classes with constructors with over 10 parameters is not supported';
            }

            return new _class(parameters[0], parameters[1], parameters[2], parameters[3], parameters[4], parameters[5], parameters[6], parameters[7], parameters[8], parameters[9]);
        }

        return new _class();
    }
}
