


/* PORTAL */

class Portal {
    constructor() {
        this.apps = [];
        this.element = document.createElement('portal');
        this.appNames = fetch('Portal/App/GetNames', { credentials: 'include' })
            .then(response => {
                if (!response.ok) {
                    throw new Error(`${response.status} (${response.statusText})`);
                }

                return response.json();
            });

        var bootstrap = () => { document.body.appendChild(this.element); }

        if (document.readyState != 'loading') {
            bootstrap();
        } else {
            document.addEventListener('DOMContentLoaded', bootstrap);
        }

        this.nav = document.createElement('portal-nav');
        this.element.appendChild(this.nav);
    }

    addApp(app) {
        this.apps.push(app);
        
        var item = document.createElement('portal-nav-item');

        item.innerText = '...';
        item.setAttribute('app-id', app.name);
        item.addEventListener('click', () => this.openApp(app));

        this.appNames.then(translations => item.innerText = translations[app.name] ? translations[app.name] : app.name);

        this.nav.appendChild(item);
    }

    openApp(app) {
        [...this.nav.children].forEach(c => c.classList.remove('active'));
        [...this.element.querySelectorAll('app')].forEach(a => this.element.removeChild(a));

        this.nav.querySelector(`[app-id="${app.name}"]`).classList.add('active');

        this.element.appendChild(app.element);

        app.open();
    }

    openStartApp() {
        if (!this.apps.length) {
            return;
        }

        this.openApp(this.apps[0]);
    }
}

var portal = new Portal();