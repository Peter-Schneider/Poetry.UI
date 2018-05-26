


/* BLADE */

class Blade {
    constructor() {
        this.name = this.constructor.name;
        this.root = document.createElement('blade');
    }

    open(data) {

    }

    onClose(callback) {
        this.onCloseCallback = callback;

        return this;
    }

    close(data) {
        if (this.onCloseCallback) {
            this.onCloseCallback(data);
        }
    }
}
