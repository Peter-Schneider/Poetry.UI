import FadeInEffect from "./Effects/fade-in.js";
import FadeOutEffect from "./Effects/fade-out.js";



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

        var bootstrap = () => {
            document.body.appendChild(this.element);

            if (location.hash) {
                var match = location.hash.substr(1).match(/^[a-z0-9]+/i);

                if (match) {
                    var appId = match[0].toLowerCase();

                    var app = this.apps.find(a => a.name.toLowerCase() == appId);

                    if (app) {
                        this.openApp(app);
                    } else {
                        this.startApp = appId;
                    }
                }
            }
        }

        if (document.readyState != 'loading') {
            bootstrap();
        } else {
            document.addEventListener('DOMContentLoaded', bootstrap);
        }

        this.nav = document.createElement('portal-nav');

        var toggle = document.createElement('portal-nav-toggle');
        var hideToggle = () => {
            if (!toggle.classList.contains('active')) {
                return;
            }

            toggle.classList.remove('active');

            new FadeOutEffect(this.nav).onComplete(() => this.element.removeChild(this.nav));
        };
        toggle.addEventListener('click', () => {
            if (!toggle.classList.contains('active')) {
                toggle.classList.add('active');

                this.element.appendChild(this.nav);

                new FadeInEffect(this.nav);
            } else {
                hideToggle();
            }
        });
        document.documentElement.addEventListener('click', event => {
            if (!event.target.matches('portal-nav, portal-nav-toggle')) {
                hideToggle();
            }
        });
        this.element.appendChild(toggle);
    }

    addApp(app) {
        this.apps.push(app);
        
        var item = document.createElement('portal-nav-item');

        item.innerText = '...';
        item.setAttribute('app-id', app.name);
        item.addEventListener('click', () => this.openApp(app));

        this.appNames.then(translations => item.innerText = translations[app.name] ? translations[app.name] : app.name);

        this.nav.appendChild(item);

        if (this.startApp && this.startApp == app.name.toLowerCase()) {
            setTimeout(() => this.openApp(app, false), 1);
        }
    }

    openApp(app, updateHash = true) {
        [...this.nav.children].forEach(c => c.classList.remove('active'));
        [...this.element.querySelectorAll('app')].forEach(a => this.element.removeChild(a));

        this.nav.querySelector(`[app-id="${app.name}"]`).classList.add('active');

        this.element.appendChild(app.element);

        if (updateHash) {
            history.pushState(null, null, `#${app.name.toLowerCase()}`);
        }

        app.open();
    }

    openStartApp() {
        if (!this.apps.length) {
            return;
        }

        this.openApp(this.apps[0]);
    }
}

export default new Portal();