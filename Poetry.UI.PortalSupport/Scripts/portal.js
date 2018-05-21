


/* PORTAL */

class Portal {
    constructor() {
        this.appClasses = [];
        this.apps = [];
        this.container = new Container();
        this.appContainers = [];
        this.root = document.createElement('portal');
        this.appNames = fetch('Portal/App/GetNames', { credentials: 'include' })
            .then(response => {
                if (!response.ok) {
                    throw new Error(`${response.status} (${response.statusText})`);
                }

                return response.json();
            });

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

        item.innerText = '...';
        this.appNames.then(translations => item.innerText = translations[appClass.name] ? translations[appClass.name] : appClass.name);

        this.nav.appendChild(item);

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
        if (!this.appClasses[0]) {
            return;
        }

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

var portal = new Portal();
