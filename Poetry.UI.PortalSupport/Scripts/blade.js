


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
