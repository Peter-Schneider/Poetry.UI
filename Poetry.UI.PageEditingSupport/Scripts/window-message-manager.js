


/* WINDOW MESSAGE MANAGER */

class WindowMessageManager {
    constructor(element) {
        this.element = element;
        this.callbacks = {};
    }

    send(name, data) {
        var message = JSON.stringify({
            action: name,
            data: data,
        });

        var frame = this.element && this.element.contentWindow ? this.element.contentWindow : parent;

        frame.postMessage(message, '*');
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

            callback(data.data);
        };

        window.addEventListener('message', this.callbacks[name], false);
    }

    off(name, callback) {
        window.removeEventListener('message', this.callbacks[name], false);
        delete this.callbacks[name];
    }
}