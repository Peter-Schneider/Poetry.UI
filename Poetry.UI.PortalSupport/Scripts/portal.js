


/* PORTAL */

class Portal {
    constructor() {
        this.appClasses = [];
        this.apps = [];
        this.element = document.createElement('portal');
        this.appNames = fetch('Portal/App/GetNames', { credentials: 'include' })
            .then(response => {
                if (!response.ok) {
                    throw new Error(`${response.status} (${response.statusText})`);
                }

                return response.json();
            });

        var bootstrap = () => { document.body.appendChild(this.element); this.openStartApp(); }

        if (document.readyState != 'loading') {
            bootstrap();
        } else {
            document.addEventListener('DOMContentLoaded', bootstrap);
        }

        this.nav = document.createElement('portal-nav');
        this.element.appendChild(this.nav);
    }

    addApp(appClass) {
        this.appClasses.push(appClass);

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

        var app = new appClass();

        this.apps.push(app);

        this.element.appendChild(app.element);

        app.openStartBlade();
    }

    openStartApp() {
        if (!this.appClasses[0]) {
            return;
        }

        this.openApp(this.appClasses[0].name);
    }
}

var portal = new Portal();