


/* BLADE */

class Blade {
    constructor(fullscreen) {
        this.element = document.createElement('blade');
        this.containers = {};
    }

    setFullscreen(value) {
        if (value) {
            this.element.classList.add('blade-fullscreen');
        } else {
            this.element.classList.remove('blade-fullscreen');
        }

        return this;
    }

    setItems(id, items) {
        if (!this.containers[id]) {
            this.containers[id] = document.createElement(id);
            this.element.appendChild(this.containers[id]);
        } else {
            [...this.containers[id].children].forEach(this.containers[id].removeChild);
        }
        
        if (items.length) {
            this.containers[id].style.display = '';
        } else {
            this.containers[id].style.display = 'none';
        }

        items.forEach(item => {
            if (item instanceof Node) {
                this.containers[id].appendChild(item);
            } else if (item.element instanceof Node) {
                this.containers[id].appendChild(item.element);
            } else {
                var text = document.createElement(`${id}-text`);
                text.innerText = item;
                this.containers[id].appendChild(text);
            }
        });
    }

    setTitle(...items) {
        this.setItems('blade-title', items);
    }

    setToolbar(...items) {
        this.setItems('blade-toolbar', items);
    }

    setContent(...items) {
        this.setItems('blade-content', items);
    }

    setCustomContent(...items) {
        this.setItems('blade-custom-content', items);
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
