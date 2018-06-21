


/* WINDOW MESSAGE MANAGER */

class WindowMessageManager {
    constructor(element) {
        this.element = element;
        this.callbacks = {};
    }

    send(name, data) {
        var message = {
            action: name,
            data: data,
        };

        var frame = this.element && this.element.contentWindow ? this.element.contentWindow : parent;

        frame.postMessage(message, '*');
    }

    on(name, callback) {
        if (!this.callbacks[name]) {
            this.callbacks[name] = [];
        }

        var func = function (event) {
            if (!event.data) {
                return;
            }

            var data = event.data;

            if (data.action != name) {
                return;
            }

            callback(data.data);
        };

        this.callbacks[name].push({
            func,
            callback,
        });

        window.addEventListener('message', func, false);
    }

    off(name, callback) {
        var item = this.callbacks[name].find(i => i.callback === callback);
        
        window.removeEventListener('message', item.func, false);
        this.callbacks[name].splice(this.callbacks[name].indexOf(item), 1);
    }
}