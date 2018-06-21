


/* WINDOW MESSAGE MANAGER */

class WindowMessageManager {
    constructor(element) {
        this.element = element;
        this.callbacks = {};
    }

    on(name, callback) {
        this.callbacks[name] = function (event) {
            if (event.data.indexOf(name) == -1) {
                return;
            }

            var data = JSON.parse(event.data);

            if (data.action != name) {
                return;
            }

            callback(data);
        };

        window.addEventListener('message', this.callbacks[name], false);
    }

    off(name, callback) {
        window.removeEventListener('message', this.callbacks[name], false);
        delete this.callbacks[name];
    }
}