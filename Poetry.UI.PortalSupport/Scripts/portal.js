


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
        item.setAttribute('app-id', appClass.name);
        item.addEventListener('click', () => this.openApp(appClass.name));

        this.appNames.then(translations => item.innerText = translations[appClass.name] ? translations[appClass.name] : appClass.name);

        this.nav.appendChild(item);
    }

    openApp(name) {
        [...this.nav.children].forEach(c => c.classList.remove('active'));
        [...this.element.querySelectorAll('app')].forEach(a => this.element.removeChild(a));

        var app = this.apps.find(a => a.name == name);

        if (!app) {
            var appClass = this.appClasses.find(c => c.name == name);

            var app = new appClass();

            this.apps.push(app);
        }

        this.nav.querySelector(`[app-id="${name}"]`).classList.add('active');

        this.element.appendChild(app.element);

        if (!app.blades.length) {
            app.openStartBlade();
        }
    }

    openStartApp() {
        if (!this.appClasses[0]) {
            return;
        }

        this.openApp(this.appClasses[0].name);
    }
}

var portal = new Portal();