


/* PORTAL */

class Portal {
    constructor() {
        this.appClasses = [];
        this.apps = [];
        this.container = new Container();
        this.appContainers = [];
        this.root = document.createElement('portal');

        var bootstrap = () => { document.body.appendChild(this.root); this.openStartApp(); }

        if (document.readyState != 'loading') {
            bootstrap();
        } else {
            document.addEventListener('DOMContentLoaded', bootstrap);
        }

        this.nav = document.createElement('portal-nav');
        this.root.appendChild(this.nav);
    }

    addApp(appClass) {
        this.appClasses.push(appClass);

        if (!this.appContainers[appClass.name]) {
            this.appContainers[appClass.name] = new Container(this.container);
        }

        this.injectForApp(appClass.name, 'container', this.appContainers[appClass.name]);

        var item = document.createElement('portal-nav-item');

        (() => {
            var translations = this.container.resolve('appNames');

            item.innerText = translations[appClass.name] ? translations[appClass.name] : appClass.name;
            this.nav.appendChild(item);
        })();

        (() => {
            var portal = this;

            item.addEventListener('click', function () {
                item.classList.add('active');
                portal.openApp(appClass.name);
            });
        })();
    }

    openApp(name) {
        var openApp = this.apps.find(app => app.name == name);

        if (openApp) {
            if (openApp.blades.length) {
                return;
            }

            openApp.openStartBlade();

            return;
        }

        var appClass = this.appClasses.find(appClass => appClass.name == name);

        var app = this.appContainers[name].resolve(appClass);

        this.apps.push(app);

        app.container = this.appContainers[name];

        this.root.appendChild(app.root);

        app.openStartBlade();
    }

    openStartApp() {
        this.openApp(this.appClasses[0].name);
    }

    inject(key, value) {
        this.container.inject(key, value);
    }

    injectForApp(appName, key, value) {
        if (!this.appContainers[appName]) {
            this.appContainers[appName] = new Container(this.container);
        }

        this.appContainers[appName].inject(key, value);
    }
}



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



/* APP */

class App {
    constructor(container) {
        this.container = container;
        this.name = this.constructor.name;
        this.bladeClasses = [];
        this.blades = [];
        this.root = document.createElement('app');
    }

    addBlade(blade) {
        this.bladeClasses.push(blade);
    }

    openBlade(name, data) {
        var bladeClass = this.bladeClasses.find(b => b.name == name);

        if (!bladeClass) {
            throw 'Blade not found';
        }

        var blade = this.container.resolve(bladeClass);

        blade.open(data);

        this.blades.push(blade);
        this.root.appendChild(blade.root);

        return blade;
    }

    openStartBlade() {
        this.openBlade(this.bladeClasses[0].name);
    }

    closeBlade(blade, data) {
        if (!blade) {
            throw 'No blade specified';
        }

        var index = this.blades.indexOf(blade);

        if (index == -1) {
            throw 'Blade not found';
        }

        this.closeBladesAfter(blade);

        blade.close(data);
        this.root.removeChild(blade.root);
        this.blades.splice(index, 1);
    }

    closeBladesAfter(blade) {
        if (!blade) {
            throw 'No blade specified';
        }

        var index = this.blades.indexOf(blade);

        if (index == -1) {
            throw 'Blade not found';
        }

        this.blades.slice(index + 1).reverse().forEach(b => {
            b.close();
            this.root.removeChild(b.root);
        });
        this.blades.splice(index + 1);
    }
}



/* BLADE */

class Blade {
    constructor() {
        this.name = this.constructor.name;
        this.root = document.createElement('blade');
        this.events = {};
    }

    open(data) {

    }

    close(data) {
        this.trigger('close', data);
    }

    addEventListener(event, callback) {
        if (!this.events[event]) {
            this.events[event] = [];
        }

        this.events[event].push(callback);
    }

    trigger(event, data) {
        if (!this.events[event]) {
            return;
        }

        this.events[event].forEach(callback => callback(data));
    }
}